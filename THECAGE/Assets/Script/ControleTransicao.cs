using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControleTransicao : MonoBehaviour
{
    public Image telaPreta;
    public float tempoDeEfeito = 1.0f;
    public bool iniciarComEfeito = true;

    private Material materialAnimado;

    void Start()
    {
        
        Time.timeScale = 1f;

        Canvas canvas = telaPreta.canvas;
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;
        }

        materialAnimado = new Material(telaPreta.material);
        telaPreta.material = materialAnimado;

        if (iniciarComEfeito)
        {
            StartCoroutine(EfeitoMosaico(3.5f, -0.5f, ""));
        }
        else
        {
            telaPreta.gameObject.SetActive(false);
        }
    }

    public void IrParaProximaFase(string nomeDaCena)
    {
        
        Time.timeScale = 0f;
        StartCoroutine(EfeitoMosaico(-0.5f, 3.5f, nomeDaCena));
    }

    IEnumerator EfeitoMosaico(float inicio, float fim, string proximaCena)
    {
        telaPreta.gameObject.SetActive(true);

        float tempo = 0f;
        while (tempo < tempoDeEfeito)
        {
            tempo += Time.unscaledDeltaTime;
            float progresso = tempo / tempoDeEfeito;

            float valorAtual = Mathf.Lerp(inicio, fim, progresso);
            materialAnimado.SetFloat("_Cutoff", valorAtual);

            yield return null;
        }

        materialAnimado.SetFloat("_Cutoff", fim);

        if (!string.IsNullOrEmpty(proximaCena))
        {
            
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(proximaCena);
        }
        else
        {
            telaPreta.gameObject.SetActive(false);
        }
    }
}