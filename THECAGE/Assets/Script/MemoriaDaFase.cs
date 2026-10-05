using UnityEngine;

public class MemoriaDaFase : MonoBehaviour
{
    [Header("Identificador Único (RG do Objeto)")]
    [Tooltip("Dê um nome diferente para CADA objeto. Ex: Fase1_Faca_01, Fase2_Chefe_Principal")]
    public string idUnico;

    void Start()
    {
        
        if (PlayerPrefs.GetInt(idUnico, 0) == 1)
        {
            
            gameObject.SetActive(false);
        }
    }

    
    public void RegistrarMorteOuColeta()
    {
        if (idUnico != "")
        {
            PlayerPrefs.SetInt(idUnico, 1);
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogWarning("Faltou colocar o ID Único no objeto: " + gameObject.name);
        }
    }
}