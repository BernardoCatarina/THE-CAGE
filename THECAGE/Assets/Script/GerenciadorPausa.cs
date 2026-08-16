using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <-- LINHA OBRIGATÓRIA PARA USAR O SLIDER

public class GerenciadorPausa : MonoBehaviour
{
    public GameObject painelOpcoes;
    public Slider sliderVolume; // <-- NOVA VARIÁVEL PARA O SLIDER

    void Start()
    {
        // Se o slider existir, ele vai começar na mesma posição do volume atual do jogo
        if (sliderVolume != null)
        {
            sliderVolume.value = AudioListener.volume;
        }
    }

    public void AbrirOpcoes()
    {
        painelOpcoes.SetActive(true);
        Time.timeScale = 0f; // Pausa
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        Time.timeScale = 1f; // Despausa
    }

    public void VoltarParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    // --- NOVA FUNÇÃO PARA O VOLUME ---
    // Repare que essa função pede um "(float valor)". O Slider vai enviar esse valor automaticamente!
    public void AlterarVolume(float valor)
    {
        AudioListener.volume = valor;
    }
}