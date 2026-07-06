using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para mexer com cenas!

public class GerenciadorDeCenas : MonoBehaviour
{
    // Criamos uma função pública para o botão conseguir "enxergar"
    public void VoltarParaMenu()
    {
        // O nome aqui deve ser EXATAMENTE igual ao nome do arquivo da sua cena de menu
        SceneManager.LoadScene("menu");
    }
}