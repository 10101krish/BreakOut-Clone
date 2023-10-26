using UnityEngine;

public class Ball : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private GameManager gameManager;

    private bool ballInPlay;

    public float startingForce = 5f;
    public float randomFactor = 1f;

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
        AddForceToBall(force);
    }

    public void BallInstaiatedThroughPowerUp()
    {
        Vector2 force = Random.insideUnitCircle * startingForce;
        AddForceToBall(force);
    }

    private void AddForceToBall(Vector2 force)
    {
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        ballInPlay = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            gameManager.BallOutOfPlayZone(this);
            Destroy(gameObject);
        }
        else if (ballInPlay)
        {
            Vector2 normal = other.GetContact(0).normal;
            rigidbody2D.AddForce(Random.insideUnitCircle * randomFactor);

            // float angle = UnityEngine.Random.Range(-75, 75);
            // normal.Normalize();
            // Vector2 randomForce = normal * (1 / Mathf.Cos(angle * Mathf.Deg2Rad)) * randomFactor;
            // Debug.Log(randomForce);
        }
    }

    // Vector2 normal = other.contacts[0].normal;
    // float inclinationAngle = Vector2.SignedAngle(normal, rigidbody2D.velocity);

    // float alterredAngle = Mathf.Clamp(UnityEngine.Random.Range(inclinationAngle - angleDeviation, inclinationAngle + angleDeviation), -maxBounceAngle, maxBounceAngle);
    // Quaternion rotation = Quaternion.AngleAxis(alterredAngle, normal);
    // Vector2 reflectedVelocity = rotation * normal * rigidbody2D.velocity.magnitude;
    // rigidbody2D.velocity = reflectedVelocity;

}
