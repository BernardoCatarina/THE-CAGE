using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio; 

public class GerenciadorPausa : MonoBehaviour
{
    public GameObject painelOpcoes;

    [Header("Configurações de Áudio Integradas")]
    public AudioMixer audioMixer; 
    public Slider sliderMusica;
    public Slider sliderEfeitos;

    void Start()
    {
        
        float volumeMusicaSalvo = PlayerPrefs.GetFloat("VolMusica", -10f);
        float volumeEfeitosSalvo = PlayerPrefs.GetFloat("VolEfeitos", -10f);

        if (sliderMusica != null) sliderMusica.value = volumeMusicaSalvo;
        if (sliderEfeitos != null) sliderEfeitos.value = volumeEfeitosSalvo;


        MudarVolumeMusica(volumeMusicaSalvo);
        MudarVolumeEfeitos(volumeEfeitosSalvo);
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

    // ---------------- FUNÇÕES DO VOLUME (IDÊNTICAS AS DO MENU) ----------------
    public void MudarVolumeMusica(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolMusica", volume);
            PlayerPrefs.SetFloat("VolMusica", volume); 
        }
    }

    public void MudarVolumeEfeitos(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolEfeitos", volume);
            PlayerPrefs.SetFloat("VolEfeitos", volume); 
        }
    }
}