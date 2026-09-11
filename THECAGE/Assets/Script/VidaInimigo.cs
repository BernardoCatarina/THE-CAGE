using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaAtual;
    public Slider barraDeVida;

   
    public GameObject prefabTextoFlutuante;

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

        
        if (prefabTextoFlutuante != null)
        {
            GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
            texto.GetComponent<TextMeshPro>().text = "-" + quantidade;
            texto.GetComponent<TextMeshPro>().color = Color.red;
        }

        
        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }
}