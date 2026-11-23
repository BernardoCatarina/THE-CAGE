using UnityEngine;

public class FlutuarItem : MonoBehaviour
{
    public float alturaDoPulo = 0.3f; 
    public float velocidade = 3f;     

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float novaPosicaoY = posicaoInicial.y + (Mathf.Sin(Time.time * velocidade) * alturaDoPulo);
        transform.position = new Vector3(posicaoInicial.x, novaPosicaoY, posicaoInicial.z);
    }
}