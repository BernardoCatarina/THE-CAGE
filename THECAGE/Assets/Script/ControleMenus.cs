using UnityEngine;
using UnityEngine.EventSystems;

public class ControleMenus : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject primeiroBotao;

    void Start()
    {
        
        EventSystem.current.SetSelectedGameObject(null);
        if (primeiroBotao != null)
        {
            EventSystem.current.SetSelectedGameObject(primeiroBotao);
        }
    }

    void Update()
    {
        
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f)
        {
            Cursor.visible = true; 
        }
        
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            Cursor.visible = false; 

            
            if (EventSystem.current.currentSelectedGameObject == null && primeiroBotao != null)
            {
                EventSystem.current.SetSelectedGameObject(primeiroBotao);
            }
        }
    }
}