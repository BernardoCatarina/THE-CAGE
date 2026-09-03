using UnityEngine;

public class InteragirPlaca : MonoBehaviour
{
    public GameObject textoAviso;
    public GameObject menuComandos;
    private bool playerPerto = false;

    void Start()
    {
        if (textoAviso != null) textoAviso.SetActive(false);
        if (menuComandos != null) menuComandos.SetActive(false);
    }

    void Update()
    {
        if (playerPerto && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)))
        {
            
            bool vaiAbrir = !menuComandos.activeSelf;

            menuComandos.SetActive(vaiAbrir);

            
            Time.timeScale = vaiAbrir ? 0f : 1f;
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

            
            if (menuComandos != null && menuComandos.activeSelf)
            {
                menuComandos.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void FecharMenu()
    {
        menuComandos.SetActive(false);

        
        Time.timeScale = 1f;
    }
}