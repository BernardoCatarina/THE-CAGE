using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class Checkpoint : MonoBehaviour
{
    [Header("Arraste o objeto Text aqui")]
    public GameObject textoFlutuante;

    private bool jaPegou = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !jaPegou)
        {
            jaPegou = true;

            
            PlayerPrefs.SetFloat("CheckX", transform.position.x);
            PlayerPrefs.SetFloat("CheckY", transform.position.y);
            PlayerPrefs.SetInt("TemCheck", 1);

           
            PlayerPrefs.SetString("FaseSalva", SceneManager.GetActiveScene().name);

            PlayerPrefs.Save();

            if (textoFlutuante != null)
            {
                StartCoroutine(MostrarTexto());
            }
        }
    }

    private IEnumerator MostrarTexto()
    {
        textoFlutuante.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoFlutuante.SetActive(false);
    }
}