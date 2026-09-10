using UnityEngine;

public class faca : MonoBehaviour
{
    public float speed = 8f;
    
    public float velocidadeDeGiro = -720f;

    void Start()
    {
        
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
    }

    void Update()
    {
        
        transform.Rotate(0f, 0f, velocidadeDeGiro * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            int dano = mov.instaKill ? 9999 : 1;

            VidaBoss scriptDoBoss = collision.GetComponent<VidaBoss>();
            if (scriptDoBoss != null)
            {
                scriptDoBoss.TomarDano(dano);
            }

            VidaInimigo scriptDoInimigo = collision.GetComponent<VidaInimigo>();
            if (scriptDoInimigo != null)
            {
                scriptDoInimigo.TomarDano(dano);
            }

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}