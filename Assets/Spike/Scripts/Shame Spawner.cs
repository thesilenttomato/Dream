using UnityEngine;

public class ShameSpawner : MonoBehaviour
{
    public Shame shamePrefab;
    private float spawnRate = 12;
    private int spawnAmount = 1;
    private int startAmount = 4;
    private float angle;

    private Vector3 pointA = new Vector3(-13.0f, 6.55f, 0.0f);
    private Vector3 pointB = new Vector3(-13.0f, -6.55f, 0.0f);
    private Vector3 pointC = new Vector3(13.0f, 6.55f, 0.0f);
    private Vector3 pointD = new Vector3(13.0f, -6.55f, 0.0f);

    private void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            Spawn();
        }
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                float randomY = Random.Range(pointA.y, pointB.y);
                Vector3 spawnPoint = new Vector3(pointA.x, randomY, 0.0f);

                Shame shame = Instantiate(shamePrefab, spawnPoint, Quaternion.identity);
            }
            else
            {
                float randomY = Random.Range(pointC.y, pointD.y);
                Vector3 spawnPoint = new Vector3(pointC.x, randomY, 0.0f);

                Shame shame = Instantiate(shamePrefab, spawnPoint, Quaternion.identity);
            }

        }
    }
}
