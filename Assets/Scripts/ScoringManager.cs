using UnityEngine;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager Instance { get; private set; }

    public TMP_Text scoreText;
    private int score = 0;

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

    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
