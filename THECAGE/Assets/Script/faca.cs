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
            VidaBoss scriptDoBoss = collision.GetComponent<VidaBoss>();

            if (scriptDoBoss != null)
            {
                scriptDoBoss.TomarDano(1);
            }

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}