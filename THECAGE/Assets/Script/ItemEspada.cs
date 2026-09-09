using UnityEngine;

public class ItemEspada : MonoBehaviour
{
    [Header("Efeito Flutuante")]
    public float velocidadeFlutuacao = 2f;
    public float alturaFlutuacao = 0.3f;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
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
                Destroy(gameObject);
            }
        }
    }
}