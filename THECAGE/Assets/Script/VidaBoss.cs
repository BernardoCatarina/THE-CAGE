using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- LINHA NOVA OBRIGATÓRIA

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 5;
    private int vidaAtual;

    public Slider barraDeVida; // <-- Cria o espaço para arrastar a barra

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    public void TomarDano(int quantidadeDeDano)
    {
        vidaAtual -= quantidadeDeDano;

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        Debug.Log("O Boss tomou dano! Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("O Boss morreu!");
        SceneManager.LoadScene("Vitoria");
    }
}