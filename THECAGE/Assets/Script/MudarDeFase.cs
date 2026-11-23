using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarDeFase : MonoBehaviour
{
    public string nomeDaProximaCena = "Jogo";
    public GameObject textoAviso;
    private bool playerPerto = false;

    [Header("Áudio da Porta")]
    public AudioClip somPorta;
    private AudioSource audioSource;

    public string nomeDoPontoDeSpawnDestino;

    [Header("Tipo de Transição")]
    [Tooltip("Marque para usar o Fade Preto simples. Se desmarcado, usará o Mosaico padrão.")]
    public bool usarFadePreto = false;

    void Start()
    {
        if (textoAviso != null) textoAviso.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerPerto && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            if (somPorta != null && audioSource != null)
            {
                audioSource.PlayOneShot(somPorta);
            }

            PlayerPrefs.SetString("PortaDestino", nomeDoPontoDeSpawnDestino);

            
            if (usarFadePreto)
            {
                EfeitoFade fade = FindObjectOfType<EfeitoFade>();
                if (fade != null)
                {
                    
                    fade.IniciarFade(nomeDaProximaCena, Color.black, 1.5f);
                }
                else
                {
                    SceneManager.LoadScene(nomeDaProximaCena);
                }
            }
            else
            {
                
                ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

                if (transicao != null)
                {
                    transicao.IrParaProximaFase(nomeDaProximaCena);
                }
                else
                {
                    SceneManager.LoadScene(nomeDaProximaCena);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPerto = true;
            if (textoAviso != null) textoAviso.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPerto = false;
            if (textoAviso != null) textoAviso.SetActive(false);
        }
    }
}