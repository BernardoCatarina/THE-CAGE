using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class OtimizarForaDaTela : MonoBehaviour
{
    [Header("O que Desligar?")]
    [Tooltip("Arraste para cá a luz (Light2D) deste objeto, se tiver uma.")]
    public Light2D luzDaLanterna;

    [Tooltip("Arraste para cá os scripts pesados (ex: movimento da serra, IA do inimigo).")]
    public MonoBehaviour[] scriptsParaPausar;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnBecameVisible()
    {
        
        if (luzDaLanterna != null) luzDaLanterna.enabled = true;

        
        if (animator != null) animator.enabled = true;
        if (rb != null) rb.simulated = true;

        
        foreach (MonoBehaviour script in scriptsParaPausar)
        {
            if (script != null) script.enabled = true;
        }
    }

    void OnBecameInvisible()
    {
        
        if (luzDaLanterna != null) luzDaLanterna.enabled = false;

        
        if (animator != null) animator.enabled = false;
        if (rb != null) rb.simulated = false;

        
        foreach (MonoBehaviour script in scriptsParaPausar)
        {
            if (script != null) script.enabled = false;
        }
    }
}