using UnityEngine;
using UnityEngine.SceneManagement;

public class cenaparaoFase : MonoBehaviour
{
    public void VoltarParaoFase()
    {
        // Desliga cheats
        mov.instaKill = false;
        mov.modoVoo = false;
        mov.modoImortal = false;
        mov.modoMetralhadora = false;

        string cenaDestino = PlayerPrefs.GetString("FaseSalva", "Fase");

        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            transicao.IrParaProximaFase(cenaDestino);
        }
        else
        {
            SceneManager.LoadScene(cenaDestino); 
        }
    }
}