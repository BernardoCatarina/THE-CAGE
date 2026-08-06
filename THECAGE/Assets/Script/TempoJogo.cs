using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- MUDOU AQUI: Usamos a UI normal em vez do TMPro

public class TempoJogo : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float tempoTotal = 30f; // Vi que colocaste 30 segundos na Unity, perfeito!
    private float tempoRestante;
    private bool tempoAcabou = false;

    [Header("Interface de Usuário")]
    public Text textoTempo; // <-- MUDOU AQUI: Agora usa o Text Legacy

    void Start()
    {
        tempoRestante = tempoTotal;
    }

    void Update()
    {
        if (tempoAcabou) return;

        if (tempoRestante > 0)
        {
            // Subtrai o tempo passado a cada frame
            tempoRestante -= Time.deltaTime;
            AtualizarInterface();
        }
        else
        {
            // Tempo zerou!
            tempoRestante = 0;
            tempoAcabou = true;
            AtualizarInterface();
            GameOverPorTempo();
        }
    }

    void AtualizarInterface()
    {
        if (textoTempo != null)
        {
            // Converte segundos em minutos e segundos (ex: 00:30)
            int minutos = Mathf.FloorToInt(tempoRestante / 60);
            int segundos = Mathf.FloorToInt(tempoRestante % 60);

            textoTempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    void GameOverPorTempo()
    {
        Debug.Log("O tempo acabou! Volta para o Game Over.");
        SceneManager.LoadScene("GameOver");
    }
}