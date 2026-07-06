using UnityEngine;
using UnityEngine.SceneManagement; // <-- LINHA OBRIGATÓRIA: Adicione isso no topo!

public class VidaPlayer : MonoBehaviour
{
    public int vidaMaxima = 10;
    private int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;
        Debug.Log("Ai! O Player tomou dano. Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Game Over!");
        // Carrega a cena de Game Over automaticamente quando o player morre
        SceneManager.LoadScene("GameOver");
    }
}