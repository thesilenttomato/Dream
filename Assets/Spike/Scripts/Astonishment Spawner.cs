using DG.Tweening.Core.Easing;
using UnityEngine;

public class AstonishmentSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Astonishment astonishmentPrefab;
    private float spawnRate = 18;
    private int spawnAmount = 1;
    private int startAmount = 1;
    private float spawnDistance = 15.0f;
    private float existTimeMax = 30;
    private float angle;

    private void Start()
    {
        spawnRate = 14 + gameManager.totalKind * 1f;
        if (gameManager.emotionalQuantity[4] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[4]) >= 2 && Mathf.Abs(gameManager.emotionalQuantity[4]) < 4)
            {
                spawnRate -= 2;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[4]) >= 4 && Mathf.Abs(gameManager.emotionalQuantity[4]) < 7)
            {
                spawnRate -= 4;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[4]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[4]) < 10)
            {
                spawnRate -= 6;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[4]) >= 10)
            {
                spawnRate -= 7.5f;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[4]) >= 11)
            {
                existTimeMax = 40;
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

            // Ӧ����ת
            //Vector3 newDirection = rotation * -spawnDirection;

            //float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            //Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Astonishment astonishment = Instantiate(astonishmentPrefab, spawnPoint, Quaternion.identity);
            astonishment.existTimeMax = existTimeMax;
            //happiness.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
