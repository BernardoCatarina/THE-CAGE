using UnityEngine;

public class BossAtira : MonoBehaviour
{
    public GameObject socoPrefab;
    public Transform pontoDeTiro;
    public float tempoEntreAtaques = 2f;

    private float proximoTiro;
    private Animator animator; 

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

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
        
        if (animator != null)
        {
            animator.SetTrigger("Soco");
        }

        Instantiate(socoPrefab, pontoDeTiro.position, transform.rotation);
    }
}