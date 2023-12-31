using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float bombAreaLength = 4f;
    [SerializeField]
    private float bombAreabredth = 4f;

    private Collider2D[] collider2Ds;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")))
            DetonateBomb();
    }

    private void DetonateBomb()
    {
        gameObject.SetActive(false);

        collider2Ds = new Collider2D[8];
        Vector2 pointA = new Vector2(transform.position.x - bombAreaLength / 2, transform.position.y + bombAreabredth / 2);
        Vector2 pointB = new Vector2(transform.position.x + bombAreaLength / 2, transform.position.y - bombAreabredth / 2);
        int numColliders = Physics2D.OverlapAreaNonAlloc(pointA, pointB, collider2Ds);

        if (numColliders > 0)
        {
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (!collider2Ds[i])
                    continue;
                if (collider2Ds[i].gameObject.layer.Equals(LayerMask.NameToLayer("Bricks")))
                    collider2Ds[i].gameObject.GetComponent<Brick>().DestoyedByBomb();
                // else if (collider2Ds[i].gameObject.layer.Equals(LayerMask.NameToLayer("Bomb")))
                //     collider2Ds[i].gameObject.GetComponent<Bomb>().DetonateBomb();
            }
        }

        Destroy(gameObject);
    }
}
