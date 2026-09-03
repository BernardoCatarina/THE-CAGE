using UnityEngine;
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

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        
        municao = PlayerPrefs.GetInt("MunicaoSalva", 0);
        possuiFaca = PlayerPrefs.GetInt("TemFacaSalva", 0) == 1;

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

            proximoTiro = Time.time + tempoDeRecarga;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
    public void AtualizarTextoMunicao()
    {
        if (textoMunicao != null)
        {
            textoMunicao.text = "Facas: " + municao;
        }
    }


    public void ColetarFacas(int quantidade)
    {
        possuiFaca = true;
        municao += quantidade;

        PlayerPrefs.SetInt("MunicaoSalva", municao); // Salva a nova quantidade
        PlayerPrefs.SetInt("TemFacaSalva", 1);       // Grava que o jogador destravou a arma

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
}