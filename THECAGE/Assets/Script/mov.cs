using UnityEngine;

public class mov : MonoBehaviour
{

    public float Speed;
    public float Jumpforce;
    public GameObject bullet;
    private Rigidbody2D rig;
    public bool isJumping;
    [SerializeField] private Animator animator;

    [Header("Recarga da Faca")]
    public float tempoDeRecarga = 0.5f; 
    private float proximoTiro = 0f;    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();
        Jump();

        
        if (Input.GetButtonDown("Fire1") && Time.time >= proximoTiro)
        {
            
            proximoTiro = Time.time + tempoDeRecarga;

           
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