using UnityEngine;

public class SuperShameSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public SuperShame superShamePrefab;
    //private float spawnRate = 12;
    private int spawnAmount = 1;
    //private int startAmount = 0;
    private float angle;

    public int count = 0;
    private int countMax = 6;

    private Vector3 pointA = new Vector3(-13.5f, 6.55f, 0.0f);
    private Vector3 pointB = new Vector3(-13.5f, -6.55f, 0.0f);
    private Vector3 pointC = new Vector3(13.5f, 6.55f, 0.0f);
    private Vector3 pointD = new Vector3(13.5f, -6.55f, 0.0f);

    private void Start()
    {
        if (gameManager.emotionalQuantity[5] >= 11)
        {
            countMax = 5;
        }
        /*for (int i = 0; i < startAmount; i++)
        {
            Spawn();
        }
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);*/
    }
    private void Update()
    {
        if (count >= countMax)
        {
            Spawn();
            count = 0;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                float randomY = Random.Range(pointA.y, pointB.y);
                Vector3 spawnPoint = new Vector3(pointA.x, randomY, 0.0f);

                SuperShame shame = Instantiate(superShamePrefab, spawnPoint, Quaternion.identity);
            }
            else
            {
                float randomY = Random.Range(pointC.y, pointD.y);
                Vector3 spawnPoint = new Vector3(pointC.x, randomY, 0.0f);

                SuperShame shame = Instantiate(superShamePrefab, spawnPoint, Quaternion.identity);
            }

        }
    }
}
