using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class TempoJogo : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float tempoTotal = 30f; 
    private float tempoRestante;
    private bool tempoAcabou = false;

    [Header("Interface de Usuário")]
    public Text textoTempo; 

    void Start()
    {
        tempoRestante = tempoTotal;
    }

    void Update()
    {
        if (tempoAcabou) return;

        if (tempoRestante > 0)
        {
            
            tempoRestante -= Time.deltaTime;
            AtualizarInterface();
        }
        else
        {
            
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