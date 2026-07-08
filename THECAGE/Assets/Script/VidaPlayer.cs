using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- LINHA NOVA OBRIGATÓRIA: Permite usar UI (Slider, Text, etc)

public class VidaPlayer : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaAtual;

    public Slider barraDeVida; // <-- Cria o espaço para arrastar a barra na Unity

    void Start()
    {
        vidaAtual = vidaMaxima;

        // Configura a barra logo que o jogo começa
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;

        // Atualiza o desenho da barra
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        Debug.Log("Ai! O Player tomou dano. Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}