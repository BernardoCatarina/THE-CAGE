using System.Collections; // <-- OBRIGATÓRIO PARA A COROUTINE (PISCAR) FUNCIONAR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour 
{
    public int vidaMaxima = 3;
    private int vidaAtual;

    public Slider barraDeVida;
    public Text textoDeVida;

    // --- VARIÁVEIS NOVAS PARA O PISCAR ---
    private SpriteRenderer spriteRenderer;
    public int quantidadeDePiscadas = 3; // Quantas vezes ele vai piscar
    public float tempoPiscada = 0.1f;    // A velocidade da piscada

    void Start()
    {
        vidaAtual = vidaMaxima;

        // Pega o componente que desenha o player na tela
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
        vidaAtual -= quantidade;

        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
        }

        AtualizarTexto();

        // --- COMEÇA O EFEITO VISUAL ---
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

    // --- NOVA FUNÇÃO: O TEMPORIZADOR DE PISCAR ---
    IEnumerator EfeitoPiscar()
    {
        // Vai repetir esse bloco de acordo com a quantidade de piscadas
        for (int i = 0; i < quantidadeDePiscadas; i++)
        {
            spriteRenderer.enabled = false; // Fica invisível
            yield return new WaitForSeconds(tempoPiscada); // Espera uma fração de segundo

            spriteRenderer.enabled = true; // Volta a ficar visível
            yield return new WaitForSeconds(tempoPiscada); // Espera de novo
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