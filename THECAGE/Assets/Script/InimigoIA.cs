using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 2f;

    [Header("Limites de Patrulha")]
    public float limiteEsquerdo;
    public float limiteDireito;

    private Rigidbody2D rig;
    private Animator animator;
    private bool indoParaDireita = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

       
        Vector3 escala = transform.localScale;
        escala.x = indoParaDireita ? -1 : 1;
        transform.localScale = escala;
    }

    void FixedUpdate()
    {
       
        animator.SetBool("isMoving", true);

        
        if (indoParaDireita && transform.position.x >= limiteDireito)
        {
            Virar();
        }
        else if (!indoParaDireita && transform.position.x <= limiteEsquerdo)
        {
            Virar();
        }

        
        if (indoParaDireita)
            rig.linearVelocity = new Vector2(velocidade, rig.linearVelocity.y);
        else
            rig.linearVelocity = new Vector2(-velocidade, rig.linearVelocity.y);
    }

    void Virar()
    {
        indoParaDireita = !indoParaDireita;

        Vector3 escala = transform.localScale;
        escala.x = indoParaDireita ? -1 : 1;
        transform.localScale = escala;
    }
}