using UnityEngine;
using System.Collections;
using TMPro; 

public class TutorialFade : MonoBehaviour
{
    public SpriteRenderer[] imagensDasTeclas;
    public TMP_Text[] textosTutorial; 
    public float velocidadeFade = 3f;

    void Start()
    {
        
        foreach (SpriteRenderer img in imagensDasTeclas)
        {
            if (img != null)
            {
                Color corInicial = img.color;
                corInicial.a = 0f;
                img.color = corInicial;
            }
        }

        
        foreach (TMP_Text txt in textosTutorial)
        {
            if (txt != null)
            {
                Color corInicial = txt.color;
                corInicial.a = 0f;
                txt.color = corInicial;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(EfeitoFade(1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(EfeitoFade(0f));
        }
    }

    IEnumerator EfeitoFade(float alphaAlvo)
    {
        
        float alphaAtual = 0f;
        if (imagensDasTeclas.Length > 0 && imagensDasTeclas[0] != null)
            alphaAtual = imagensDasTeclas[0].color.a;
        else if (textosTutorial.Length > 0 && textosTutorial[0] != null)
            alphaAtual = textosTutorial[0].color.a;

       
        while (Mathf.Abs(alphaAtual - alphaAlvo) > 0.01f)
        {
            alphaAtual = Mathf.MoveTowards(alphaAtual, alphaAlvo, velocidadeFade * Time.deltaTime);

            foreach (SpriteRenderer img in imagensDasTeclas)
            {
                if (img != null)
                {
                    Color cor = img.color;
                    cor.a = alphaAtual;
                    img.color = cor;
                }
            }

            foreach (TMP_Text txt in textosTutorial)
            {
                if (txt != null)
                {
                    Color cor = txt.color;
                    cor.a = alphaAtual;
                    txt.color = cor;
                }
            }

            yield return null;
        }
    }
}