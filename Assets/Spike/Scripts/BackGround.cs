using UnityEngine;

public class BackGround : MonoBehaviour
{
    public SpecialEffectAnimation specialEffectAnimationPrefab;
    public GameManager gameManager;
    private float[] ssTime = new float[8];
    private float[] ssTimeMax = new float[8];
    public void Start()
    {
        InvokeRepeating(nameof(SpawnYanhua), 20, 20);
        for (int i = 0; i < 8; i++)
        {
            if (gameManager.emotionalQuantity[i] == 0)
            {
                ssTimeMax[i] = 9999999;
            }
            else
            {
                if (gameManager.emotionalQuantity[i] > 10)
                {
                    ssTimeMax[i] = 1;
                }
                else
                {
                    ssTimeMax[i] = 11 - gameManager.emotionalQuantity[i];
                }
                ssTimeMax[i] *= 5;
                ssTimeMax[i] += Random.Range(-1f, 1f);
            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < ssTime.Length; i++)
        {
            ssTime[i] += Time.deltaTime;
        }
        if (ssTime[0] > ssTimeMax[0])
        {
            ssTime[0] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_1 = true;
        }
        if (ssTime[1] > ssTimeMax[1])
        {
            ssTime[1] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_2 = true;
        }
        if (ssTime[2] > ssTimeMax[2])
        {
            ssTime[2] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_3 = true;
        }
        if (ssTime[3] > ssTimeMax[3])
        {
            ssTime[3] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_4 = true;
        }
        if (ssTime[4] > ssTimeMax[4])
        {
            ssTime[4] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_5 = true;
        }
        if (ssTime[5] > ssTimeMax[5])
        {
            ssTime[5] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_6 = true;
        }
        if (ssTime[6] > ssTimeMax[6])
        {
            ssTime[6] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_7 = true;
        }
        if (ssTime[7] > ssTimeMax[7])
        {
            ssTime[7] = 0;
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f)), Quaternion.identity);
            specialEffectAnimation.ss_8 = true;
        }
    }
    public void SpawnYanhua()
    {
        int a = Random.Range(1, 3);
        for (int i = 0; i < a; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * 15;
            Vector3 spawnPoint = transform.position + spawnDirection;
            float angle = 0;

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

            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, spawnPoint, Quaternion.identity);
            specialEffectAnimation.direction = newDirection.normalized;
            specialEffectAnimation.yanhua_first = true;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            specialEffectAnimation.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 90);
            //enemy.size = Random.Range(enemy.minSize, enemy.maxSize);

            //enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
