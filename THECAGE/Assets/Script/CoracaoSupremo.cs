using UnityEngine;

public class CoracaoSupremo : MonoBehaviour
{
    [Header("Configurações do Item")]
    public string idDoItem = "CoracaoSecretoFase2"; 
    public int vidaExtra = 10;
    public AudioClip somColeta;

    void Start()
    {
       
        if (PlayerPrefs.GetInt(idDoItem, 0) == 1)
        {
            Destroy(gameObject);
        }
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