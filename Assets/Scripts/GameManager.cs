using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Paddle paddle;

    public GameObject ballPrefab;
    public GameObject ballsParentGameObject;

    private Ball currentMainBall;
    private int ballCount = 0;

    private bool ballInPlay;

    private void Start()
    {
        NewBall();
    }

    private void Update()
    {
        if (!ballInPlay)
            currentMainBall.SetPosition(paddle.gameObject.transform.position);
        if (!ballInPlay && Input.GetMouseButtonDown(0))
            FreeBallFromPaddle();
    }

    private void NewBall()
    {
        InstantiateNewBall();
        ballInPlay = false;
        currentMainBall.ResetBall();
    }

    private void InstantiateNewBall()
    {
        ballCount++;
        GameObject newBall = Instantiate(ballPrefab, paddle.gameObject.transform.position, quaternion.identity);
        newBall.transform.parent = ballsParentGameObject.transform;
        currentMainBall = newBall.GetComponent<Ball>();
    }

    public void BallOutOfPlayZone()
    {
        ballCount--;
        if (ballCount <= 0)
            NewBall();
    }

    private void FreeBallFromPaddle()
    {
        ballInPlay = true;
        currentMainBall.BallInPlay();
    }
}
