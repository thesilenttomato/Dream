using DG.Tweening.Core.Easing;
using UnityEngine;

public class HateSpawner : MonoBehaviour
{
    public GameManager gameManager;
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
        spawnRate = 38 + gameManager.totalKind * 2;
        if (gameManager.emotionalQuantity[7] == 0)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[7]) < 7)
            {
                spawnRate -= 4;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 7 && Mathf.Abs(gameManager.emotionalQuantity[7]) < 13)
            {
                spawnRate -= 8;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 13 && Mathf.Abs(gameManager.emotionalQuantity[7]) < 19)
            {
                spawnRate -= 12;
            }
            if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 19)
            {
                spawnRate -= 16;
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
