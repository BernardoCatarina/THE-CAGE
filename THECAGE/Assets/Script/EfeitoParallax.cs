using UnityEngine;

public class EfeitoParallax : MonoBehaviour
{
    [Header("Configurações")]
    public Transform cameraDoJogo;
    public float multiplicadorParallax = 0.5f; 

    private Vector3 ultimaPosicaoCamera;

    void Start()
    {
        
        if (cameraDoJogo == null)
        {
            cameraDoJogo = Camera.main.transform;
        }

        ultimaPosicaoCamera = cameraDoJogo.position;
    }

    
    void LateUpdate()
    {
        
        Vector3 movimentoCamera = cameraDoJogo.position - ultimaPosicaoCamera;

        
        transform.position += new Vector3(movimentoCamera.x * multiplicadorParallax, 0, 0);

        
        ultimaPosicaoCamera = cameraDoJogo.position;
    }
}