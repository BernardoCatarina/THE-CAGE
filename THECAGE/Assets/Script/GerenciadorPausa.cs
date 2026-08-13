using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorPausa : MonoBehaviour
{
    public GameObject painelOpcoes; // Arraste o seu PainelOpcoes para cá depois

    // Função para abrir o painel e pausar o jogo
    public void AbrirOpcoes()
    {
        painelOpcoes.SetActive(true);
        Time.timeScale = 0f; // O tempo em 0 congela o jogo (pausa)
    }

    // Função para fechar o painel e despausar o jogo (Botão de Continuar, se você tiver)
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        Time.timeScale = 1f; // O tempo em 1 faz o jogo voltar ao normal
    }

    // Função para o botão "Voltar ao Menu"
    public void VoltarParaMenu()
    {
        Time.timeScale = 1f; // É MUITO importante voltar o tempo ao normal antes de mudar de cena!
        SceneManager.LoadScene("menu"); // O nome exato da sua cena de menu
    }
}
