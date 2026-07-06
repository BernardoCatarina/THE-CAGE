using UnityEngine;
using UnityEngine.SceneManagement; // <-- LINHA OBRIGATÓRIA: Adicione isso no topo!

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 50;
    private int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void TomarDano(int quantidadeDeDano)
    {
        vidaAtual -= quantidadeDeDano;
        Debug.Log("O Boss tomou dano! Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("O Boss morreu!");
        // Carrega a cena de Vitória automaticamente quando o boss perde toda a vida
        SceneManager.LoadScene("Vitoria");
    }
}