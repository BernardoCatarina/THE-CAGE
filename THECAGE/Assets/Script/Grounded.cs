using UnityEngine;
using UnityEngine.TextCore.Text;

public class Grounded : MonoBehaviour
{
    public class Character : MonoBehaviour
    {
        public bool isJumping;
    }
    
    

    Character Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = gameObject.transform.parent.gameObject.GetComponent<Character>();
    }

    void OnCollisionEnter2D(Collision2D collisor)
    {
        if(collisor.gameObject.layer == 8)
        {
            Player.isJumping = false;
        }
    }

   void OnCollisionExit2D(Collision2D collisor)
    {
        if (collisor.gameObject.layer == 8)
        {
            Player.isJumping = true;
        }
    }

}
