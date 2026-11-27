using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 200;
    private int vidaAtual;
    public Slider barraDeVida;
    public GameObject prefabTextoFlutuante;

    [Header("Escudo Reativo")]
    public GameObject visualEscudo;

    private bool fase150Ativada = false;
    private bool fase100Ativada = false;
    private bool fase50Ativada = false;

    private BossIA scriptIA;

    [Header("Loot do Boss")]
    public GameObject prefabCinturao;
    public PortaArenaBoss[] scriptsDasPortas;

    [Header("Áudio e Efeitos")]
    public GameObject prefabEfeitoMorte;
    public AudioClip somMorte;
    public AudioClip somDano; 
    private AudioSource audioSource; 

    void Start()
    {
        vidaAtual = vidaMaxima;
        scriptIA = GetComponent<BossIA>();
        audioSource = GetComponent<AudioSource>(); 

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
        if (visualEscudo != null) visualEscudo.SetActive(false);
    }

    public void TomarDano(int quantidadeDeDano)
    {
        if (scriptIA != null && scriptIA.escudoAtivo)
        {
            StartCoroutine(PiscarEscudo(0.2f));
            return;
        }

        
        vidaAtual -= quantidadeDeDano;
        if (barraDeVida != null) barraDeVida.value = vidaAtual;

        
        if (somDano != null && audioSource != null)
        {
            audioSource.PlayOneShot(somDano);
        }

        if (prefabTextoFlutuante != null)
        {
            GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
            texto.GetComponent<TextMeshPro>().text = "-" + quantidadeDeDano;
            texto.GetComponent<TextMeshPro>().color = Color.red;
        }

        if (scriptIA != null)
        {
            if (vidaAtual <= 150 && vidaAtual > 100 && !fase150Ativada)
            {
                fase150Ativada = true;
                scriptIA.IniciarInvocacao();
            }
            else if (vidaAtual <= 100 && vidaAtual > 50 && !fase100Ativada)
            {
                fase100Ativada = true;
                scriptIA.IniciarTeletransporte();
            }
            else if (vidaAtual <= 50 && vidaAtual > 0 && !fase50Ativada)
            {
                fase50Ativada = true;
                scriptIA.IniciarInvocacao();
            }
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    public IEnumerator PiscarEscudo(float tempo)
    {
        if (visualEscudo != null)
        {
            visualEscudo.SetActive(true);
            yield return new WaitForSeconds(tempo);
            visualEscudo.SetActive(false);
        }
    }

    void Morrer()
    {
        MemoriaDaFase memoria = GetComponent<MemoriaDaFase>();
        if (memoria != null) memoria.RegistrarMorteOuColeta();

        if (prefabEfeitoMorte != null)
        {
            Instantiate(prefabEfeitoMorte, transform.position, Quaternion.identity);
        }

        if (somMorte != null)
        {
            AudioSource.PlayClipAtPoint(somMorte, Camera.main.transform.position);
        }

        if (prefabCinturao != null)
        {
            Instantiate(prefabCinturao, transform.position, Quaternion.identity);
        }

        if (scriptsDasPortas != null)
        {
            foreach (PortaArenaBoss porta in scriptsDasPortas)
            {
                if (porta != null) porta.BossDerrotado();
            }
        }

        Destroy(gameObject);
    }
}