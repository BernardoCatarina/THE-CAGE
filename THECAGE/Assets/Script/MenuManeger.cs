using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;

    public void Jogar()
    {
        
        ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

        if (transicao != null)
        {
            
            transicao.IrParaProximaFase(nomeDoLevelDeJogo);
        }
        else
        {
            
            SceneManager.LoadScene(nomeDoLevelDeJogo);
        }
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair Do Jogo");
        Application.Quit();
    }
}