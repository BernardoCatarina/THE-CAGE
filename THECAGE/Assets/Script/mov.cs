using UnityEngine;

public class mov : MonoBehaviour
{

    public float Speed;
    public float Jumpforce;
    public GameObject bullet;
    private Rigidbody2D rig;
    public bool isJumping;
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }

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
        // Se apertar o botão de pulo E não estiver pulando...
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);

            isJumping = true; // Trava o pulo duplo
            animator.SetBool("isJumping", true); // Liga a animação de pulo!
        }
    }

    // --- NOVA FUNÇÃO: ADICIONE ISSO ANTES DA ÚLTIMA CHAVE "}" DO SCRIPT ---
    // Essa função verifica se o player encostou em alguma coisa (como o chão)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o objeto que encostamos tiver a Tag "Ground" (Chão)...
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // Libera o pulo novamente
            animator.SetBool("isJumping", false); // Desliga a animação de pulo!
        }
    }
}