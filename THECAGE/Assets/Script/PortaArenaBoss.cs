using UnityEngine;
using TMPro;

public class PortaArenaBoss : MonoBehaviour
{
    [Header("Configuração das Portas")]
    public GameObject portaFechada;
    public GameObject portaAberta;
    public BoxCollider2D travaDaArena;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip somAbrir;
    public AudioClip somFechar;

    [Header("UI de Interação")]
    public GameObject textoInteracao;

    [Header("Configuração do Boss")]
    public GameObject objetoBoss;
    public GameObject barraDeVidaBoss;

    private bool jogadorPerto = false;
    private bool portaEstaAberta = false;
    private bool bossAtivado = false;

    void Start()
    {
        if (textoInteracao != null) textoInteracao.SetActive(false);
        if (portaFechada != null) portaFechada.SetActive(true);
        if (portaAberta != null) portaAberta.SetActive(false);
        if (objetoBoss != null) objetoBoss.SetActive(false);
        if (barraDeVidaBoss != null) barraDeVidaBoss.SetActive(false);
        portaEstaAberta = false;

        
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (jogadorPerto && !bossAtivado)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetMouseButtonDown(1))
            {
                AlternarPorta();
            }
        }
    }

    void AlternarPorta()
    {
        portaEstaAberta = !portaEstaAberta;

        if (portaEstaAberta)
        {
            if (portaFechada != null) portaFechada.SetActive(false);
            if (portaAberta != null) portaAberta.SetActive(true);
            if (audioSource != null && somAbrir != null) audioSource.PlayOneShot(somAbrir);
        }
        else
        {
            if (portaFechada != null) portaFechada.SetActive(true);
            if (portaAberta != null) portaAberta.SetActive(false);
            if (audioSource != null && somFechar != null) audioSource.PlayOneShot(somFechar);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = true;
            if (textoInteracao != null && !bossAtivado) textoInteracao.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (textoInteracao != null) textoInteracao.SetActive(false);
        }
    }


    public void AtivarBoss()
    {
        if (bossAtivado) return;

        bossAtivado = true; // Impede que a porta seja aberta de novo no Update

        
        portaEstaAberta = false;
        if (portaFechada != null) portaFechada.SetActive(true);
        if (portaAberta != null) portaAberta.SetActive(false);
        if (textoInteracao != null) textoInteracao.SetActive(false);
        if (travaDaArena != null) travaDaArena.enabled = true;

        if (audioSource != null && somFechar != null) audioSource.PlayOneShot(somFechar);

        
        if (objetoBoss != null) objetoBoss.SetActive(true);
        if (barraDeVidaBoss != null) barraDeVidaBoss.SetActive(true);
    }
    public void BossDerrotado()
    {
        bossAtivado = false; 

        if (travaDaArena != null)
            travaDaArena.enabled = false; 

        if (!portaEstaAberta) AlternarPorta();
    }
}