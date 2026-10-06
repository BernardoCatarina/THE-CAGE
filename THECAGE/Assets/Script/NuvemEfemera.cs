using System.Collections;
using UnityEngine;

public class NuvemEfemera : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    public float tempoParaSumir = 1f; 
    public float tempoParaReaparecer = 2.5f; 

    private SpriteRenderer spriteRenderer;
    private Collider2D colisor;
    private bool jaPisou = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !jaPisou)
        {
            StartCoroutine(RotinaDesaparecer());
        }
    }

    private IEnumerator RotinaDesaparecer()
    {
        jaPisou = true;

        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(tempoParaSumir);

        spriteRenderer.enabled = false;
        colisor.enabled = false;

        yield return new WaitForSeconds(tempoParaReaparecer);

        spriteRenderer.enabled = true;
        colisor.enabled = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        jaPisou = false;
    }
}