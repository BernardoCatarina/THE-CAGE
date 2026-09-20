using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class EfeitosBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Animação (Tamanho)")]
    public Vector3 tamanhoAoPassarMouse = new Vector3(1.1f, 1.1f, 1.1f); 
    public float velocidadeAnimacao = 12f;
    private Vector3 tamanhoOriginal;
    private bool mouseEmCima = false;

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

        Vector3 tamanhoAlvo = mouseEmCima ? tamanhoAoPassarMouse : tamanhoOriginal;
        transform.localScale = Vector3.Lerp(transform.localScale, tamanhoAlvo, Time.deltaTime * velocidadeAnimacao);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEmCima = true;
        if (somHover != null && audioSource != null)
        {
            audioSource.PlayOneShot(somHover);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEmCima = false;
    }

     public void OnPointerClick(PointerEventData eventData)
    {
        if (somClick != null && audioSource != null)
        {
            audioSource.PlayOneShot(somClick);
        }
    }
}