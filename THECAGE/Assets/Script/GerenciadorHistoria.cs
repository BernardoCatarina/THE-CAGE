using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GerenciadorHistoria : MonoBehaviour
{
    [Header("Elementos de UI")]
    public Image componenteImagem;
    public Text componenteTexto;
    public Text textoDoBotao; // Para mudar "Próximo" para "Jogar!" no final

    [Header("Conteúdo da História")]
    public List<Sprite> imagensHistoria; // Suas imagens em pixel art
    [TextArea(3, 5)]
    public List<string> textosHistoria; // Os textos correspondentes do seu GDD

    private int indiceAtual = 0;

    void Start()
    {
        // Começa mostrando o primeiro slide da história
        ExibirSlideAtual();
    }

    public void AvanarSlide()
    {
        indiceAtual++;

        // Se ainda houver slides para mostrar
        if (indiceAtual < imagensHistoria.Count && indiceAtual < textosHistoria.Count)
        {
            ExibirSlideAtual();
        }
        else
        {
            // Quando os slides acabarem, entra na fase do jogo!
            SceneManager.LoadScene("Jogo");
        }
    }

    void ExibirSlideAtual()
    {
        // Atualiza a imagem e o texto
        componenteImagem.sprite = imagensHistoria[indiceAtual];
        componenteTexto.text = textosHistoria[indiceAtual];

        // Se estiver no último slide (ex: "Você está pronto?")
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