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

        // --- DESLIGA OS CHEATS AQUI ---
        mov.instaKill = false;
        mov.modoVoo = false;
        mov.modoImortal = false;
        mov.modoMetralhadora = false;
        // ------------------------------

        
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            
            transicao.IrParaProximaFase("menu");
        }
        else
        {
            
            SceneManager.LoadScene("menu");
        }
    }
}