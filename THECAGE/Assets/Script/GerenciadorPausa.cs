using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorPausa : MonoBehaviour
{
    public GameObject painelOpcoes;
    public Slider sliderVolume;

    void Start()
    {
        if (sliderVolume != null)
        {
            sliderVolume.value = AudioListener.volume;
        }
    }

    public void AbrirOpcoes()
    {
        painelOpcoes.SetActive(true);
        Time.timeScale = 0f;
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        Time.timeScale = 1f;
    }

    public void VoltarParaMenu()
    {
       
        Time.timeScale = 1f;

       
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            
            transicao.IrParaProximaFase("menu");
        }
        else
        {
            
            SceneManager.LoadScene("menu");
        }
    }

    public void AlterarVolume(float valor)
    {
        AudioListener.volume = valor;
    }
}