using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GerenciadorHistoria : MonoBehaviour
{
    [Header("Elementos de UI")]
    public Text componenteTexto;
    public Text textoDoBotao;

    [Header("Conteúdo da História")]
    [TextArea(3, 5)]
    public List<string> textosHistoria;

    private int indiceAtual = 0;

    void Start()
    {
        ExibirSlideAtual();
    }

    public void AvanarSlide()
    {
        indiceAtual++;

        if (indiceAtual < textosHistoria.Count)
        {
            ExibirSlideAtual();
        }
        else
        {
            
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

    void ExibirSlideAtual()
    {
        componenteTexto.text = textosHistoria[indiceAtual];

        if (indiceAtual == textosHistoria.Count - 1)
        {
            if (textoDoBotao != null)
            {
                textoDoBotao.text = "JOGAR!";
            }
        }
        else
        {
            if (textoDoBotao != null)
            {
                textoDoBotao.text = "Próximo >";
            }
        }
    }
}