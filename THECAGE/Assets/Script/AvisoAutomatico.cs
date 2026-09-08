using UnityEngine;

public class AvisoAutomatico : MonoBehaviour
{
    public GameObject textoPerigo;

    void Start()
    {
        
        if (textoPerigo != null) textoPerigo.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (textoPerigo != null) textoPerigo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (textoPerigo != null) textoPerigo.SetActive(false);
        }
    }
}