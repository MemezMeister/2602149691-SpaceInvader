using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public int baseShootingDifficulty = 1;
    private int shootingDifficulty;
    public AudioClip firingSound; 
    private AudioSource audioSource;
    private DiceRoll diceRoll;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        diceRoll = FindObjectOfType<DiceRoll>();
        if (diceRoll == null)
        {
            Debug.LogError("DiceRoll script not found in the scene. Ensure there is an active GameObject with the DiceRoll script attached.");
            enabled = false;
            return;
        }
        Debug.Log("DiceRoll script found.");
        shootingDifficulty = baseShootingDifficulty;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (diceRoll != null)
        {
            int roll = diceRoll.RollDice(20); 
            if (roll <= shootingDifficulty)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.up * bulletSpeed; 

        if (audioSource != null && firingSound != null)
        {
            audioSource.PlayOneShot(firingSound);
        }
    }
}
