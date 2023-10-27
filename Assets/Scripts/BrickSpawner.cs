using Unity.Mathematics;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brickPrefabs;

    [SerializeField]
    private float brickSpawnCoefficient = 0.25f;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InstantiateBrickSystem();
        gameObject.SetActive(false);
    }

    private void InstantiateBrickSystem()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1f);
        if (randomNumber >= brickSpawnCoefficient)
            SpawnBrick();
    }

    private void SpawnBrick()
    {
        int randomIndex = UnityEngine.Random.Range(0, brickPrefabs.Length);
        GameObject gameObject = Instantiate(brickPrefabs[randomIndex], transform.position, quaternion.identity);
        if (gameObject.layer.Equals(LayerMask.NameToLayer("Bricks")))
            gameManager.BrickSpawnedBySpawner();
    }

}
