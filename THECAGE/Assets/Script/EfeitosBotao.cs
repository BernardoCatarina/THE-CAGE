using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EfeitosBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
{
    [Header("Animação (Tamanho)")]
    public Vector3 tamanhoAoPassarMouse = new Vector3(1.1f, 1.1f, 1.1f);
    public float velocidadeAnimacao = 12f;
    private Vector3 tamanhoOriginal;
    private bool estaSelecionado = false;

    [Header("Sons")]
    public AudioClip somHover;
    public AudioClip somClick;
    private AudioSource audioSource;

    void Start()
    {
        tamanhoOriginal = transform.localScale;
        audioSource = FindObjectOfType<AudioSource>();
    }

    void Update()
    {
       
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        
        Vector3 tamanhoAlvo = estaSelecionado ? tamanhoAoPassarMouse : tamanhoOriginal;
        transform.localScale = Vector3.Lerp(transform.localScale, tamanhoAlvo, Time.deltaTime * velocidadeAnimacao);
    }

    // --- MOUSE ---
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (EventSystem.current != null) EventSystem.current.SetSelectedGameObject(null);
        AtivarEfeito();
    }
    public void OnPointerExit(PointerEventData eventData) { DesativarEfeito(); }
    public void OnPointerClick(PointerEventData eventData) { TocarSomClick(); }

    // --- CONTROLE ---
    public void OnSelect(BaseEventData eventData) { AtivarEfeito(); }
    public void OnDeselect(BaseEventData eventData) { DesativarEfeito(); }
    public void OnSubmit(BaseEventData eventData) { TocarSomClick(); }

    // --- LÓGICA ---
    private void AtivarEfeito()
    {
        estaSelecionado = true;
        if (somHover != null && audioSource != null) audioSource.PlayOneShot(somHover);
    }

    private void DesativarEfeito()
    {
        estaSelecionado = false;
    }

    private void TocarSomClick()
    {
        if (somClick != null && audioSource != null) audioSource.PlayOneShot(somClick);
    }
}