using UnityEngine;

public class faca : MonoBehaviour
{

    public float speed = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Tenta encontrar o script de vida no Boss que a faca encostou
            VidaBoss scriptDoBoss = collision.GetComponent<VidaBoss>();

            // Se encontrou o script, aplica 1 de dano
            if (scriptDoBoss != null)
            {
                scriptDoBoss.TomarDano(1);
            }

            // A faca se destrói ao bater no inimigo, independente se ele morreu ou não
            Destroy(gameObject);
        }
    }
    
    }