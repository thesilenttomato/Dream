using DG.Tweening.Core.Easing;
using UnityEngine;

public class CalmnessSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Calmness calmnessPrefab;
    private float spawnRate = 20;
    private int spawnAmount = 1;
    private int startAmount = 0;
    private float spawnDistance = 15.0f;
    private float angle;

    private void Start()
    {
        spawnRate = 18.8f + gameManager.totalKind * 1.2f;
        if (gameManager.emotionalQuantity[2] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 2 && Mathf.Abs(gameManager.emotionalQuantity[2]) < 4)
            {
                spawnRate -= 2;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 4 && Mathf.Abs(gameManager.emotionalQuantity[2]) < 7)
            {
                spawnRate -= 4;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[2]) < 10)
            {
                spawnRate -= 6;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 10)
            {
                spawnRate -= 8;
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

            /*while (angle == 0)
            {
                angle = Random.Range(-1, 2) * Random.Range(15f, 25f);
            }*/

            float angleInRadians = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ó¦ÓÃÐý×ª
            Vector3 newDirection = rotation * -spawnDirection;

            //float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            //Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Calmness calmness = Instantiate(calmnessPrefab, spawnPoint, Quaternion.identity);
            calmness.direction = newDirection.normalized;
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
