using UnityEngine;
using TMPro;

public class VidaMiniBoss : MonoBehaviour
{
    public int vidaMaxima = 25;
    private int vidaAtual;
    public GameObject prefabTextoFlutuante;

    [Header("Drops")]
    public GameObject prefabCoracao;
    public GameObject prefabMunicao;
    public int quantidadeDeDrops = 4; 
    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void TomarDano(int quantidade)
    {
        vidaAtual -= quantidade;

        if (prefabTextoFlutuante != null)
        {
            GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
            texto.GetComponent<TextMeshPro>().text = "-" + quantidade;
            texto.GetComponent<TextMeshPro>().color = Color.red;
        }

        if (vidaAtual <= 0)
        {
            SoltarRecompensas();
            Destroy(gameObject);
        }
    }

    void SoltarRecompensas()
    {
        for (int i = 0; i < quantidadeDeDrops; i++)
        {
            
            GameObject itemSorteado = Random.value > 0.5f ? prefabCoracao : prefabMunicao;

            if (itemSorteado != null)
            {
                
                Vector3 posicaoAleatoria = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0f, 1f), 0);
                Instantiate(itemSorteado, posicaoAleatoria, Quaternion.identity);
            }
        }
    }
}