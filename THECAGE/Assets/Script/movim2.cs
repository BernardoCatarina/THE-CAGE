using UnityEngine;

public class movimentaçao2 : MonoBehaviour
{
    public float _speed = 5f;
    private Vector2 _moviment;
    private Rigidbody2D _rb;
    public GameObject bullet;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moviment = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _rb.linearVelocity = _moviment * _speed;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position, transform.rotation);


        }



    }
}
