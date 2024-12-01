using UnityEngine;

public class BackGround : MonoBehaviour
{
    public SpecialEffectAnimation specialEffectAnimationPrefab;
    public void Start()
    {
        InvokeRepeating(nameof(SpawnYanhua), 20, 20);
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
