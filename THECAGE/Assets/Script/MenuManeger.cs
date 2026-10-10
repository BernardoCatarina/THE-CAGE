using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

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

    [Header("Botões para o Controle")]
    [SerializeField] private GameObject primeiroBotaoOpcoes;
    [SerializeField] private GameObject primeiroBotaoCreditos;
    [SerializeField] private GameObject botaoOpcoesNoMenu;
    [SerializeField] private GameObject botaoCreditosNoMenu;
    [SerializeField] private GameObject botaoJogarInicial;

    private Vector3 ultimaPosicaoMouse;

    void Start()
    {
        float volumeMusicaSalvo = PlayerPrefs.GetFloat("VolMusica", -10f);
        float volumeEfeitosSalvo = PlayerPrefs.GetFloat("VolEfeitos", -10f);

        if (sliderMusica != null) sliderMusica.value = volumeMusicaSalvo;
        if (sliderEfeitos != null) sliderEfeitos.value = volumeEfeitosSalvo;

        MudarVolumeMusica(volumeMusicaSalvo);
        MudarVolumeEfeitos(volumeEfeitosSalvo);

        // Salva onde o mouse está no começo
        ultimaPosicaoMouse = Input.mousePosition;

        // Foca no botão Jogar ao iniciar a cena
        StartCoroutine(SelecionarComAtraso(botaoJogarInicial));
    }

    void Update()
    {
        // 1. LÓGICA DO SENSOR DO MOUSE
        bool mouseMoveuDeVerdade = false;

        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            
            mouseMoveuDeVerdade = Mathf.Abs(Input.GetAxis("Mouse X")) > 1.5f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 1.5f;
        }
        else
        {
           
            mouseMoveuDeVerdade = (Input.mousePosition - ultimaPosicaoMouse).sqrMagnitude > 2.0f;
        }

        // 2. MOUSE NO COMANDO
        if (mouseMoveuDeVerdade || Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }

        // 3. CONTROLE NO COMANDO
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;

            
            if (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
            {
                if (painelOpcoes != null && painelOpcoes.activeInHierarchy)
                    EventSystem.current.SetSelectedGameObject(primeiroBotaoOpcoes);

                else if (painelCreditos != null && painelCreditos.activeInHierarchy)
                    EventSystem.current.SetSelectedGameObject(primeiroBotaoCreditos);

                else if (botaoJogarInicial != null)
                    EventSystem.current.SetSelectedGameObject(botaoJogarInicial);
            }
        }

        
        ultimaPosicaoMouse = Input.mousePosition;
    }

    public void Jogar()
    {
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null) transicao.IrParaProximaFase(nomedoLevelDeJogo);
        else SceneManager.LoadScene(nomedoLevelDeJogo);
    }

    // -------------- NAVEGAÇÃO DE MENUS (COM COROUTINES) --------------

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
        StartCoroutine(SelecionarComAtraso(primeiroBotaoOpcoes));
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        StartCoroutine(SelecionarComAtraso(botaoOpcoesNoMenu));
    }

    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
        StartCoroutine(SelecionarComAtraso(primeiroBotaoCreditos));
    }

    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);
        StartCoroutine(SelecionarComAtraso(botaoCreditosNoMenu));
    }
        
    private IEnumerator SelecionarComAtraso(GameObject botao)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        if (botao != null && botao.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(botao);
        }
    }

    // -------------- SAIR DO JOGO --------------
    public void SairJogo()
    {
        Debug.Log("Sair Do Jogo");
        Application.Quit();
    }

    // -------------- FUNÇÕES DE ÁUDIO --------------
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
}