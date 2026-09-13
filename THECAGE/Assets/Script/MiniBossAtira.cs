using UnityEngine;

public class MiniBossAtira : MonoBehaviour
{
    public GameObject prefabBumerangue;
    public Transform pontoDeTiro;
    public float tempoEntreAtaques = 3f;

    private float proximoTiro;

    void Update()
    {
        if (Time.time >= proximoTiro)
        {
            Instantiate(prefabBumerangue, pontoDeTiro.position, Quaternion.identity);
            proximoTiro = Time.time + tempoEntreAtaques;
        }
    }
}