using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private GameManager gameManager;

    private bool ballInPlay;

    public float startingForce = 5f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ResetBall()
    {
        ballInPlay = false;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0;
    }

    public void SetPosition(Vector3 position)
    {
        position.y += 0.5f;
        transform.position = position;
    }

    public void BallInPlay()
    {
        Vector2 force = Vector2.up * startingForce;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        ballInPlay = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            gameManager.BallOutOfPlayZone();
            Destroy(gameObject);
        }
    }
}
