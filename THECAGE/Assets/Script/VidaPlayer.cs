using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour 
{

    [Header("Sons")]
    public AudioClip somDeDano; 
    private AudioSource audioSource; 

    public int vidaMaxima = 3;
    private int vidaAtual;

    public Slider barraDeVida;
    public Text textoDeVida;

   
    private SpriteRenderer spriteRenderer;
    public int quantidadeDePiscadas = 3; 
    public float tempoPiscada = 0.1f;    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        vidaAtual = PlayerPrefs.GetInt("VidaSalva", vidaMaxima);

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();
    }

    public void TomarDano(int quantidade)
    {
        PlayerPrefs.SetInt("VidaSalva", vidaAtual);

        if (somDeDano != null && audioSource != null)
        {
            audioSource.PlayOneShot(somDeDano);
        }

        vidaAtual -= quantidade;

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();

       
        if (spriteRenderer != null)
        {
            StartCoroutine(EfeitoPiscar());
        }

        Debug.Log("Ai! O Player tomou dano. Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    
    IEnumerator EfeitoPiscar()
    {
        
        for (int i = 0; i < quantidadeDePiscadas; i++)
        {
            spriteRenderer.enabled = false; 
            yield return new WaitForSeconds(tempoPiscada); 

            spriteRenderer.enabled = true; 
            yield return new WaitForSeconds(tempoPiscada); 
        }
    }

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