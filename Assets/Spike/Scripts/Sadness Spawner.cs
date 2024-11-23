using UnityEngine;

public class SadnessSpawner : MonoBehaviour
{
    public Sadness sadnessPrefab;
    private float spawnRate = 18;
    private int spawnAmount = 1;
    private int startAmount = 1;
    private float spawnDistance = 15.0f;
    private float angle;

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
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            //while (angle == 0)
            //{
            //    angle = Random.Range(-1, 2) * Random.Range(15f, 25f);
            //}

            //float angleInRadians = angle * Mathf.Deg2Rad;
            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ó¦ÓÃÐý×ª
            //Vector3 newDirection = rotation * -spawnDirection;

            //float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            //Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Sadness sadness = Instantiate(sadnessPrefab, spawnPoint, Quaternion.identity);
            //happiness.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
