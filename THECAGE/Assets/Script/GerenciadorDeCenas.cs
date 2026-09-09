using UnityEngine;
using UnityEngine.SceneManagement; 

public class GerenciadorDeCenas : MonoBehaviour
{
    
    public void VoltarParaMenu()
    {
        PlayerPrefs.DeleteKey("VidaSalva");
        PlayerPrefs.DeleteKey("MunicaoSalva");
        PlayerPrefs.DeleteKey("TemFacaSalva");
        PlayerPrefs.DeleteKey("TemEspadaSalva");

        SceneManager.LoadScene("menu");
    }
}