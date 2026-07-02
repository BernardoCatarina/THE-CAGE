using UnityEngine;

public class AtaqueBoss : MonoBehaviour
{
    public int danoDoAtaque = 1;

    // Usamos OnCollisionEnter2D para dano de contato físico
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Verifica se quem esbarrou no boss foi o Player
        if (colisao.gameObject.CompareTag("Player"))
        {
            // Procura o script de vida no player
            VidaPlayer vidaDoPlayer = colisao.gameObject.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(danoDoAtaque);
            }
        }
    }
}