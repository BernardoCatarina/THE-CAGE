using UnityEngine;

public class MagiaMiniBoss : MonoBehaviour
{
    public float velocidade = 8f;
    public float tempoDeVida = 4f;

    [Header("Ajustes Visuais")]
    public float ajusteRotacaoZ = 180f;
    public bool inverterYParaEsquerda = false;
    public bool inverterYParaDireita = true;

    private Rigidbody2D rig;
    private Vector3 escalaInicial;
    private float tempoNascimento;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        escalaInicial = transform.localScale;
        tempoNascimento = Time.time;

        GameObject alvo = GameObject.FindGameObjectWithTag("AlvoMagia");
        Vector3 posicaoAlvo;

        if (alvo != null)
        {
            posicaoAlvo = alvo.transform.position;
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            posicaoAlvo = player != null ? player.transform.position + new Vector3(0f, 1f, 0f) : transform.position;
        }

        Vector2 direcao = (posicaoAlvo - transform.position).normalized;
        rig.linearVelocity = direcao * velocidade;

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo + ajusteRotacaoZ);

        // --- CORREÇÃO DEFINITIVA DE ESPELHAMENTO ---
        if (spriteRenderer != null)
        {
            if (direcao.x < 0 && inverterYParaEsquerda)
            {
                spriteRenderer.flipY = true;
            }
            else if (direcao.x > 0 && inverterYParaDireita)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
        }

        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        float progresso = (Time.time - tempoNascimento) / tempoDeVida;
        transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, progresso);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}