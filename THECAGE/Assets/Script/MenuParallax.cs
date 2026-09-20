using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float forcaDoParallax = 12f; 

    private RectTransform rectTransform;
    private Vector2 posicaoInicial;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posicaoInicial = rectTransform.anchoredPosition;
    }

    void Update()
    {
        
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        
        Vector2 deslocamento = new Vector2(mouseX * -forcaDoParallax, mouseY * -forcaDoParallax);

        
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, posicaoInicial + deslocamento, Time.deltaTime * 5f);
    }
}