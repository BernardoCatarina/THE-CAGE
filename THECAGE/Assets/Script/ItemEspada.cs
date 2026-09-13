using UnityEngine;
using TMPro; 
public class ItemEspada : MonoBehaviour
{
    [Header("Efeito Flutuante")]
    public float velocidadeFlutuacao = 2f;
    public float alturaFlutuacao = 0.3f;
    private Vector3 posicaoInicial;

    [Header("Efeito Visual")]
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
            mov scriptPlayer = collision.GetComponent<mov>();
            if (scriptPlayer != null)
            {
                
                scriptPlayer.ColetarEspada();

                
                if (prefabTextoFlutuante != null)
                {
                    Vector3 posicaoTexto = transform.position + new Vector3(0, 0.5f, 0);
                    GameObject texto = Instantiate(prefabTextoFlutuante, posicaoTexto, Quaternion.identity);

                    TextMeshPro tmpro = texto.GetComponent<TextMeshPro>();
                    if (tmpro != null)
                    {
                        tmpro.text = "Espada Adquirida!";
                        tmpro.color = Color.cyan; 
                    }
                }

                
                Destroy(gameObject);
            }
        }
    }
}