using UnityEngine;

public class SocoBoss : MonoBehaviour
{
    public float velocidade = 5f;
    public int danoDoSoco = 1;

    void Start()
    {
        // Como o Boss está na direita, o soco vai voar para a esquerda (Vector2.left)
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * velocidade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se bater no Player, dá dano
        if (collision.CompareTag("Player"))
        {
            VidaPlayer vidaDoPlayer = collision.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(danoDoSoco);
            }

            // Destrói o soco depois de acertar
            Destroy(gameObject);
        }
    }

    // Se o soco sair da tela e não acertar nada, ele se destrói sozinho
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}