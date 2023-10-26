using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        score = 0;
        UpdateScore();
    }

    public void IncreaseScore(int extraScore)
    {
        score += extraScore;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

}
