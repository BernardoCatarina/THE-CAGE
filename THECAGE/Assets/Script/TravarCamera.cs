using UnityEngine;

public class TravarCamera : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Se você usa Cinemachine:
            // FindObjectOfType<Cinemachine.CinemachineVirtualCamera>().Follow = null;

            // Se você usa um script próprio na câmera (substitua "ScriptDaCamera" pelo nome do seu script):
            Camera.main.GetComponent<SeguirPlayer>().enabled = false;
        }
    }
}