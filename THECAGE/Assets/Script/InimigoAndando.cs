using UnityEngine;

public class InimigoAndando : MonoBehaviour
{
    public float velocidade = 2f;
    public float tempoDeCaminhada = 2f; // Tempo que ele anda antes de virar

    private float contadorTempo;
    private bool indoParaDireita = false;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        contadorTempo = tempoDeCaminhada;
    }

    void Update()
    {
        contadorTempo -= Time.deltaTime;

        if (contadorTempo <= 0)
        {
            Virar();
            contadorTempo = tempoDeCaminhada;
        }
    }

    void FixedUpdate()
    {
        // Aplica a velocidade baseada na direção
        if (indoParaDireita)
        {
            rig.linearVelocity = new Vector2(velocidade, rig.linearVelocity.y);
        }
        else
        {
            rig.linearVelocity = new Vector2(-velocidade, rig.linearVelocity.y);
        }
    }

    void Virar()
    {
        indoParaDireita = !indoParaDireita;

        // Vira a imagem do monstro para o outro lado
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}