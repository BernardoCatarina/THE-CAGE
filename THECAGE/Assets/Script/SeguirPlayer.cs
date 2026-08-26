using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public Transform player; 
    public float suavidade = 5f; 
    public Vector3 offset = new Vector3(0, 2f, -10f); 

    void FixedUpdate()
    {
        if (player != null)
        {
            
            Vector3 posicaoDesejada = player.position + offset;

            
            transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavidade * Time.deltaTime);
        }
    }
}