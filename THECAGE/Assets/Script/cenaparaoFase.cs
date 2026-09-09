using UnityEngine;
using UnityEngine.SceneManagement;

public class cenaparaoFase : MonoBehaviour
{
    public void VoltarParaoFase()
    {
        
        PlayerPrefs.DeleteKey("VidaSalva");
        PlayerPrefs.DeleteKey("MunicaoSalva");
        PlayerPrefs.DeleteKey("TemFacaSalva");
        PlayerPrefs.DeleteKey("TemEspadaSalva");


        SceneManager.LoadScene("Fase");
    }
}