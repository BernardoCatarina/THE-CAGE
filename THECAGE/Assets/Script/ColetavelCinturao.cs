using UnityEngine;

public class ColetavelCinturao : MonoBehaviour
{
    [Header("Efeitos Épicos de Coleta")]
    public GameObject prefabExplosao; 
    public GameObject prefabAura;     
    public AudioClip somDeVitoria;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (somDeVitoria != null)
            {
                AudioSource.PlayClipAtPoint(somDeVitoria, Camera.main.transform.position);
            }

            
            if (prefabExplosao != null)
            {
                Instantiate(prefabExplosao, transform.position, Quaternion.identity);
            }

           
            if (prefabAura != null)
            {
             
                GameObject aura = Instantiate(prefabAura, collision.transform.position, Quaternion.identity);

                
                aura.transform.SetParent(collision.transform);
            }

            
            Destroy(gameObject);
        }
    }
}