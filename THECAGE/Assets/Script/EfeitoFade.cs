using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EfeitoFade : MonoBehaviour
{
    public Image telaDeCor;

    void Start()
    {
        
        Canvas canvas = telaDeCor.canvas;
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 105;
        }

       
        telaDeCor.gameObject.SetActive(false);
    }

    
    public void IniciarFade(string nomeDaProximaCena, Color corDoEfeito, float duracaoDoEfeito)
    {
        StartCoroutine(RotinaFade(nomeDaProximaCena, corDoEfeito, duracaoDoEfeito));
    }

    IEnumerator RotinaFade(string cena, Color cor, float tempoTotal)
    {
        telaDeCor.gameObject.SetActive(true);

       
        telaDeCor.color = new Color(cor.r, cor.g, cor.b, 0f);

        float tempo = 0f;
        while (tempo < tempoTotal)
        {
            tempo += Time.unscaledDeltaTime;
            float transparencia = tempo / tempoTotal;

           
            telaDeCor.color = new Color(cor.r, cor.g, cor.b, transparencia);
            yield return null;
        }

       
        telaDeCor.color = new Color(cor.r, cor.g, cor.b, 1f);

       
        SceneManager.LoadScene(cena);
    }
}