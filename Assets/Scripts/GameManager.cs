using System.Reflection;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Paddle paddle;

    public GameObject ballPrefab;
    public GameObject ballsParentGameObject;

    public GameObject brickParentGameObject;
    private int brickCount;

    public Ball currentMainBall;
    private int ballCount = 0;
    public int maxBallCount = 5;

    private bool ballInPlay;

    public int brickHitScore = 50;
    public int brickDestroyedScore = 100;

    private GameSystem gameSystem;
    public bool autoplayEnabled = false;

    private System.Action[] actions;

    private void Awake()
    {
        gameSystem = FindObjectOfType<GameSystem>();
    }

    private void Start()
    {
        actions = new System.Action[] { paddle.IncreasePaddleSize, paddle.DecreasePaddleSize, this.MultipleBallsPowerUp };
        CountNumberOfBricks();
        NewBall();
    }

    private void Update()
    {
        if (!ballInPlay)
            currentMainBall.SetPosition(paddle.gameObject.transform.position);
        if (!ballInPlay && Input.GetMouseButtonDown(0))
            FreeBallFromPaddle();
        if (ballInPlay && autoplayEnabled)
        {
            paddle.autoPlay = true;
            if (currentMainBall == null)
                FindNewBall();
            paddle.gameObject.transform.position = new Vector3(currentMainBall.gameObject.transform.position.x, paddle.gameObject.transform.position.y, paddle.gameObject.transform.position.z);
        }
    }

    private void CountNumberOfBricks()
    {
        Brick[] bricks = brickParentGameObject.GetComponentsInChildren<Brick>();
        brickCount = bricks.Length;
    }

    private void FindNewBall()
    {
        currentMainBall = ballsParentGameObject.GetComponentsInChildren<Ball>()[0];
    }

    private void NewBall()
    {
        ballCount++;
        InstantiateNewBall(paddle.gameObject.transform.position);
        ballInPlay = false;
        currentMainBall.ResetBall();
    }

    private Ball InstantiateNewBall(Vector3 currPosition)
    {
        GameObject newBall = Instantiate(ballPrefab, currPosition, quaternion.identity);
        newBall.transform.parent = ballsParentGameObject.transform;
        currentMainBall = newBall.GetComponent<Ball>();
        return currentMainBall;
    }

    public void BallOutOfPlayZone(Ball ball)
    {
        ballCount--;
        if (currentMainBall = ball)
            currentMainBall = null;
        if (ballCount <= 0)
            LifeLost();
    }

    private void LifeLost()
    {
        gameSystem.DecreaseLives();
        if (gameSystem.GetCurrentLives() > 0)
            NewBall();
        else
        {
            Debug.Log("Game Lost");
            Destroy(gameSystem.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FreeBallFromPaddle()
    {
        ballInPlay = true;
        currentMainBall.BallInPlay();
    }

    public void BrickHit()
    {
        gameSystem.IncreaseScore(brickHitScore);
    }

    public void BrickDestroyed()
    {
        gameSystem.IncreaseScore(brickDestroyedScore);
        brickCount--;
        if (brickCount == 0)
        {
            gameSystem.IncreaseLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PowerUpHit()
    {
        int randomIndex = UnityEngine.Random.Range(0, actions.Length);
        actions[randomIndex]();
    }

    private void MultipleBallsPowerUp()
    {
        int extraBalls = UnityEngine.Random.Range(1, maxBallCount - ballCount + 1);
        ballCount += extraBalls;
        for (int i = 1; i <= extraBalls; i++)
            InstantiateNewBall(currentMainBall.gameObject.transform.position).BallInstaiatedThroughPowerUp();
    }
}
