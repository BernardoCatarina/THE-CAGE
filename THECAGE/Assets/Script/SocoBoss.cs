using UnityEngine;

public class SocoBoss : MonoBehaviour
{
    public float velocidade = 5f;
    public int danoDoSoco = 1;

    public float tempoDeEspera = 1.5f; 
    private float proximoDano = 0f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * velocidade;
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