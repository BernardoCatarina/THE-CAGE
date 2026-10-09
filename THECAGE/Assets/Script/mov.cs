using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class mov : MonoBehaviour
{
    public float tempoDeRecarga = 0.5f;
    private float proximoTiro = 0f;

    public float Speed;
    public float Jumpforce;
    public GameObject bullet;
    private Rigidbody2D rig;
    public bool isJumping;
    private bool r2EstavaPressionado = false;
    private bool usandoControle = false;
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

    [Header("Habilidades Desbloque�veis")]
    public bool possuiPuloDuplo = false;
    private bool podePuloDuplo = false;

    public bool possuiDash = false;
    public float velocidadeDash = 20f;
    public float tempoDash = 0.2f;
    public float tempoRecargaDash = 1f;
    private bool estaDandoDash = false;
    private float tempoProximoDash = 0f;

    [Header("Buff da Espada")]
    public SpriteRenderer spriteDaEspada;
    public TMPro.TextMeshProUGUI textoTimerEspada; 
    public int danoExtraBuff = 10;

    [Header("UI e Recargas")]
    public UnityEngine.UI.Image iconeDash;
    public UnityEngine.UI.Image iconeRecargaEspada;
    public UnityEngine.UI.Image iconePuloDuplo;
    public float tempoRecargaEspada = 5f;
    private float tempoProximoTiroEspada = 0f;

    [Header("Sistema de Mira (Crosshair)")]
    public Transform objetoMira; 
    public float distanciaDaMira = 2.5f; 
    private Vector2 direcaoAtualDaMira = Vector2.right; 
    void Start()
    {
        possuiPuloDuplo = PlayerPrefs.GetInt("PuloDuploSalvo", 0) == 1;
        possuiDash = PlayerPrefs.GetInt("DashSalvo", 0) == 1;

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

        // --- SISTEMA DE NASCER NA PORTA CORRETA ---
        string destino = PlayerPrefs.GetString("PortaDestino", "");

        if (destino != "")
        {
            
            GameObject pontoSpawn = GameObject.Find(destino);

            if (pontoSpawn != null)
            {
                
                transform.position = pontoSpawn.transform.position;
            }

            
            PlayerPrefs.SetString("PortaDestino", "");
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

        // --- MOVIMENTO DO PET ESPADA ---
        if (possuiEspada && espadaPet != null && !espadaAtacando)
        {
            Vector3 centroDoBuck = transform.position + new Vector3(0f, 1f, 0f);

            float distanciaEspada = 2.2f;

            Vector3 posicaoAlvo = centroDoBuck + new Vector3(direcaoAtualDaMira.x, direcaoAtualDaMira.y, 0f) * distanciaEspada;

            espadaPet.transform.position = Vector3.Lerp(espadaPet.transform.position, posicaoAlvo, Time.deltaTime * 6f);

            float angulo = (Mathf.Atan2(direcaoAtualDaMira.y, direcaoAtualDaMira.x) * Mathf.Rad2Deg) - 90f;
            espadaPet.transform.rotation = Quaternion.Euler(0f, 0f, angulo);
        }


        if (estaDandoDash)
        {
            return;
        }


       
        if (iconeDash != null)
        {
           
            iconeDash.transform.parent.gameObject.SetActive(possuiDash);

            if (possuiDash)
            {
                if (Time.time < tempoProximoDash)
                {
                    
                    iconeDash.fillAmount = 1f - ((tempoProximoDash - Time.time) / tempoRecargaDash);
                }
                else
                {
                    
                    iconeDash.fillAmount = 1f;
                }
            }
        }

        
        if (iconeRecargaEspada != null)
        {
            
            iconeRecargaEspada.transform.parent.gameObject.SetActive(possuiEspada);

            if (possuiEspada)
            {
                if (Time.time < tempoProximoTiroEspada)
                {
                    iconeRecargaEspada.fillAmount = 1f - ((tempoProximoTiroEspada - Time.time) / tempoRecargaEspada);
                }
                else
                {
                    iconeRecargaEspada.fillAmount = 1f;
                }
            }
        }

       
        if (iconePuloDuplo != null)
        {
            
            iconePuloDuplo.transform.parent.gameObject.SetActive(possuiPuloDuplo);

            if (possuiPuloDuplo)
            {
                
                if (podePuloDuplo)
                {
                    iconePuloDuplo.fillAmount = 1f; 
                }
                else
                {
                    iconePuloDuplo.fillAmount = 0f; 
                }
            }
        }

        if (possuiDash && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.JoystickButton2)) && Time.time >= tempoProximoDash)
        {
            StartCoroutine(ExecutarDash());
        }

        if (animator != null)
        {
            animator.SetFloat("VelocidadeY", rig.linearVelocity.y);
        }

        Move();
        Jump();

        float valorEixoR2 = Input.GetAxis("TiroR2");
        bool r2PressionadoAgora = (valorEixoR2 > 0.5f) || Input.GetKey(KeyCode.JoystickButton7);

        bool r2ApertouNesteFrame = r2PressionadoAgora && !r2EstavaPressionado;

        bool querAtirar = modoMetralhadora ? (Input.GetButton("Fire1") || r2PressionadoAgora) : (Input.GetButtonDown("Fire1") || r2ApertouNesteFrame);

        r2EstavaPressionado = r2PressionadoAgora;

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

            
            Vector3 posicaoDoTiro = transform.position + new Vector3(0f, 1f, 0f);

            
            float anguloTiro = Mathf.Atan2(direcaoAtualDaMira.y, direcaoAtualDaMira.x) * Mathf.Rad2Deg;

           
            Instantiate(bullet, posicaoDoTiro, Quaternion.Euler(0f, 0f, anguloTiro));


            if (possuiEspada && projetilEspada != null && !espadaAtacando && Time.time >= tempoProximoTiroEspada)
            {

                tempoProximoTiroEspada = Time.time + tempoRecargaEspada;
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

        // --- 1. DETECTA QUAL DISPOSITIVO ESTÁ SENDO USADO ---
        float aimX = Input.GetAxis("RightHorizontal");
        float aimY = Input.GetAxis("RightVertical");
        Vector3 centroDoBuck = transform.position + new Vector3(0f, 1f, 0f);

        // Se mexer o mouse fisicamente (com uma tolerância menor para captar rápido) ou clicar
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f || Input.GetMouseButton(0))
        {
            usandoControle = false;
        }
        // Se usar o analógico direito da mira ou o gatilho do controle
        else if (Mathf.Abs(aimX) > 0.1f || Mathf.Abs(aimY) > 0.1f || Input.GetAxis("TiroR2") > 0.5f)
        {
            usandoControle = true;
        }

        // Aplica a visibilidade baseada na memória
        Cursor.visible = !usandoControle;
        if (objetoMira != null) objetoMira.gameObject.SetActive(usandoControle);


        // --- 2. COMPORTAMENTO DA MIRA ---
        if (usandoControle)
        {
            // MODO CONTROLE: Só atualiza o ângulo se estiver empurrando o analógico (evita resetar pro meio)
            if (Mathf.Abs(aimX) > 0.1f || Mathf.Abs(aimY) > 0.1f)
            {
                direcaoAtualDaMira = new Vector2(aimX, aimY).normalized;
            }

            if (objetoMira != null)
            {
                objetoMira.position = centroDoBuck + new Vector3(direcaoAtualDaMira.x, direcaoAtualDaMira.y, 0) * distanciaDaMira;
                float anguloMira = Mathf.Atan2(direcaoAtualDaMira.y, direcaoAtualDaMira.x) * Mathf.Rad2Deg;
                objetoMira.rotation = Quaternion.Euler(0f, 0f, anguloMira);
            }
        }
        else
        {
            // MODO MOUSE
            Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicaoMouse.z = 0f;
            direcaoAtualDaMira = (posicaoMouse - centroDoBuck).normalized;
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
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping) 
            {
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0); 
                rig.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);

                if (somPulo != null && audioSource != null) audioSource.PlayOneShot(somPulo);

                isJumping = true;
                podePuloDuplo = possuiPuloDuplo; 
                animator.SetBool("isJumping", true);
            }
            else if (podePuloDuplo) 
            {
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0); 
                rig.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);

                if (somPulo != null && audioSource != null) audioSource.PlayOneShot(somPulo);

                podePuloDuplo = false; 
            }
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

        Vector3 localDoLaser = pontaDaEspada != null ? pontaDaEspada.position : espadaPet.transform.position;

        Vector2 direcaoLaser = direcaoAtualDaMira;
        float anguloLaser = Mathf.Atan2(direcaoLaser.y, direcaoLaser.x) * Mathf.Rad2Deg;

        Instantiate(projetilEspada, localDoLaser, Quaternion.Euler(0f, 0f, anguloLaser));

        if (somLaser != null && audioSource != null)
        {
            audioSource.PlayOneShot(somLaser);
        }

        espadaPet.transform.position -= (Vector3)direcaoLaser * 0.3f; 

        yield return new WaitForSeconds(0.2f);

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
    
    public void DesbloquearPuloDuplo()
    {
        possuiPuloDuplo = true;
        PlayerPrefs.SetInt("PuloDuploSalvo", 1);
    }

    public void DesbloquearDash()
    {
        possuiDash = true;
        PlayerPrefs.SetInt("DashSalvo", 1);
    }


    System.Collections.IEnumerator ExecutarDash()
    {
        estaDandoDash = true;
        tempoProximoDash = Time.time + tempoRecargaDash;

        if (animator != null)
        {
            animator.SetBool("isDashing", true);
        }

        float gravOriginal = rig.gravityScale;
        rig.gravityScale = 0f;

        float direcao = (transform.eulerAngles.y == 0) ? 1f : -1f;

        rig.linearVelocity = new Vector2(direcao * velocidadeDash, 0f);

        yield return new WaitForSeconds(tempoDash);

        rig.gravityScale = gravOriginal;
        rig.linearVelocity = Vector2.zero;
        estaDandoDash = false;

        
        if (animator != null)
        {
            animator.SetBool("isDashing", false);
        }
    }
        public System.Collections.IEnumerator AtivarBuffEspada()
    {
        float tempoRestante = 5f;

        
        if (spriteDaEspada != null) spriteDaEspada.color = Color.red;
                
                
        if (textoTimerEspada != null) textoTimerEspada.gameObject.SetActive(true);

        
        while (tempoRestante > 0)
        {
            if (textoTimerEspada != null)
            {
                textoTimerEspada.text = tempoRestante.ToString("0") + "s";
            }
            yield return new WaitForSeconds(1f);
            tempoRestante--;
        }

       
        if (spriteDaEspada != null) spriteDaEspada.color = Color.white;
        
        if (textoTimerEspada != null) textoTimerEspada.gameObject.SetActive(false);
    }
}
