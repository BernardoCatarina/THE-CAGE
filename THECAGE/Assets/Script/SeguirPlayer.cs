using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public Transform player;
    public float suavidade = 5f;
    public Vector3 offset = new Vector3(0, 2f, -10f);

    [Header("Configuração de Respawn/Checkpoint")]
    [Tooltip("Se a câmara estiver a uma distância maior que este valor do player, ela corta instantaneamente em vez de fazer o movimento suave.")]
    public float distanciaParaCorte = 15f;

    void Start()
    {
        
        ReposicionarImediatamente();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 posicaoDesejada = player.position + offset;

            
            float distancia = Vector3.Distance(transform.position, posicaoDesejada);

            
            if (distancia > distanciaParaCorte)
            {
                transform.position = posicaoDesejada;
            }
            else
            {
               
                transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavidade * Time.deltaTime);
            }
        }
    }

    
    public void ReposicionarImediatamente()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}