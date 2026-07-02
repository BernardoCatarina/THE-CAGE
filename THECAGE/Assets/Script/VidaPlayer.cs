using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public int vidaMaxima = 10; // Quantidade de acertos que o player aguenta
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
        // Por enquanto vamos só destruir o player, mas depois podemos 
        // fazer a tela de Game Over ou reiniciar a fase aqui!
        Destroy(gameObject);
    }
}