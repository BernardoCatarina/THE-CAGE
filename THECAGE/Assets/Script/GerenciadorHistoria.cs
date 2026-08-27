using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GerenciadorHistoria : MonoBehaviour
{
    [Header("Elementos de UI")]
    public Image componenteImagem;
    public Text componenteTexto;
    public Text textoDoBotao; 

    [Header("Conteúdo da História")]
    public List<Sprite> imagensHistoria; 
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

        
        if (indiceAtual < imagensHistoria.Count && indiceAtual < textosHistoria.Count)
        {
            ExibirSlideAtual();
        }
        else
        {
            
            SceneManager.LoadScene("Fase");
        }
    }

    void ExibirSlideAtual()
    {
        
        componenteImagem.sprite = imagensHistoria[indiceAtual];
        componenteTexto.text = textosHistoria[indiceAtual];

        
        if (indiceAtual == imagensHistoria.Count - 1)
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