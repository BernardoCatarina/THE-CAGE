using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 1;

    private void OnCollisionEnter2D(Collision2D colisao)
    {
       
        if (colisao.gameObject.CompareTag("Player"))
        {
            
            VidaPlayer vida = colisao.gameObject.GetComponent<VidaPlayer>();
            if (vida != null)
            {
                vida.TomarDano(dano);
            }
        }
    }
}