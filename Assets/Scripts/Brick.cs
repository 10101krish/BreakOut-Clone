using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    private Color[] brickColors = { Color.green, Color.yellow, Color.red };
    public int currentHealth = 3;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
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
        if (other.gameObject.tag == "Ball")
        {
            currentHealth--;
            if (currentHealth == 0)
                Destroy(gameObject);
            else
                ChangeBrickColor();
        }
    }
}
