using UnityEngine;
using UnityEngine.SceneManagement; 

public class GerenciadorDeCenas : MonoBehaviour
{
    
    public void VoltarParaMenu()
    {
        
        SceneManager.LoadScene("menu");
    }
}