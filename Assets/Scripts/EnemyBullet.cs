using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
