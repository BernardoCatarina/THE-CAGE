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

    [Header("Sons")]
    public AudioClip somDano;
    public AudioClip somMorte; 
    private AudioSource audioSource;

    [Header("Efeito Visual")]
    public GameObject prefabEfeitoMorte;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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


            if (prefabEfeitoMorte != null)
            {
                Instantiate(prefabEfeitoMorte, transform.position, Quaternion.identity);
            }


            if (somMorte != null && audioSource != null)
            {
                audioSource.PlayOneShot(somMorte);
            }


            Renderer[] todosVisuais = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in todosVisuais) r.enabled = false;

            Canvas[] todasTelas = GetComponentsInChildren<Canvas>();
            foreach (Canvas c in todasTelas) c.enabled = false;


            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                GetComponent<Rigidbody2D>().simulated = false;
            }

            Collider2D[] colisores = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colisores) col.enabled = false;


            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour s in scripts)
            {
                if (s != this) s.enabled = false;
            }


            float tempoSom = (somMorte != null) ? somMorte.length : 0.1f;
            Destroy(gameObject, tempoSom);
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