using UnityEngine;
using TMPro;

public class TextoFlutuante : MonoBehaviour
{
    public float tempoDeVida = 1f;
    public float velocidadeSubida = 2f;
    public Vector3 offsetInicial = new Vector3(0, 1.5f, 0); 

    private TextMeshPro texto;

    void Start()
    {
       
        transform.position += offsetInicial;

        texto = GetComponent<TextMeshPro>();

       
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        
        transform.position += new Vector3(0, velocidadeSubida * Time.deltaTime, 0);

       
        if (texto != null)
        {
            Color cor = texto.color;
            cor.a -= (1f / tempoDeVida) * Time.deltaTime;
            texto.color = cor;
        }
    }
}