using UnityEngine;
using UnityEngine.SceneManagement; 

public class MudarDeFase : MonoBehaviour
{
    [Header("Nome da cena que vai carregar")]
    public string nomeDaProximaCena = "Jogo";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(nomeDaProximaCena);
        }
    }
}