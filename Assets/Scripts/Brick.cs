using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    private readonly Color[] brickColors = { Color.green, Color.yellow, Color.red };
    public int currentHealth = 3;

    public GameObject powerUpPrefab;
    public float randomPowerUpCoefficient = 0.1f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeBrickColor();
    }

    private void ChangeBrickColor()
    {
        spriteRenderer.color = brickColors[currentHealth - 1];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            currentHealth--;
            PowerUpSpawner();
            if (currentHealth == 0)
            {
                gameManager.BrickDestroyed();
                Destroy(gameObject);
            }
            else
            {
                gameManager.BrickHit();
                ChangeBrickColor();
            }
        }
    }

    public void DestoyedByBomb()
    {
        gameManager.BrickDestroyed();
        Destroy(gameObject);
    }

    private void PowerUpSpawner()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber <= randomPowerUpCoefficient)
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
    }
}
