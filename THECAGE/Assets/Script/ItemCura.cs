using UnityEngine;
using TMPro;

public class ItemCura : MonoBehaviour
{
    [Header("Cura")]
    public int cura = 1;
    public GameObject prefabTextoFlutuante;

    [Header("Animação")]
    public float velocidadeFlutuacao = 3f;
    public float distanciaFlutuacao = 0.2f;
    private Vector3 posicaoInicial;

    [Header("Sons")]
    public AudioClip somCura;
    private AudioSource audioSource;
    void Start()
    {
        posicaoInicial = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {       
        transform.position = posicaoInicial + new Vector3(0, Mathf.Sin(Time.time * velocidadeFlutuacao) * distanciaFlutuacao, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<VidaPlayer>().Curar(cura);

            if (prefabTextoFlutuante != null)
            {
                GameObject texto = Instantiate(prefabTextoFlutuante, transform.position, Quaternion.identity);
                texto.GetComponent<TextMeshPro>().text = "+" + cura;
                texto.GetComponent<TextMeshPro>().color = Color.green;
            }

            if (somCura != null && audioSource != null)
            {
                audioSource.PlayOneShot(somCura);
            }

            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
            if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

            float tempoDoSom = somCura != null ? somCura.length : 0.1f;
            Destroy(gameObject, tempoDoSom);
        }
    }
}