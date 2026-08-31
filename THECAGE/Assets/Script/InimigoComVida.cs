using UnityEngine;
using UnityEngine.UI;

public class InimigoComVida : MonoBehaviour
{
    [Header("Atributos")]
    public int vidaMaxima = 5;
    private int vidaAtual;
    public int danoNoPlayer = 1;
    public float velocidade = 2f;

    [Header("Interface")]
    public Slider barraDeVida;

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        
        if (colisao.CompareTag("Tiro"))
        {
            int danoRecebido = 1;

            
            vidaAtual -= danoRecebido;
            if (barraDeVida != null) barraDeVida.value = vidaAtual;

            Destroy(colisao.gameObject); 

            
            if (vidaAtual <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        else if (colisao.CompareTag("Player"))
        {
            
            VidaPlayer vida = colisao.GetComponent<VidaPlayer>();

            if (vida != null)
            {
                
                vida.TomarDano(danoNoPlayer);
            }

            Destroy(gameObject); 
        }
    }
}