using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public int forcaDoDano = 1;

    private float tempoProximoDano = 0f;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TentarCausarDano(collision);
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        TentarCausarDano(collision);
    }

    
    private void TentarCausarDano(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= tempoProximoDano)
        {
            VidaPlayer vidaScript = collision.gameObject.GetComponent<VidaPlayer>();

            if (vidaScript != null)
            {
                
                vidaScript.TomarDano(forcaDoDano, transform.position);

                tempoProximoDano = Time.time + 1.5f;
            }
        }
    }
}