using UnityEngine;

public class CoracaoSupremo : MonoBehaviour
{
    [Header("Configurações do Item")]
    public string idDoItem = "CoracaoSecretoFase2";
    public int vidaExtra = 10;
    public AudioClip somColeta;

    [Header("Animação (Flutuar)")]
    public float velocidadeFlutuar = 3f; 
    public float alturaFlutuar = 0.3f;   

    private Vector3 posicaoInicial;

    void Start()
    {
        if (PlayerPrefs.GetInt(idDoItem, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }
                
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velocidadeFlutuar) * alturaFlutuar;
        transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaPlayer scriptVida = collision.GetComponent<VidaPlayer>();

            if (scriptVida != null)
            {
                scriptVida.AumentarVidaMaxima(vidaExtra);

               
                PlayerPrefs.SetInt(idDoItem, 1);
                PlayerPrefs.Save();

                if (somColeta != null)
                {
                   
                    AudioSource.PlayClipAtPoint(somColeta, transform.position);
                }

                Destroy(gameObject);
            }
        }
    }
}