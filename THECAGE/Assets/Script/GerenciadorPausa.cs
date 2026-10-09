using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GerenciadorPausa : MonoBehaviour
{
    public GameObject painelOpcoes;

    [Header("Configurações de Áudio Integradas")]
    public AudioMixer audioMixer;
    public Slider sliderMusica;
    public Slider sliderEfeitos;
    public GameObject primeiroBotao; // O botão que o controle seleciona primeiro (Ex: Botão "Continuar")
    private bool jogoPausado = false;

    void Start()
    {
        
        float volumeMusicaSalvo = PlayerPrefs.GetFloat("VolMusica", 1f);
        float volumeEfeitosSalvo = PlayerPrefs.GetFloat("VolEfeitos", 1f);

        if (sliderMusica != null) sliderMusica.value = volumeMusicaSalvo;
        if (sliderEfeitos != null) sliderEfeitos.value = volumeEfeitosSalvo;

        MudarVolumeMusica(volumeMusicaSalvo);
        MudarVolumeEfeitos(volumeEfeitosSalvo);
    }

    void Update()
    {
       
        if (Input.GetButtonDown("Pause"))
        {
            if (jogoPausado)
            {
                FecharOpcoes();
            }
            else
            {
                AbrirOpcoes();
            }
        }

        if (jogoPausado)
        {
           
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f)
            {
                Cursor.visible = true; 
            }
           
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                Cursor.visible = false; 
                                
                if (EventSystem.current.currentSelectedGameObject == null && primeiroBotao != null)
                {
                    EventSystem.current.SetSelectedGameObject(primeiroBotao);
                }
            }
        }
    }
    public void AbrirOpcoes()
    {
        painelOpcoes.SetActive(true);
        Time.timeScale = 0f;
        jogoPausado = true;

        EventSystem.current.SetSelectedGameObject(null);
       
        if (primeiroBotao != null)
        {
            EventSystem.current.SetSelectedGameObject(primeiroBotao);
        }
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    // ---------------- FUNÇÕES DE ÁUDIO CORRIGIDAS ----------------
    public void MudarVolumeMusica(float volume)
    {
        if (audioMixer != null)
        {
            
            if (volume <= 0f) volume = 0.0001f;

            float decibeis = Mathf.Log10(volume) * 20f;
            audioMixer.SetFloat("VolMusica", decibeis);
            PlayerPrefs.SetFloat("VolMusica", volume);
        }
    }

    public void MudarVolumeEfeitos(float volume)
    {
        if (audioMixer != null)
        {
            
            if (volume <= 0f) volume = 0.0001f;

            float decibeis = Mathf.Log10(volume) * 20f;
            audioMixer.SetFloat("VolEfeitos", decibeis);
            PlayerPrefs.SetFloat("VolEfeitos", volume);
        }
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
}