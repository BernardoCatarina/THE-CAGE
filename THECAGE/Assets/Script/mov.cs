using UnityEngine;
using System.Collections;
using TMPro;

public class mov : MonoBehaviour
{
    public float tempoDeRecarga = 0.5f;
    private float proximoTiro = 0f;

    public float Speed;
    public float Jumpforce;
    public GameObject bullet;
    private Rigidbody2D rig;
    public bool isJumping;
    [SerializeField] private Animator animator;

    [Header("Sistema de Facas")]
    public bool possuiFaca = false;
    public int municao = 0;
    public TextMeshProUGUI textoMunicao;

    [Header("Sons")]
    public AudioClip somPulo;
    public AudioClip somAtaque;
    public AudioClip somLaser;
    private AudioSource audioSource;

    [Header("Espada Pet")]
    public bool possuiEspada = false;
    public GameObject espadaPet;
    public Transform pontaDaEspada;
    private bool espadaAtacando = false;
    public GameObject projetilEspada;

    [Header("Cheats")]
    public static bool instaKill = false;
    public static bool modoVoo = false;
    private float gravidadeOriginal;
    public static bool modoImortal = false;
    public static bool modoMetralhadora = false;
    private float tempoDeRecargaOriginal;
    public TMPro.TextMeshProUGUI textoAvisoCheat;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        gravidadeOriginal = rig.gravityScale;
        tempoDeRecargaOriginal = tempoDeRecarga;

        municao = PlayerPrefs.GetInt("MunicaoSalva", 0);
        possuiFaca = PlayerPrefs.GetInt("TemFacaSalva", 0) == 1;

        possuiEspada = PlayerPrefs.GetInt("TemEspadaSalva", 0) == 1;
        if (possuiEspada && espadaPet != null)
        {
            espadaPet.SetActive(true);
        }

        AtualizarTextoMunicao();

        if (modoVoo)
        {
            rig.gravityScale = 0f;
            rig.linearVelocity = Vector2.zero;
        }

