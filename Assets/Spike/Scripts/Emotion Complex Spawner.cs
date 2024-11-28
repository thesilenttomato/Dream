using Unity.VisualScripting;
using UnityEngine;

public class EmotionComplexSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public EmotionComplex emotionComplexPrefab;
    //private float spawnRate = 15f;
    private int spawnAmount = 1;
    private int startAmount = 1;
    private float spawnDistance = 16f;
    private float angle;

    public Vector3 disappearPosition;
    public float newLife;
    public bool disappearState = false;
    public float[] time = new float[8];
    private void Start()
    {
        if (gameManager.bossFight == false)
        {
            startAmount = 0;
            Destroy(gameObject);
        }
        for (int i = 0; i < startAmount; i++)
        {
            Invoke(nameof(Spawn), 0.5f);
        }
        //InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }
    public void Update()
    {
        if (disappearState == true)
        {
            NextSpawn();
            disappearState = false;
        }
    }

    private void NextSpawn()
    {
        while (angle == 0)
        {
            angle = Random.Range(-1, 2) * Random.Range(15f, 25f);
        }
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newDirection = rotation * -(disappearPosition - transform.position).normalized;
        EmotionComplex emotionComplex = Instantiate(emotionComplexPrefab, disappearPosition, Quaternion.identity);
        emotionComplex.direction = newDirection.normalized;
        emotionComplex.emotionComplexSpawner = this;
        emotionComplex.time = time;
        emotionComplex.baseUnitData = new BaseUnitData(newLife, 1, 1000, 1, 125);
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

            //float angleInRadians = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 newDirection = rotation * -spawnDirection;

            //float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            //Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            EmotionComplex emotionComplex = Instantiate(emotionComplexPrefab, spawnPoint, Quaternion.identity);
            emotionComplex.direction = newDirection.normalized;
            emotionComplex.emotionComplexSpawner = this;
            int sum = 0;
            foreach (int value in gameManager.emotionalQuantity)
            {
                sum += value;
            }
            emotionComplex.baseUnitData = new BaseUnitData(100 - sum, 1, 1000, 1, 125);
            //emotionComplex.baseUnitData.life = 100 - sum * 5;
            if (emotionComplex.baseUnitData.life < 50)
            {
                emotionComplex.baseUnitData.life = 50;
            }
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
