using UnityEngine;

public class SocoBoss : MonoBehaviour
{
    public float velocidade = 5f;
    public int danoDoSoco = 1;

    void Start()
    {
        
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * velocidade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            VidaPlayer vidaDoPlayer = collision.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(danoDoSoco);
            }

            
            Destroy(gameObject);
        }
    }

    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}