        if (modoMetralhadora)
        {
            tempoDeRecarga = 0.01f;
        }
    }

    void Update()
    {
        // ---------------- COMANDO DO ADMIN ----------------
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            // +500 Facas (Ctrl + Shift + F)
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColetarFacas(500);
                StartCoroutine(MostrarAvisoCheat("+500 Facas!"));
            }

            // Ligar/Desligar Dano Infinito (Ctrl + Shift + K)
            if (Input.GetKeyDown(KeyCode.K))
            {
                instaKill = !instaKill;
                string status = instaKill ? "LIGADO" : "DESLIGADO";
                StartCoroutine(MostrarAvisoCheat("Dano Infinito: " + status));
            }

            // Ganhar Espada Pet (Ctrl + Shift + E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                ColetarEspada();
                StartCoroutine(MostrarAvisoCheat("Espada Pet Adquirida!"));
            }

            // Ligar/Desligar Modo Imortal (Ctrl + Shift + I)
            if (Input.GetKeyDown(KeyCode.I))
            {
                modoImortal = !modoImortal;
                string status = modoImortal ? "LIGADO" : "DESLIGADO";
                StartCoroutine(MostrarAvisoCheat("Deus: " + status));
            }

            // Ligar/Desligar Modo Voo (Ctrl + Shift + V)
            if (Input.GetKeyDown(KeyCode.V))
            {
                modoVoo = !modoVoo;
                string status = modoVoo ? "LIGADO" : "DESLIGADO";

                if (modoVoo)
                {
                    rig.gravityScale = 0f;
                    rig.linearVelocity = Vector2.zero;
                }
                else
                {
                    rig.gravityScale = gravidadeOriginal;
                }
                StartCoroutine(MostrarAvisoCheat("Voo: " + status));
            }

            // Ligar/Desligar Modo Metralhadora (Ctrl + Shift + M)
            if (Input.GetKeyDown(KeyCode.M))
            {
                modoMetralhadora = !modoMetralhadora;
                string status = modoMetralhadora ? "LIGADO" : "DESLIGADO";

                if (modoMetralhadora)
                {
                    tempoDeRecarga = 0.01f;
                }
                else
                {
                    tempoDeRecarga = tempoDeRecargaOriginal;
                }
                StartCoroutine(MostrarAvisoCheat("Metralhadora: " + status));
            }
        }
        // --------------------------------------------------

        Move();
        Jump();

        bool querAtirar = modoMetralhadora ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

        if (possuiFaca && municao > 0 && querAtirar && Time.time >= proximoTiro)
        {
            municao--;
            PlayerPrefs.SetInt("MunicaoSalva", municao);
            AtualizarTextoMunicao();

            if (somAtaque != null && audioSource != null)
            {
                audioSource.PlayOneShot(somAtaque);
            }

            proximoTiro = Time.time + tempoDeRecarga;

            
            Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicaoMouse.z = 0f;

            
            Vector2 direcaoTiro = (posicaoMouse - transform.position).normalized;
            float anguloTiro = Mathf.Atan2(direcaoTiro.y, direcaoTiro.x) * Mathf.Rad2Deg;

            
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, anguloTiro));

            if (possuiEspada && projetilEspada != null && !espadaAtacando)
            {
                StartCoroutine(AtaqueEspadaPet());
            }
        }
    }

    public void AtualizarTextoMunicao()
    {
        if (textoMunicao != null)
        {
            if (possuiEspada)
            {
                textoMunicao.text = "Facas & Laser: " + municao;
            }
            else
            {
                textoMunicao.text = "Facas: " + municao;
            }
        }
    }

    public void ColetarFacas(int quantidade)
    {
        possuiFaca = true;
        municao += quantidade;

        PlayerPrefs.SetInt("MunicaoSalva", municao);
        PlayerPrefs.SetInt("TemFacaSalva", 1);

        AtualizarTextoMunicao();
    }

    void Move()
    {
        if (modoVoo)
        {
            Vector3 movimentoVoo = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            transform.position += movimentoVoo * Time.deltaTime * (Speed * 1.5f);
        }
        else
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;
        }

        float InputAxis = Input.GetAxis("Horizontal");

        if (InputAxis > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (InputAxis < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }

        if (InputAxis != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);

            if (somPulo != null && audioSource != null)
            {
                audioSource.PlayOneShot(somPulo);
            }

            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    public void ColetarEspada()
    {
        possuiEspada = true;
        PlayerPrefs.SetInt("TemEspadaSalva", 1);

        if (espadaPet != null)
        {
            espadaPet.SetActive(true);
        }

        AtualizarTextoMunicao();
    }

    IEnumerator AtaqueEspadaPet()
    {
        espadaAtacando = true;

        Vector3 posicaoDescanso = new Vector3(-0.193f, 0.3511f, 0f);
        Vector3 posicaoAtaque = new Vector3(0.289f, 0.131f, 0f);

        espadaPet.transform.localPosition = posicaoAtaque;

        yield return new WaitForSeconds(0.05f);

        Vector3 localDoLaser = pontaDaEspada != null ? pontaDaEspada.position : espadaPet.transform.position;

        Vector3 posicaoMouseLaser = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoMouseLaser.z = 0f;

        Vector2 direcaoLaser = (posicaoMouseLaser - localDoLaser).normalized;
        float anguloLaser = Mathf.Atan2(direcaoLaser.y, direcaoLaser.x) * Mathf.Rad2Deg;

        Instantiate(projetilEspada, localDoLaser, Quaternion.Euler(0f, 0f, anguloLaser));

        if (somLaser != null && audioSource != null)
        {
            audioSource.PlayOneShot(somLaser);
        }

        yield return new WaitForSeconds(0.2f);

        espadaPet.transform.localPosition = posicaoDescanso;

        espadaAtacando = false;
    }

    IEnumerator MostrarAvisoCheat(string mensagem)
    {
        if (textoAvisoCheat != null)
        {
            textoAvisoCheat.text = mensagem;
            textoAvisoCheat.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            textoAvisoCheat.gameObject.SetActive(false);
        }
    }
}
