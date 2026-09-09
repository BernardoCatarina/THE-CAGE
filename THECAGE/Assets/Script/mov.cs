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
    private AudioSource audioSource;

    [Header("Espada Pet")]
    public bool possuiEspada = false;
    public GameObject espadaPet;
    public Transform pontaDaEspada;
    private bool espadaAtacando = false;

    public GameObject projetilEspada;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        municao = PlayerPrefs.GetInt("MunicaoSalva", 0);
        possuiFaca = PlayerPrefs.GetInt("TemFacaSalva", 0) == 1;

        
        possuiEspada = PlayerPrefs.GetInt("TemEspadaSalva", 0) == 1;
        if (possuiEspada && espadaPet != null)
        {
            espadaPet.SetActive(true);
        }
        
        AtualizarTextoMunicao();
    }

    void Update()
    {
        Move();
        Jump();

        if (possuiFaca && municao > 0 && Input.GetButtonDown("Fire1") && Time.time >= proximoTiro)
        {
            municao--;
            PlayerPrefs.SetInt("MunicaoSalva", municao);
            AtualizarTextoMunicao();

            if (somAtaque != null && audioSource != null)
            {
                audioSource.PlayOneShot(somAtaque);
            }

            proximoTiro = Time.time + tempoDeRecarga;


            
            Instantiate(bullet, transform.position, transform.rotation);

            
            if (possuiEspada && projetilEspada != null)
            {
                StartCoroutine(AtaqueEspadaPet()); 
            }

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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

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

        Debug.Log("Espada Pet ativada!");
    }
    IEnumerator AtaqueEspadaPet()
    {
        espadaAtacando = true;

        
        Vector3 posicaoDescanso = new Vector3(-0.193f, 0.3511f, 0f);
        Vector3 posicaoAtaque = new Vector3(0.289f, 0.131f, 0f);

        
        espadaPet.transform.localPosition = posicaoAtaque;

        yield return new WaitForSeconds(0.05f);

        Vector3 localDoLaser = pontaDaEspada != null ? pontaDaEspada.position : espadaPet.transform.position;
        Instantiate(projetilEspada, localDoLaser, transform.rotation);

        yield return new WaitForSeconds(0.2f);

       
        espadaPet.transform.localPosition = posicaoDescanso;

        espadaAtacando = false;
    }
}