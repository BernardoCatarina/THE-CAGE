using UnityEngine;
using UnityEngine.UI;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaAtual;
    public Slider barraDeVida;

    

    void Start()
    {
        

        vidaAtual = vidaMaxima;
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }

    }
}