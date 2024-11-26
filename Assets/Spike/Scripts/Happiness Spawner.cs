using UnityEngine;

public class HappinessSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Happiness happinessPrefab;
    private float spawnRate = 15f;
    private int spawnAmount = 1;
    private int startAmount = 0;
    private float spawnDistance = 15.0f;
    private float angle;

    private void Start()
    {
        if (gameManager.emotionalQuantity[0] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[0]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[0]) < 7)
            {
                spawnRate = 14;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[0]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[0]) < 13)
            {
                spawnRate = 13;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[0]) >= 13 && Mathf.Abs(gameManager.emotionalQuantity[0]) < 19)
            {
                spawnRate = 12;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[0]) >= 19)
            {
                spawnRate = 11.5f;
            }
        }
        for (int i = 0; i < startAmount; i++)
        {
            Invoke(nameof(Spawn),0.5f);
        }
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            while (angle == 0)
            {
                angle = Random.Range(-1, 2) * Random.Range(15f, 25f);
            }

            float angleInRadians = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ó¦ÓÃÐý×ª
            Vector3 newDirection = rotation * -spawnDirection;

            //float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            //Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Happiness happiness = Instantiate(happinessPrefab, spawnPoint, Quaternion.identity);
            happiness.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
