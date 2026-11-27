using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossIA : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;

    [Header("Efeitos e Teletransporte")]
    public GameObject prefabFumaca;
    public Transform pontoTeleporteEsquerdo;
    public Transform pontoTeleporteDireito;

    [Header("Invocação de Inimigos")]
    public GameObject[] prefabsInimigos;
    public Transform[] pontosDeSpawnInimigos;

    [Header("Limites do Ringue (Para os Inimigos)")]
    public float limiteEsquerdoDoRingue;
    public float limiteDireitoDoRingue;

    [Header("Efeito do Escudo")]
    public GameObject prefabEfeitoEscudo; 

    [HideInInspector] public bool escudoAtivo = false;

    private bool estaTeletransportando = false;
    private bool aguardandoInimigos = false;
    private List<GameObject> inimigosVivos = new List<GameObject>();

    private SpriteRenderer spriteRenderer;
    private Collider2D colisor;
    private Rigidbody2D rig;
    private BossAtira scriptAtira;
    private PuloBoss scriptPulo;
    private VidaBoss scriptVida;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
        scriptAtira = GetComponent<BossAtira>();
        scriptPulo = GetComponent<PuloBoss>();
        scriptVida = GetComponent<VidaBoss>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (!estaTeletransportando && !aguardandoInimigos && player != null)
        {
            VirarParaOPlayer();
        }

        if (aguardandoInimigos)
        {
            VerificarInimigosVivos();
        }
    }

    void VirarParaOPlayer()
    {
        float tamanhoX = Mathf.Abs(transform.localScale.x);
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(-tamanhoX, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(tamanhoX, transform.localScale.y, transform.localScale.z);
    }

    public void IniciarTeletransporte()
    {
        StartCoroutine(RotinaTeletransporte());
    }

    IEnumerator RotinaTeletransporte()
    {
        estaTeletransportando = true;

        if (prefabFumaca != null) Instantiate(prefabFumaca, transform.position, Quaternion.identity);

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (colisor != null) colisor.enabled = false;
        if (rig != null) rig.simulated = false;

        if (scriptAtira != null) scriptAtira.enabled = false;
        if (scriptPulo != null) scriptPulo.enabled = false;

        yield return new WaitForSeconds(0.8f);

        Vector3 destino = player.position.x > (pontoTeleporteEsquerdo.position.x + pontoTeleporteDireito.position.x) / 2
            ? pontoTeleporteEsquerdo.position
            : pontoTeleporteDireito.position;

        transform.position = destino;

        if (prefabFumaca != null) Instantiate(prefabFumaca, transform.position, Quaternion.identity);

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (colisor != null) colisor.enabled = true;
        if (rig != null) rig.simulated = true;

        VirarParaOPlayer();

        if (scriptAtira != null)
        {
            scriptAtira.tempoEntreAtaques = 1.5f;
            scriptAtira.enabled = true;
        }
        if (scriptPulo != null)
        {
            scriptPulo.tempoEntrePulos = 2.4f;
            scriptPulo.enabled = true;
        }

        estaTeletransportando = false;
    }

    public void IniciarInvocacao()
    {
        aguardandoInimigos = true;
        escudoAtivo = true;

        
        if (prefabEfeitoEscudo != null)
        {
            Instantiate(prefabEfeitoEscudo, transform.position, Quaternion.identity);
        }

        
        if (scriptVida != null) StartCoroutine(scriptVida.PiscarEscudo(1f));

        if (scriptAtira != null) scriptAtira.enabled = false;
        if (scriptPulo != null) scriptPulo.enabled = false;

        inimigosVivos.Clear();
        int quantidade = Random.Range(2, 4);

        List<Transform> pontosDisponiveis = new List<Transform>(pontosDeSpawnInimigos);

        if (quantidade > pontosDisponiveis.Count)
        {
            quantidade = pontosDisponiveis.Count;
        }

        for (int i = 0; i < quantidade; i++)
        {
            if (prefabsInimigos.Length > 0 && pontosDisponiveis.Count > 0)
            {
                GameObject inimigoSorteado = prefabsInimigos[Random.Range(0, prefabsInimigos.Length)];

                int indexPonto = Random.Range(0, pontosDisponiveis.Count);
                Transform pontoEscolhido = pontosDisponiveis[indexPonto];

                pontosDisponiveis.RemoveAt(indexPonto);

                if (inimigoSorteado != null)
                {
                    GameObject novoInimigo = Instantiate(inimigoSorteado, pontoEscolhido.position, Quaternion.identity);

                    MiniBossIA scriptIAInimigo = novoInimigo.GetComponent<MiniBossIA>();
                    if (scriptIAInimigo != null)
                    {
                        scriptIAInimigo.limiteEsquerdo = limiteEsquerdoDoRingue;
                        scriptIAInimigo.limiteDireito = limiteDireitoDoRingue;
                    }

                    inimigosVivos.Add(novoInimigo);
                }
            }
        }
    }

    void VerificarInimigosVivos()
    {
        inimigosVivos.RemoveAll(inimigo => inimigo == null);

        if (inimigosVivos.Count == 0)
        {
            aguardandoInimigos = false;
            escudoAtivo = false;

            
            if (prefabEfeitoEscudo != null)
            {
                Instantiate(prefabEfeitoEscudo, transform.position, Quaternion.identity);
            }

            if (scriptAtira != null) scriptAtira.enabled = true;
            if (scriptPulo != null) scriptPulo.enabled = true;
        }
    }
}