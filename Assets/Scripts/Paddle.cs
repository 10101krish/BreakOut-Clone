using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float paddleLength = 2f;

    private float leftMargin;
    private float rightMargin;

    private void Awake()
    {
        leftMargin = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        rightMargin = Camera.main.ViewportToWorldPoint(Vector3.right).x;
    }

    private void Start()
    {
        UpdatePaddleSize();
    }

    private void Update()
    {
        MoveMouse();
    }

    private void MoveMouse()
    {
        Vector3 newPosotion = Camera.main.ScreenToWorldPoint(new(Input.mousePosition.x, 0, 0));
        newPosotion.x = Mathf.Clamp(newPosotion.x, leftMargin + paddleLength / 2, rightMargin - paddleLength / 2);
        newPosotion.y = transform.position.y;
        newPosotion.z = 0;
        transform.position = newPosotion;
    }

    private void UpdatePaddleSize()
    {
        transform.localScale = new Vector3(paddleLength, transform.localScale.y, transform.localScale.z);
    }

}
