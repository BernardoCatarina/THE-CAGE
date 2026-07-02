using UnityEngine;

public class BossAtira : MonoBehaviour
{
    public GameObject socoPrefab; // Aqui você vai arrastar o Prefab do soco
    public Transform pontoDeTiro; // De onde o soco vai nascer
    public float tempoEntreAtaques = 2f; // Tempo em segundos entre cada soco

    private float proximoTiro;

    void Update()
    {
        // Verifica se já passou o tempo para atirar de novo
        if (Time.time >= proximoTiro)
        {
            Atirar();
            // Define o tempo do próximo tiro
            proximoTiro = Time.time + tempoEntreAtaques;
        }
    }

    void Atirar()
    {
        // Cria o soco na posição do ponto de tiro
        Instantiate(socoPrefab, pontoDeTiro.position, transform.rotation);
    }
}