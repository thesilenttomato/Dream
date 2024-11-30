using DG.Tweening.Core.Easing;
using UnityEngine;

public class ShameSpawner : MonoBehaviour
{
    public GameManager gameManager;
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
        spawnRate = 11.2f + gameManager.totalKind * 0.8f;
        if (gameManager.emotionalQuantity[5] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[5]) < 7)
            {
                spawnRate -= 1;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[5]) < 13)
            {
                spawnRate -= 1.5f;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 13 && Mathf.Abs(gameManager.emotionalQuantity[5]) < 19)
            {
                spawnRate -= 2;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 19)
            {
                spawnRate -= 2.5f;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 17)
            {
                startAmount = 6;
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
