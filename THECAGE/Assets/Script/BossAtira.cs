using UnityEngine;

public class BossAtira : MonoBehaviour
{
    public GameObject socoPrefab; 
    public Transform pontoDeTiro; 
    public float tempoEntreAtaques = 2f; 

    private float proximoTiro;

    void Update()
    {
        
        if (Time.time >= proximoTiro)
        {
            Atirar();
            
            proximoTiro = Time.time + tempoEntreAtaques;
        }
    }

    void Atirar()
    {
        
        Instantiate(socoPrefab, pontoDeTiro.position, transform.rotation);
    }
}