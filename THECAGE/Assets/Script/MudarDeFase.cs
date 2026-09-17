using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarDeFase : MonoBehaviour
{
    public string nomeDaProximaCena = "Jogo";
    public GameObject textoAviso;
    private bool playerPerto = false;

    void Start()
    {
        if (textoAviso != null) textoAviso.SetActive(false);
    }

    void Update()
    {
       
        if (playerPerto && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)))
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