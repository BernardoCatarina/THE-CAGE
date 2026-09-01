using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public int forcaDoDano = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            VidaPlayer vidaScript = collision.gameObject.GetComponent<VidaPlayer>();

            if (vidaScript != null)
            {
                // SUBSTITUA 'SuaFuncaoDeDano' pelo nome da função que você criou para tirar vida
                vidaScript.TomarDano(forcaDoDano);
            }
        }
    }
}