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

    public Animator animator;

    private bool estaMorto = false;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Inimigo"), false);

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
        if (estaMorto || mov.modoImortal)
        {
            return;
        }

        vidaAtual -= quantidade;

        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        PlayerPrefs.SetInt("VidaSalva", vidaAtual);

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }
        AtualizarTexto();

        if (somDeDano != null && audioSource != null)
        {
            audioSource.PlayOneShot(somDeDano);
        }

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
        int layerPlayer = LayerMask.NameToLayer("Player");
        int layerInimigo = LayerMask.NameToLayer("Inimigo");

        Physics2D.IgnoreLayerCollision(layerPlayer, layerInimigo, true);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); 
            yield return new WaitForSeconds(0.15f);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }

        Physics2D.IgnoreLayerCollision(layerPlayer, layerInimigo, false);
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
        
        if (estaMorto) return;
        estaMorto = true;

        StartCoroutine(RotinaQuedaEMorte());
    }

    IEnumerator RotinaQuedaEMorte()
    {
        Debug.Log("Game Over!");

        if (GetComponent<mov>() != null)
        {
            GetComponent<mov>().enabled = false;
        }

     
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetTrigger("AnimacaoMorte");
        }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
  
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

             yield return new WaitForSeconds(0.1f);

            float tempoLimite = 3f; 
            float timer = 0f;

            while (Mathf.Abs(rb.linearVelocity.y) > 0.05f && timer < tempoLimite)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);

        EfeitoFade fade = FindObjectOfType<EfeitoFade>();
        if (fade != null)
        {
            Color vermelhoSangue = new Color(0.5f, 0f, 0f);
            fade.IniciarFade("GameOver", vermelhoSangue, 2.5f);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void Curar(int quantidade)
    {
        vidaAtual += quantidade;

       
        if (vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }

        
        PlayerPrefs.SetInt("VidaSalva", vidaAtual);

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();
    }
}