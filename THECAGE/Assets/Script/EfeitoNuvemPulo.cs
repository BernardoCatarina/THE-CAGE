using UnityEngine;

public class EfeitoNuvemPulo : MonoBehaviour
{
    public float duracao = 0.35f;

    private SpriteRenderer sprite;
    private float tempo;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        tempo += Time.deltaTime;

        float progresso = Mathf.Clamp01(tempo / duracao);

        if (sprite != null)
        {
            Color cor = sprite.color;
            cor.a = 1f - progresso;
            sprite.color = cor;
        }

        if (progresso >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
