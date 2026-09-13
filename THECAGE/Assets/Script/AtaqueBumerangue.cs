using UnityEngine;
using System.Collections;

public class AtaqueBumerangue : MonoBehaviour
{
    public float velocidade = 7f;
    public float velocidadeGiro = -600f;
    public float tempoDeVoo = 1f;

    private Rigidbody2D rig;
    private float direcaoX;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
           
            if (player.transform.position.x > transform.position.x)
            {
                direcaoX = 1f; 
            }
            else
            {
                direcaoX = -1f; 
            }
        }

        StartCoroutine(Trajeto());
    }

    void Update()
    {
        
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);
    }

    IEnumerator Trajeto()
    {
        
        rig.linearVelocity = new Vector2(direcaoX * velocidade, 0);

        yield return new WaitForSeconds(tempoDeVoo);

        
        rig.linearVelocity = new Vector2(-direcaoX * velocidade, 0);

        yield return new WaitForSeconds(tempoDeVoo);

        
        Destroy(gameObject);
    }
}