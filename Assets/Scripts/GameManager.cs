using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject enemyPrefab;
    public GameObject playerPrefab; 
    public Transform spawnPoint; 
    public TMP_Text healthText;
    public TMP_Text gameOverText; 
    public GameObject restartButton; 
    private List<Enemy> enemies = new List<Enemy>();
    public int rows = 5;
    public int columns = 10;
    public float baseSpacing = 0.95f; 
    private bool movingRight = true;
    public float moveDownAmount = 0.5f; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnPlayer();
        SpawnEnemiesInGrid();
        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);
    }

    void Update()
    {
        MoveEnemies();
    }

    void SpawnPlayer()
    {
        Vector2 playerPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.1f));
        GameObject player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null && healthText != null)
        {
            playerHealth.SetHealthTextUI(healthText);
        }
    }

    void SpawnEnemiesInGrid()
    {
        Vector2 startPos = spawnPoint.position; 
        for (int row = 0; row < rows; row++)
        {
            float rowOffset = row * 0.05f; 
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(startPos.x + col * baseSpacing - rowOffset, startPos.y - row * baseSpacing);
                GameObject enemyObject = Instantiate(enemyPrefab, position, Quaternion.identity);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                enemies.Add(enemy);
            }
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void EnemyHitWall()
    {
        movingRight = !movingRight;
        foreach (Enemy enemy in enemies)
        {
            enemy.transform.position += new Vector3(0, -moveDownAmount, 0);
            enemy.initialSpeed += 1f; 
        }
    }

    void MoveEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Move(movingRight);
        }
    }

    public void GameOver()
    {
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        gameOverText.gameObject.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
