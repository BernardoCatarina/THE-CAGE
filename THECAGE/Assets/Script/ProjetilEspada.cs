using UnityEngine;

public class ProjetilEspada : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public float speed = 8f;
    public int danoDaEspada = 5;

    [HideInInspector] public bool disparado = false; 

    void Start()
    {
        
    }

   
    public void Lancar()
    {
        disparado = true;
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!disparado) return;

        if (collision.CompareTag("Enemy"))
        {
            int dano = mov.instaKill ? 999 : danoDaEspada;

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