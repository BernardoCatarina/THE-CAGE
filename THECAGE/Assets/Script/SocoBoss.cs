using UnityEngine;

public class SocoBoss : MonoBehaviour
{
    public float velocidade = 5f;
    public int danoDoSoco = 1;

    public float tempoDeEspera = 1.5f;
    private float proximoDano = 0f;

    void Start()
    {
        float direcaoX = -1f; 

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            
            if (player.transform.position.x > transform.position.x)
            {
                direcaoX = 1f;

                
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.flipX = true; 
                }
                else
                {
                    
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
        }

       
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(direcaoX * velocidade, 0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= proximoDano)
        {
            VidaPlayer vidaDoPlayer = collision.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(danoDoSoco);
                proximoDano = Time.time + tempoDeEspera;
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}