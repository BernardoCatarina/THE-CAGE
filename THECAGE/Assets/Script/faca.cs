using UnityEngine;

public class faca : MonoBehaviour
{
    public float speed = 8f;
    // Nova variável para controlar a velocidade do giro
    public float velocidadeDeGiro = -720f;

    void Start()
    {
        // A faca voa para a direção que estiver "olhando"
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
    }

    void Update()
    {
        // Gira a faca constantemente no eixo Z (que é o eixo de rotação 2D)
        // Usamos Time.deltaTime para o giro ficar suave independente do FPS do jogo
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