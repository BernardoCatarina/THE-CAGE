using UnityEngine;

public class PuloBoss : MonoBehaviour
{
    public float forcaDoPulo = 10f; 
    public float tempoEntrePulos = 3f; 

    private float proximoPulo;
    private Rigidbody2D rig;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Time.time >= proximoPulo)
        {
            Pular();
            proximoPulo = Time.time + tempoEntrePulos;
        }
    }

    void Pular()
    {
        if (animator != null)
        {
            animator.SetTrigger("Pular"); 
        }


        rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);

        
        rig.AddForce(new Vector2(0f, forcaDoPulo), ForceMode2D.Impulse);
    }
}