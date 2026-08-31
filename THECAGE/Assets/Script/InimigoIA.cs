using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 2f;
    public float distanciaVisao = 5f; 

    [Header("Referências")]
    public Transform detectorDeChao; 

    private Transform player;
    private Rigidbody2D rig;
    private Animator animator;
    private bool indoParaDireita = false; 

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null)
        {
            player = objPlayer.transform;
        }
    }

    void Update()
    {
        if (player == null) return; 

        
        RaycastHit2D temChao = Physics2D.Raycast(detectorDeChao.position, Vector2.down, 1f);

        
        float distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

        
        if (temChao.collider != null)
        {
            if (distanciaDoPlayer <= distanciaVisao)
            {
                PerseguirPlayer();
            }
            else
            {
                Patrulhar();
            }

            
            animator.SetBool("isMoving", true);
        }
        
        else
        {
            
            rig.linearVelocity = new Vector2(0, rig.linearVelocity.y);

           
            animator.SetBool("isMoving", false);

            
            if (distanciaDoPlayer > distanciaVisao)
            {
                Virar();
            }
        }
    }

    void Patrulhar()
    {
        MoverFrente();
    }

    void PerseguirPlayer()
    {
        
        if (player.position.x > transform.position.x && !indoParaDireita)
        {
            Virar();
        }
        
        else if (player.position.x < transform.position.x && indoParaDireita)
        {
            Virar();
        }

        MoverFrente();
    }

    void MoverFrente()
    {
        if (indoParaDireita)
            rig.linearVelocity = new Vector2(velocidade, rig.linearVelocity.y);
        else
            rig.linearVelocity = new Vector2(-velocidade, rig.linearVelocity.y);
    }

    void Virar()
    {
        indoParaDireita = !indoParaDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1; 
        transform.localScale = escala;
    }
}