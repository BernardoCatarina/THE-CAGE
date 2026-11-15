using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 200;
    private int vidaAtual;
    public Slider barraDeVida;
    public GameObject prefabTextoFlutuante;

    private bool fase150Ativada = false;
    private bool fase100Ativada = false;
    private bool fase50Ativada = false;

    private BossIA scriptIA;

    void Start()
    {
        vidaAtual = vidaMaxima;
        scriptIA = GetComponent<BossIA>();

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    public void TomarDano(int quantidadeDeDano)
    {
        vidaAtual -= quantidadeDeDano;
        if (barraDeVida != null) barraDeVida.value = vidaAtual;

        if (prefabTextoFlutuante != null)
        {
            GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
            texto.GetComponent<TextMeshPro>().text = "-" + quantidadeDeDano;
            texto.GetComponent<TextMeshPro>().color = Color.red;
        }

        // --- GERENCIADOR DE FASES ---
        if (scriptIA != null)
        {
            // Fase 1: 150 de Vida (Invoca inimigos e pausa)
            if (vidaAtual <= 150 && vidaAtual > 100 && !fase150Ativada)
            {
                fase150Ativada = true;
                scriptIA.IniciarInvocacao();
            }
            // Fase 2: 100 de Vida (Teletransporte e Buff de velocidade)
            else if (vidaAtual <= 100 && vidaAtual > 50 && !fase100Ativada)
            {
                fase100Ativada = true;
                scriptIA.IniciarTeletransporte();
            }
            // Fase 3: 50 de Vida (Invoca inimigos de novo)
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

    void Morrer()
    {
        MemoriaDaFase memoria = GetComponent<MemoriaDaFase>();
        if (memoria != null) memoria.RegistrarMorteOuColeta();

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