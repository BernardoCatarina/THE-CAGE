using UnityEngine;

public class AtaqueBoss : MonoBehaviour
{
    public int danoDoAtaque = 1;

    
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        
        if (colisao.gameObject.CompareTag("Player"))
        {
            
            VidaPlayer vidaDoPlayer = colisao.gameObject.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(danoDoAtaque);
            }
        }
    }
}