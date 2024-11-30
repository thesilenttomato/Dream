using DG.Tweening.Core.Easing;
using UnityEngine;

public class AngerSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Anger angerPrefab;
    private float spawnRate = 40;
    private int spawnAmount = 1;
    private int startAmount = 0;
    private float spawnDistance = 15.0f;
    private float angle;

    private void Start()
    {
        spawnRate = 38 + gameManager.totalKind * 2;
        if (gameManager.emotionalQuantity[6] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 2 && Mathf.Abs(gameManager.emotionalQuantity[6]) < 4)
            {
                spawnRate -= 5;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 4 && Mathf.Abs(gameManager.emotionalQuantity[6]) < 7)
            {
                spawnRate -= 10;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[6]) < 10)
            {
                spawnRate -= 14;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 10)
            {
                spawnRate -= 18;
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

            Anger anger = Instantiate(angerPrefab, spawnPoint, Quaternion.identity);
            anger.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
