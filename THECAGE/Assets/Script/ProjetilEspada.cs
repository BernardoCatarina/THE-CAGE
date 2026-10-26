
using UnityEngine;

public class ProjetilEspada : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public float speed = 8f;
    public int danoDaEspada = 5;

    private Rigidbody2D rig;
    private Collider2D colisor;
    private Vector3 escalaOriginal;
    private int danoBase;
    private bool aguardandoDisparo = false;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        colisor = GetComponent<Collider2D>();

        escalaOriginal = transform.localScale;
        danoBase = danoDaEspada;
    }

    void Start()
    {
        if (!aguardandoDisparo && rig != null)
        {
            rig.linearVelocity = transform.right * speed;
        }
    }

    
    public void PrepararCarga()
    {
        aguardandoDisparo = true;

        if (rig != null)
        {
            rig.linearVelocity = Vector2.zero;
        }

        if (colisor != null)
        {
            colisor.enabled = false;
        }
    }

    
    public void AtualizarCarga(float progresso, float tamanhoMaximo)
    {
        progresso = Mathf.Clamp01(progresso);

        float multiplicadorTamanho =
            Mathf.Lerp(1f, tamanhoMaximo, progresso);

        transform.localScale =
            escalaOriginal * multiplicadorTamanho;

        danoDaEspada = Mathf.RoundToInt(
            danoBase * Mathf.Lerp(1f, 2f, progresso)
        );
    }

    
    public void DispararCarregado(Vector2 direcao)
    {
        aguardandoDisparo = false;

        if (colisor != null)
        {
            colisor.enabled = true;
        }

        if (rig != null)
        {
            rig.linearVelocity = direcao.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aguardandoDisparo)
            return;

        if (collision.CompareTag("Enemy"))
        {
            int dano = mov.instaKill ? 999 : danoDaEspada;

            VidaBoss scriptDoBoss =
                collision.GetComponent<VidaBoss>();

            if (scriptDoBoss != null)
            {
                scriptDoBoss.TomarDano(dano);
            }

            VidaInimigo scriptDoInimigo =
                collision.GetComponent<VidaInimigo>();

            if (scriptDoInimigo != null)
            {
                scriptDoInimigo.TomarDano(dano);
            }

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        
        if (!aguardandoDisparo)
        {
            Destroy(gameObject);
        }
    }
}
