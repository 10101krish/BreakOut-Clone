using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // private Color color = new Color(255, 0, 255, 255);
    private GameManager gameManager;

    private new Rigidbody2D rigidbody2D;
    [SerializeField]
    private float downwardSpeed = 1f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody2D.velocity = Vector2.down * downwardSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Paddle")))
        {
            gameManager.PowerUpHit();
        }
        Destroy(gameObject);
    }
}
