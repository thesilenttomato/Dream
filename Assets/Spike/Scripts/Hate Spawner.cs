using UnityEngine;

public class HateSpawner : MonoBehaviour
{
    public Hate hateSpawner;
    private float spawnRate = 45;
    private int spawnAmount = 1;
    private int startAmount = 1;
    private float angle;

    private Vector3 pointA = new Vector3(-12.0f, 7.55f, 0.0f);
    private Vector3 pointB = new Vector3(-12.0f, -7.55f, 0.0f);
    private Vector3 pointC = new Vector3(12.0f, 7.55f, 0.0f);
    private Vector3 pointD = new Vector3(12.0f, -7.55f, 0.0f);

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
                float randomX = Random.Range(pointA.x, pointC.x);
                Vector3 spawnPoint = new Vector3(randomX, pointA.y, 0.0f);

                Hate shame = Instantiate(hateSpawner, spawnPoint, Quaternion.identity);
            }
            else
            {
                float randomX = Random.Range(pointB.x, pointD.x);
                Vector3 spawnPoint = new Vector3(randomX, pointD.y, 0.0f);

                Hate shame = Instantiate(hateSpawner, spawnPoint, Quaternion.identity);
            }

        }
    }
}
