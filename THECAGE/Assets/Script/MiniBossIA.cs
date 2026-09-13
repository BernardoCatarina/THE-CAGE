using UnityEngine;

public class MiniBossIA : MonoBehaviour
{
    [Header("Alvo e Movimento")]
    public Transform player;
    public float velocidade = 2.5f;
    public float distanciaDeAtaque = 5f;
    public float distanciaDeVisao = 10f;

    [Header("Limites da Arena")]
    public float limiteEsquerdo;
    public float limiteDireito;

    [Header("Ataque")]
    public GameObject prefabSerra;
    public Transform pontoDeTiro;
    public float tempoEntreAtaques = 2.5f;
    private float proximoTiro;

    private Animator anim;
    private bool estaAndando;
    private bool patrulhandoParaDireita = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

       
        if (distancia > distanciaDeVisao)
        {
            Patrulhar();
        }
        else
        {
            PerseguirPlayer();
        }

        
        if (distancia <= distanciaDeAtaque)
        {
            if (Time.time >= proximoTiro)
            {
                Atacar();
            }
        }

        anim.SetBool("isMoving", estaAndando);
    }

    void Patrulhar()
    {
        estaAndando = true;
        float alvoX = patrulhandoParaDireita ? limiteDireito : limiteEsquerdo;

        VirarParaAlvo(alvoX);

        float proximoX = Mathf.MoveTowards(transform.position.x, alvoX, (velocidade / 2) * Time.deltaTime);
        transform.position = new Vector2(proximoX, transform.position.y);

        if (Mathf.Abs(transform.position.x - alvoX) < 0.1f)
        {
            patrulhandoParaDireita = !patrulhandoParaDireita;
        }
    }

    void PerseguirPlayer()
    {
        VirarParaAlvo(player.position.x);

        float proximoX = Mathf.MoveTowards(transform.position.x, player.position.x, velocidade * Time.deltaTime);
        proximoX = Mathf.Clamp(proximoX, limiteEsquerdo, limiteDireito);

        if (Mathf.Abs(transform.position.x - proximoX) > 0.001f)
        {
            transform.position = new Vector2(proximoX, transform.position.y);
            estaAndando = true;
        }
        else
        {
            
            estaAndando = false;
        }
    }

    void VirarParaAlvo(float posAlvoX)
    {
        float tamanho = Mathf.Abs(transform.localScale.y);

        if (posAlvoX > transform.position.x)
        {
            transform.localScale = new Vector3(-tamanho, tamanho, tamanho);
        }
        else
        {
            transform.localScale = new Vector3(tamanho, tamanho, tamanho);
        }
    }

    void Atacar()
    {
        anim.SetTrigger("attack");
        Instantiate(prefabSerra, pontoDeTiro.position, transform.rotation);
        proximoTiro = Time.time + tempoEntreAtaques;
    }
}