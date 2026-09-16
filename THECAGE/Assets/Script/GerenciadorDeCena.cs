using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GerenciadorDeCena : MonoBehaviour
{
    public Animator transicaoAnim;
    public float tempoDeTransicao = 1f;

    
    public void CarregarProximaFase(string nomeDaCena)
    {
        StartCoroutine(Transicao(nomeDaCena));
    }

    IEnumerator Transicao(string nomeDaCena)
    {
        
        transicaoAnim.SetTrigger("Saida");

       
        yield return new WaitForSeconds(tempoDeTransicao);

       
        SceneManager.LoadScene(nomeDaCena);
    }
}