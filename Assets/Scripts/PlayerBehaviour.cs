using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        SetDirection(moveHorizontal);
        Move();
    }

    void SetDirection(float horizontal)
    {
        movementDirection = new Vector2(horizontal, 0).normalized;
    }

    void Move()
    {
        rb.velocity = movementDirection * speed;
    }
}