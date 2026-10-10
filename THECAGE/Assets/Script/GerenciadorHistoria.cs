using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GerenciadorHistoria : MonoBehaviour
{
    [Header("Elementos de UI")]
    public Text componenteTexto;
    public Text textoDoBotao;

    [Header("Controle")]
    public GameObject botaoPrincipal;

    [Header("Conteúdo da História")]
    [TextArea(3, 5)]
    public List<string> textosHistoria;

    private int indiceAtual = 0;
    private Vector3 ultimaPosicaoMouse;

    private float tempoUltimoClique = 0f;
    private float tempoEsperaClique = 0.3f; 

    void Start()
    {
        ExibirSlideAtual();

        ultimaPosicaoMouse = Input.mousePosition;

        EventSystem.current.SetSelectedGameObject(null);
        if (botaoPrincipal != null)
        {
            EventSystem.current.SetSelectedGameObject(botaoPrincipal);
        }
    }

    void Update()
    {
        // --- A MÁGICA "PRO" DO MOUSE E CONTROLE ---
        bool mouseMoveuDeVerdade = false;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseMoveuDeVerdade = Mathf.Abs(Input.GetAxis("Mouse X")) > 1.5f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 1.5f;
        }
        else
        {
            mouseMoveuDeVerdade = (Input.mousePosition - ultimaPosicaoMouse).sqrMagnitude > 2.0f;
        }

        if (mouseMoveuDeVerdade || Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
            {
                if (botaoPrincipal != null)
                    EventSystem.current.SetSelectedGameObject(botaoPrincipal);
            }
        }

        ultimaPosicaoMouse = Input.mousePosition;

        // --- FORÇAR O CLIQUE COM CONTROLES GENÉRICOS ---
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Button botaoAtivo = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                if (botaoAtivo != null)
                {
                    botaoAtivo.onClick.Invoke();
                }
            }
        }
    }

    public void AvancarSlide()
    {
        // BARREIRA ANTI-DUPLO CLIQUE: Se o tempo atual for menor que o tempo de segurança, ele cancela o clique extra!
        if (Time.time < tempoUltimoClique + tempoEsperaClique)
            return;
                
        tempoUltimoClique = Time.time;

        indiceAtual++;

        if (indiceAtual < textosHistoria.Count)
        {
            ExibirSlideAtual();
        }
        else
        {
            ControleTransicao transicao = FindObjectOfType<ControleTransicao>();

            if (transicao != null)
            {
                transicao.IrParaProximaFase("Fase");
            }
            else
            {
                SceneManager.LoadScene("Fase");
            }
        }
    }

    void ExibirSlideAtual()
    {
        componenteTexto.text = textosHistoria[indiceAtual];

        if (indiceAtual == textosHistoria.Count - 1)
        {
            if (textoDoBotao != null)
                textoDoBotao.text = "JOGAR!";
        }
        else
        {
            if (textoDoBotao != null)
                textoDoBotao.text = "Próximo >";
        }
    }
}