using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- Mantemos essa linha, ela cuida do Slider e do Text Legacy!

public class VidaPlayer : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaAtual;

    public Slider barraDeVida;
    public Text textoDeVida; // <-- MUDOU AQUI: Agora é apenas "Text" (Legacy)

    void Start()
    {
        vidaAtual = vidaMaxima;

        // Configura a barra
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;

        // Atualiza a barra visual
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();

        Debug.Log("Ai! O Player tomou dano. Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // Essa função cuida só de escrever o número na tela
    void AtualizarTexto()
    {
        if (textoDeVida != null)
        {
            textoDeVida.text = vidaAtual + " / " + vidaMaxima;
        }
    }

    void Morrer()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}