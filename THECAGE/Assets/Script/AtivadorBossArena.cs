using UnityEngine;

public class AtivadorBossArena : MonoBehaviour
{
    public PortaArenaBoss scriptDaPorta; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (scriptDaPorta != null)
            {
                scriptDaPorta.AtivarBoss(); 
            }

            
            gameObject.SetActive(false);
        }
    }
}