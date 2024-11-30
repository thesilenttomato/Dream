using UnityEngine;

public class FearSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Fear fearPrefab;
    private float spawnRate = 25;
    private int spawnAmount = 1;
    private int startAmount = 0;
    private float spawnDistance = 15.0f;
    private float existTimeMax = 40;
    private float angle;

    private void Start()
    {
        spawnRate = 23.5f + gameManager.totalKind * 1.5f;
        if (gameManager.emotionalQuantity[3] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[3]) < 7)
            {
                spawnRate -= 3.5f;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[3]) < 13)
            {
                spawnRate -= 6;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 13 && Mathf.Abs(gameManager.emotionalQuantity[3]) < 19)
            {
                spawnRate -= 7.5f;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 19)
            {
                spawnRate -= 9;
            }
        }
        for (int i = 0; i < startAmount; i++)
        {
            Invoke(nameof(Spawn), 0.5f);
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

            Fear fear = Instantiate(fearPrefab, spawnPoint, Quaternion.identity);
            fear.existTimeMax = existTimeMax;
            //happiness.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
