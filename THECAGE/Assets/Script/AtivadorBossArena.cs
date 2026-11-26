using UnityEngine;

public class AtivadorBossArena : MonoBehaviour
{
    [Header("Portas para Trancar")]
    public PortaArenaBoss portaEsquerda;
    public PortaArenaBoss portaDireita;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            if (portaEsquerda != null)
            {
                portaEsquerda.AtivarBoss();
            }

            
            if (portaDireita != null)
            {
                portaDireita.AtivarBoss();
            }

            
            gameObject.SetActive(false);
        }
    }
}