using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importante para usar o TextMeshPro!

public class TempoJogo : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float tempoTotal = 60f; // Tempo em segundos (ex: 60 segundos)
    private float tempoRestante;
    private bool tempoAcabou = false;

    [Header("Interface de Usuário")]
    public TextMeshProUGUI textoTempo; // Arraste o seu TextoTempo aqui na Unity

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
            // Converte segundos em minutos e segundos (ex: 01:30)
            int minutos = Mathf.FloorToInt(tempoRestante / 60);
            int segundos = Mathf.FloorToInt(tempoRestante % 60);

            textoTempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    void GameOverPorTempo()
    {
        Debug.Log("O tempo acabou! Vota para o Game Over.");
        SceneManager.LoadScene("GameOver");
    }
}