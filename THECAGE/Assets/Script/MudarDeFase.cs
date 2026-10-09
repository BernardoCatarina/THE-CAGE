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

            ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

            if (transicao != null)
            {
                
                PlayerPrefs.SetString("PortaDestino", nomeDoPontoDeSpawnDestino);
                transicao.IrParaProximaFase(nomeDaProximaCena);
            }
            else
            {
               
                PlayerPrefs.SetString("PortaDestino", nomeDoPontoDeSpawnDestino);
                SceneManager.LoadScene(nomeDaProximaCena);
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