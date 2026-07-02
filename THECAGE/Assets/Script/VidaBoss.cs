using UnityEngine;

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 50; // Quantidade de facadas que o Boss aguenta
    private int vidaAtual;

    void Start()
    {
        // Quando o jogo começa, a vida atual é igual a vida máxima
        vidaAtual = vidaMaxima;
    }

    // Método que será chamado quando a faca acertar ele
    public void TomarDano(int quantidadeDeDano)
    {
        vidaAtual -= quantidadeDeDano;
        Debug.Log("O Boss tomou dano! Vida restante: " + vidaAtual);

        // Verifica se a vida zerou
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        // Por enquanto, apenas destrói o Boss. 
        // No futuro, você pode colocar uma animação de morte aqui!
        Destroy(gameObject);
    }
}