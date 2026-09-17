using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 5;
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

    public void TomarDano(int quantidadeDeDano)
    {
        vidaAtual -= quantidadeDeDano;

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        Debug.Log("O Boss tomou dano! Vida restante: " + vidaAtual);

       
        if (prefabTextoFlutuante != null)
        {
            GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
            texto.GetComponent<TextMeshPro>().text = "-" + quantidadeDeDano;
            texto.GetComponent<TextMeshPro>().color = Color.red;
        }
        

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("O Boss morreu!");

        
        EfeitoFade fade = FindObjectOfType<EfeitoFade>();

        if (fade != null)
        {
            
            fade.IniciarFade("Vitoria", Color.white, 1.5f);
        }
        else
        {
            
            SceneManager.LoadScene("Vitoria");
        }
    }
}