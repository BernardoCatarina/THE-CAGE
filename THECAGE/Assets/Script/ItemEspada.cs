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

    [Header("Sons")]
    public AudioClip somColeta;
    private AudioSource audioSource;

    void Start()
    {
        posicaoInicial = transform.position;
        audioSource = GetComponent<AudioSource>();
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

                
                if (somColeta != null && audioSource != null)
                {
                    audioSource.PlayOneShot(somColeta);
                }

                
                if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
                if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

                
                float tempoDoSom = somColeta != null ? somColeta.length : 0.1f;
                Destroy(gameObject, tempoDoSom);
            }
        }
    }
}