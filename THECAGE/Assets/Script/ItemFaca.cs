using UnityEngine;
using TMPro;

public class ItemFaca : MonoBehaviour
{
    public int quantidadeParaDar = 100;

    [Header("Efeito Flutuante")]
    public float velocidadeFlutuacao = 2f;
    public float alturaFlutuacao = 0.3f;

    private Vector3 posicaoInicial;
    public GameObject prefabTextoFlutuante;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
       
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velocidadeFlutuacao) * alturaFlutuacao;
        transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            mov scriptDoPlayer = collision.GetComponent<mov>();
            if (scriptDoPlayer != null)
            {
                
                scriptDoPlayer.ColetarFacas(quantidadeParaDar);
            }

            
            if (prefabTextoFlutuante != null)
            {
               
                Vector3 posicaoTexto = transform.position + new Vector3(0, 0.5f, 0);
                GameObject texto = Instantiate(prefabTextoFlutuante, posicaoTexto, Quaternion.identity);

                TextMeshPro tmpro = texto.GetComponent<TextMeshPro>();
                if (tmpro != null)
                {
                    
                    tmpro.text = "+" + quantidadeParaDar + " Facas";
                    tmpro.color = Color.yellow;
                }
            }

           
            Destroy(gameObject);
        }
    }
}