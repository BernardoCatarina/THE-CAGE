using UnityEngine;

public class DanoEspinhos : MonoBehaviour
{
    public int quantidadeDeDano = 1; // Você pode mudar isso no Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem encostou nos espinhos foi o Player
        if (collision.CompareTag("Player"))
        {
            // Puxa o seu script de vida que já fizemos antes
            VidaPlayer vidaDoPlayer = collision.GetComponent<VidaPlayer>();

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(quantidadeDeDano);
            }
        }
    }
}