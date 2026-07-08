using UnityEngine;

public class PuloBoss : MonoBehaviour
{
    public float forcaDoPulo = 10f; // Força que vai jogar o boss para cima
    public float tempoEntrePulos = 3f; // De quantos em quantos segundos ele pula

    private float proximoPulo;
    private Rigidbody2D rig;

    void Start()
    {
        // Pega o Rigidbody2D do Boss para podermos aplicar a força física
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se já deu o tempo de pular de novo
        if (Time.time >= proximoPulo)
        {
            Pular();
            proximoPulo = Time.time + tempoEntrePulos;
        }
    }

    void Pular()
    {
        // Zeramos a velocidade Y antes de pular para o pulo ser sempre consistente
        rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);

        // Empurra o Boss para cima (Impulso)
        rig.AddForce(new Vector2(0f, forcaDoPulo), ForceMode2D.Impulse);
    }
}