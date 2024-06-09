using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float initialSpeed = 2f;
    private float speed;
    private Rigidbody2D rb;
    public float moveDownAmount = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
    }

    void Update()
    {
        if (!enabled) return;
    }

    public void Move(bool movingRight)
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GameManager.Instance.EnemyHitWall();
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.RemoveEnemy(this);
    }
}
