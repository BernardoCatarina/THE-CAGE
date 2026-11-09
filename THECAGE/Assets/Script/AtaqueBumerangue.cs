using UnityEngine;
using System.Collections;

public class AtaqueBumerangue : MonoBehaviour
{
    public float velocidade = 7f;
    public float velocidadeGiro = -600f;
    public float tempoDeVoo = 1f;

    private Rigidbody2D rig;
    private float direcaoX;

    // --- VARIÁVEIS PARA O EFEITO DE ENCOLHER ---
    private Vector3 escalaInicial;
    private float tempoNascimento;
    private float tempoTotalDeVida;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        
        escalaInicial = transform.localScale;
        tempoNascimento = Time.time;
        tempoTotalDeVida = tempoDeVoo * 2f; 

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

        // --- SISTEMA DE ENCOLHER ---
       
        float progresso = (Time.time - tempoNascimento) / tempoTotalDeVida;

        
        transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, progresso);
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