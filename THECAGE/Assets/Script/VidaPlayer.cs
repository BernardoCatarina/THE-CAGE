using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // <-- LINHA NOVA: Necessária para usar o TextMeshPro!

public class VidaPlayer : MonoBehaviour
{
    public int vidaMaxima = 10;
    private int vidaAtual;

    public Slider barraDeVida;
    public TextMeshProUGUI textoDeVida; // <-- LINHA NOVA: Cria o espaço para o texto

    void Start()
    {
        vidaAtual = vidaMaxima;

        // Configura a barra
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto(); // <-- Atualiza o número logo que o jogo começa
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;

        // Atualiza a barra visual
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto(); // <-- Atualiza o número sempre que tomar dano

        Debug.Log("Ai! O Player tomou dano. Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // --- FUNÇÃO NOVA ---
    // Essa função cuida só de escrever o número na tela
    void AtualizarTexto()
    {
        if (textoDeVida != null)
        {
            // Vai mostrar no formato "3 / 3", "2 / 3", etc.
            textoDeVida.text = vidaAtual + " / " + vidaMaxima;
        }
    }

    void Morrer()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}