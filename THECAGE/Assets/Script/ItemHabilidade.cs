using UnityEngine;
using TMPro;

public class ItemHabilidade : MonoBehaviour
{
    [Header("Qual habilidade este item libera?")]
    public bool liberaPuloDuplo = false;
    public bool liberaDash = false;

    [Header("Efeito Flutuante")]
    public float velocidadeFlutuacao = 2f;
    public float alturaFlutuacao = 0.3f;
    private Vector3 posicaoInicial;

    [Header("Efeito Visual e Áudio")]
    public GameObject prefabTextoFlutuante;
    public string textoAviso = "Habilidade Adquirida!";
    public Color corDoTexto = Color.yellow;

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
            // Procura o script de movimento do player
            mov scriptPlayer = collision.GetComponent<mov>();

            if (scriptPlayer != null)
            {
                // Libera a habilidade correta lá no script do player
                if (liberaPuloDuplo) scriptPlayer.DesbloquearPuloDuplo();
                if (liberaDash) scriptPlayer.DesbloquearDash();

                // Mostra o texto flutuante (Ex: "Dash Adquirido!")
                if (prefabTextoFlutuante != null)
                {
                    Vector3 posicaoTexto = transform.position + new Vector3(0, 0.5f, 0);
                    GameObject texto = Instantiate(prefabTextoFlutuante, posicaoTexto, Quaternion.identity);

                    TextMeshPro tmpro = texto.GetComponent<TextMeshPro>();
                    if (tmpro != null)
                    {
                        tmpro.text = textoAviso;
                        tmpro.color = corDoTexto;
                    }
                }

                // Toca o som sagrado/tecnológico de coleta
                if (somColeta != null && audioSource != null)
                {
                    audioSource.PlayOneShot(somColeta);
                }

                // Esconde o item e desliga a colisão para não pegar duas vezes
                if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
                if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

                // Destrói com atraso para o som tocar até o fim
                float tempoDoSom = somColeta != null ? somColeta.length : 0.1f;
                Destroy(gameObject, tempoDoSom);
            }
        }
    }
}