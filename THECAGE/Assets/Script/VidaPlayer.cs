using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    [Header("Sons")]
    public AudioClip somDeDano;
    private AudioSource audioSource;

    public int vidaMaxima = 10;
    private int vidaAtual;

    public Slider barraDeVida;
    public Text textoDeVida;

    private SpriteRenderer spriteRenderer;
    public int quantidadeDePiscadas = 3;
    public float tempoPiscada = 0.15f;

    public Animator animator;
    private bool estaMorto = false;

    public bool estaInvencivel = false;
    private Vector3 ultimaPosicaoSegura;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Inimigo"), false);
        audioSource = GetComponent<AudioSource>();
        vidaAtual = PlayerPrefs.GetInt("VidaSalva", vidaMaxima);

        if (vidaAtual <= 0)
        {
            vidaAtual = vidaMaxima;
            PlayerPrefs.SetInt("VidaSalva", vidaAtual);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
        AtualizarTexto();

        ultimaPosicaoSegura = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contato in collision.contacts)
            {
                if (contato.normal.y > 0.5f)
                {
                    ultimaPosicaoSegura = transform.position;
                    break;
                }
            }
        }
    }

    public void TomarDano(int quantidade)
    {
        TomarDanoMecanica(quantidade, Vector3.zero, false, false);
    }

    public void TomarDano(int quantidade, Vector3 posicaoInimigo)
    {
        TomarDanoMecanica(quantidade, posicaoInimigo, true, false);
    }

    private void TomarDanoMecanica(int quantidade, Vector3 posicaoInimigo, bool aplicarKnockback, bool ehArmadilha)
    {
        if (estaMorto || mov.modoImortal || estaInvencivel) return;

        estaInvencivel = true;

        vidaAtual -= quantidade;
        if (vidaAtual < 0) vidaAtual = 0;

        PlayerPrefs.SetInt("VidaSalva", vidaAtual);
        if (barraDeVida != null) barraDeVida.value = vidaAtual;
        AtualizarTexto();

        if (somDeDano != null && audioSource != null)
        {
            audioSource.PlayOneShot(somDeDano);
        }

        StartCoroutine(EfeitoHitstop());

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (ehArmadilha)
            {
                rb.linearVelocity = Vector2.zero;
                transform.position = ultimaPosicaoSegura;
            }
            else if (aplicarKnockback && !mov.modoVoo)
            {
                rb.linearVelocity = Vector2.zero;
                float direcao = 1f;
                if (posicaoInimigo != Vector3.zero)
                {
                    direcao = transform.position.x > posicaoInimigo.x ? 1f : -1f;
                }
                else
                {
                    direcao = transform.eulerAngles.y == 0 ? -1f : 1f;
                }

                rb.AddForce(new Vector2(direcao * 3f, 6f), ForceMode2D.Impulse);
                StartCoroutine(TravarControles());
            }
        }

        if (spriteRenderer != null)
        {
            StartCoroutine(EfeitoPiscar());
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    IEnumerator EfeitoHitstop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1f;
    }

    IEnumerator TravarControles()
    {
        mov scriptMov = GetComponent<mov>();
        if (scriptMov != null)
        {
            scriptMov.enabled = false;
            yield return new WaitForSeconds(0.25f);
            if (!estaMorto)
            {
                scriptMov.enabled = true;
            }
        }
    }

    IEnumerator EfeitoPiscar()
    {
        int layerPlayer = LayerMask.NameToLayer("Player");
        int layerInimigo = LayerMask.NameToLayer("Inimigo");

        Physics2D.IgnoreLayerCollision(layerPlayer, layerInimigo, true);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }

        Collider2D meuCollider = GetComponent<Collider2D>();
        if (meuCollider != null)
        {
            while (Physics2D.OverlapBox(meuCollider.bounds.center, meuCollider.bounds.size, 0f, 1 << layerInimigo) != null)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (!mov.modoImortal)
        {
            Physics2D.IgnoreLayerCollision(layerPlayer, layerInimigo, false);
        }

        estaInvencivel = false;
    }

    void AtualizarTexto()
    {
        if (textoDeVida != null)
        {
            textoDeVida.text = vidaAtual + " / " + vidaMaxima;
        }
    }

    void Morrer()
    {
        if (estaMorto) return;
        estaMorto = true;
        StartCoroutine(RotinaQuedaEMorte());
    }

    IEnumerator RotinaQuedaEMorte()
    {
        if (GetComponent<mov>() != null) GetComponent<mov>().enabled = false;
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetTrigger("AnimacaoMorte");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            yield return new WaitForSeconds(0.1f);
            float tempoLimite = 3f;
            float timer = 0f;
            while (Mathf.Abs(rb.linearVelocity.y) > 0.05f && timer < tempoLimite)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);
        EfeitoFade fade = FindObjectOfType<EfeitoFade>();
        if (fade != null)
        {
            fade.IniciarFade("GameOver", new Color(0.5f, 0f, 0f), 2.5f);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Curar(int quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > vidaMaxima) vidaAtual = vidaMaxima;
        PlayerPrefs.SetInt("VidaSalva", vidaAtual);
        if (barraDeVida != null) barraDeVida.value = vidaAtual;
        AtualizarTexto();
    }
}