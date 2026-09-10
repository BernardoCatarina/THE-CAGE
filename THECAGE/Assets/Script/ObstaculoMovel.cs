using UnityEngine;

public class ObstaculoMovel : MonoBehaviour
{
    public float velocidadeGiro = -300f; 
    public float velocidadeMovimento = 2f; 
    public float distancia = 3f; 
    public bool moverNaVertical = true;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);


        float movimento = Mathf.Sin(Time.time * velocidadeMovimento) * distancia;


        if (moverNaVertical)
        {
            transform.position = posicaoInicial + new Vector3(0, movimento, 0);
        }
        else
        {
            transform.position = posicaoInicial + new Vector3(movimento, 0, 0);
        }
    }
}