using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private int startingScore = 0;
    private int score = 0;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private int startingLives = 5;
    private int lives = 0;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private int startingLevel = 1;
    private int level = 0;

    [SerializeField]
    private Text levelText;


    private void Awake()
    {
        GameSystem[] gameSystems = FindObjectsOfType<GameSystem>();

        if (gameSystems.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        score = startingScore;
        lives = startingLives;
        level = startingLevel;
        UpdateScore();
        UpdateLives();
        UpdateLevel();
    }

    public void IncreaseScore(int extraScore)
    {
        score += extraScore * level;
        UpdateScore();
    }

    public void DecreaseLives()
    {
        lives--;
        UpdateLives();
    }

    public void IncreaseLevel()
    {
        level++;
        UpdateLevel();
    }

    public int GetCurrentLives()
    {
        return lives;
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateLives()
    {
        livesText.text = lives.ToString();
    }

    private void UpdateLevel()
    {
        levelText.text = level.ToString();
    }
}
