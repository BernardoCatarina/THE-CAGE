using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 2f;
    public float distanciaVisao = 5f;

    private Transform player;
    private Rigidbody2D rig;
    private Animator animator;
    private bool indoParaDireita = false;
    private bool travadoNaParede = false;

    private float tempoUltimaVirada;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;
    }

    
    void FixedUpdate()
    {
        if (player == null) return;

        float distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

        if (!travadoNaParede)
        {
            if (distanciaDoPlayer <= distanciaVisao)
            {
                PerseguirPlayer();
            }
            else
            {
                MoverFrente();
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
                travadoNaParede = false;
            }
        }
    }

    void PerseguirPlayer()
    {
        if (player.position.x > transform.position.x && !indoParaDireita) Virar();
        else if (player.position.x < transform.position.x && indoParaDireita) Virar();

        MoverFrente();
    }

    void MoverFrente()
    {
        if (indoParaDireita) rig.linearVelocity = new Vector2(velocidade, rig.linearVelocity.y);
        else rig.linearVelocity = new Vector2(-velocidade, rig.linearVelocity.y);
    }

    void Virar()
    {
        if (Time.time < tempoUltimaVirada + 0.2f) return;
        tempoUltimaVirada = Time.time;

        indoParaDireita = !indoParaDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimiteInimigo"))
        {
            Virar();

            if (player != null)
            {
                float distancia = Vector2.Distance(transform.position, player.position);
                if (distancia <= distanciaVisao)
                {
                    travadoNaParede = true;
                }
            }
        }
    }
}