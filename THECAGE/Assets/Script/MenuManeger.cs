using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private string nomedoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;

    [Header("Painéis do Menu")]
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos; 

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderEfeitos;

    void Start()
    {
        float volumeMusicaSalvo = PlayerPrefs.GetFloat("VolMusica", -10f);
        float volumeEfeitosSalvo = PlayerPrefs.GetFloat("VolEfeitos", -10f);

        if (sliderMusica != null) sliderMusica.value = volumeMusicaSalvo;
        if (sliderEfeitos != null) sliderEfeitos.value = volumeEfeitosSalvo;

        MudarVolumeMusica(volumeMusicaSalvo);
        MudarVolumeEfeitos(volumeEfeitosSalvo);
    }

    public void Jogar()
    {
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            transicao.IrParaProximaFase(nomedoLevelDeJogo);
        }
        else
        {
            SceneManager.LoadScene(nomedoLevelDeJogo);
        }
    }

    // ---------------- CONTROLE DAS OPÇÕES ----------------
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    // ---------------- CONTROLE DOS CRÉDITOS ----------------
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true); 
    }

    public void FecharCreditos()
    {
        painelCreditos.SetActive(false); 
        painelMenuInicial.SetActive(true);
    }

    // ---------------- SAIR DO JOGO ----------------
    public void SairJogo()
    {
        Debug.Log("Sair Do Jogo");
        Application.Quit();
    }

    // ---------------- FUNÇÕES DE ÁUDIO ----------------
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