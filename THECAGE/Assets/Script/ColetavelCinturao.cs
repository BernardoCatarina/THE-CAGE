using UnityEngine;

public class ColetavelCinturao : MonoBehaviour
{
    [Header("Efeitos Épicos de Coleta")]
    public GameObject prefabExplosao;
    public GameObject prefabAura;
    public AudioClip somDeVitoria;

    [Header("Ajuste da Aura")]
    [Tooltip("Ajuste o valor de Y para subir a aura até ao meio do peito do player")]
    public Vector3 deslocamentoDaAura = new Vector3(0f, 1.5f, 0f);

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

                
                aura.transform.localPosition = deslocamentoDaAura;
            }

           
            Destroy(gameObject);
        }
    }
}