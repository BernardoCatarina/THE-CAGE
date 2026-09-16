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

        // --- DESLIGA OS CHEATS AQUI ---
        mov.instaKill = false;
        mov.modoVoo = false;
        mov.modoImortal = false;
        mov.modoMetralhadora = false;
        // ------------------------------

        
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            
            transicao.IrParaProximaFase("Fase");
        }
        else
        {
            
            SceneManager.LoadScene("Fase");
        }
    }
}