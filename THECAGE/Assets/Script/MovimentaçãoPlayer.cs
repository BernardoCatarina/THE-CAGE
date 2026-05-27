using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    // Variável para controlar a velocidade do personagem
    public float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 movimento;

    void Start()
    {
        // Pega o componente de física (Rigidbody2D) anexado ao personagem
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lê os inputs do teclado (W, A, S, D ou setinhas)
        // Retorna -1, 0 ou 1, garantindo um movimento seco e responsivo
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Aplica o movimento na física do jogo
        // O .normalized garante que andar na diagonal não seja mais rápido que andar em linha reta
        rb.MovePosition(rb.position + movimento.normalized * velocidade * Time.fixedDeltaTime);
    }
}