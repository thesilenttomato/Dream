using UnityEngine;
using UnityEngine.UIElements;

public class EmotionComplex : MonoBehaviour
{
    public GameManager gameManager;
    public Transform target;
    public Player player;
    //public InvestigationBullet overloadBulletPrefab;
    public EnemyBullet enemyBulletPrefab;
    private Rigidbody2D _rigidbody;
    public Astonishment astonishmentPrefab;

    /* public float speed = 1.5f;
     private float size = 0.75f;
     public int life = 1;

     public float shootDistance = 5f;
     public float shootInterval = 2.5f;*/

    public BaseUnitData baseUnitData;
    public float[] time = new float[8];

    public Vector3 direction;

    private float existTime;

    public EmotionComplexSpawner emotionComplexSpawner;

    private bool[] shootMode = new bool[8];

    private int bulletScale = 1;
    private int bulletDamage = 1;

    private bool fearShow;
    public SpecialEffectAnimation specialEffectAnimationPrefab;
    //private float restTimeMax = 1;
    //private float restTime = 0;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        target = FindFirstObjectByType<Player>().target;
        player = FindFirstObjectByType<Player>();
        for (int i = 0; i < 8; i++)
        {
            if (gameManager.emotionalQuantity[i] != 0)
            {
            shootMode[i] = true;
            }
        }
        if (shootMode[2])
        {
            bulletScale = 2;
        }
        if (shootMode[6])
        {
            bulletDamage = 1;
        }
        //baseUnitData = new BaseUnitData(1, 1, 1000, 1, 125);
        //_rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;

        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        //Debug.Log(baseUnitData.life);
        //Vector3 direction = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Vector3 shootDirection = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);


        //transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);

        //time += Time.deltaTime;
        //if (player.gameObject.layer == 6)
        //{
        /*float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/

        transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;

        if (direction.x < 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = false;
        }
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }

        for (int i = 0; i < time.Length; i++)
        {
            if (shootMode[i] == true)
            {
                time[i] += Time.deltaTime;
            }
        }

        if (shootMode[0] && time[0] > 0.1f)
        {
            time[0] = 0;
            Shoot_1();
        }
        if (currentDistance >= 6 && shootMode[3])
        {
            if (time[3] >= 12 - 0.3f && fearShow == false && shootMode[3])
            {
                fearShow = true;
                int a = Random.Range(10, 20);
                for (int i = 1; i <= a; i++)
                {
                    SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
                    specialEffectAnimation.fear = true;
                    Vector2 newDirection = Random.insideUnitCircle.normalized;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
                    Vector3 eulerRotation = targetRotation.eulerAngles;
                    specialEffectAnimation.transform.eulerAngles = eulerRotation;
                    specialEffectAnimation.direction = newDirection;
                    //Invoke(nameof(specialEffectAnimation.DestroyGameObject),0.6f); 
                }
            }
            if (time[3] >= 12 && shootMode[3])
            {
                //time += Time.deltaTime;
                fearShow = false;
                //Debug.Log("Sb");
                time[3] = 0;
                player.GetComponent<Rigidbody2D>().AddForce(-shootDirection * 60);
                for (int i = 0; i < player.time.Length; i++)
                {
                    player.time[i] -= 2;
                }

            }
        }

        if (time[4] >= 20 && shootMode[4])
        {
            time[4] = 0;
            for (int i = 0; i < 4; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * 2;
                Vector3 spawnPoint = transform.position + spawnDirection;
                Astonishment astonishment = Instantiate(astonishmentPrefab, spawnPoint, Quaternion.identity);
                astonishment.chargeToDeath = true;
                astonishment.existTimeMax = 9999;
            }
        }
        if (time[6] > 8 && shootMode[6])
        {
            time[6] = 0;
            Shoot_3();
        }
        if (time[7] > 10 && shootMode[7])
        {
            time[7] = 0;
            for (int i = 1; i <= 20; i++)
            {
                Invoke(nameof(Shoot_4), (i - 1) * 0.1f);
            }
        }
        /*if (time >= baseUnitData.attackInterval)
        {
            time = 0;
            //Debug.Log("SB");
            Shoot();
        }*/
        existTime += Time.deltaTime;
        if (existTime >= 15)
        {
            if (transform.position.x > 14 || transform.position.x < -14 || transform.position.y > 8.55f || transform.position.y < -8.55f)
            {
                emotionComplexSpawner.newLife = baseUnitData.life;
                emotionComplexSpawner.disappearPosition = transform.position;
                emotionComplexSpawner.disappearState = true;
                emotionComplexSpawner.time = time;
                Destroy(gameObject);
                if (shootMode[5])
                {
                    SuperShameSpawner superShameSpawner = FindFirstObjectByType<SuperShameSpawner>();
                    int a = Random.Range(0, 3);
                    if (a == 0)
                    {
                        superShameSpawner.count += superShameSpawner.countMax;
                    }
                }

            }
        }
    }

    private void Shoot_1()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        for (int i = 1; i <= 1; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[0];
            enemyBullet.transform.localScale = new Vector3(enemyBullet.transform.localScale.x * bulletScale, enemyBullet.transform.localScale.y * bulletScale);
            enemyBullet.speed = 500;
            Vector3 direction = Random.insideUnitCircle.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 135);
            enemyBullet.Project(direction);
            enemyBullet.bulletType = 2;
            enemyBullet.damage = bulletDamage;
        }
    }

    private void Shoot_2()
    {
        float angle = 120;
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        for (int i = 0; i < 3; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[1];
            enemyBullet.speed = baseUnitData.bulletSpeed;
            enemyBullet.transform.localScale = new Vector3(enemyBullet.transform.localScale.x * bulletScale, enemyBullet.transform.localScale.y * bulletScale);
            float angleInRadians = angle * i;
            Quaternion rotation = Quaternion.AngleAxis(angleInRadians, Vector3.forward);
            Vector3 newDirection = rotation * randomDirection;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 135);

            //Debug.Log(newDirection);
            enemyBullet.Project(newDirection.normalized);
            enemyBullet.damage = bulletDamage;
        }
    }
    private void Shoot_3()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        for (int i = 1; i <= 3; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[4];
            enemyBullet.speed = baseUnitData.bulletSpeed * 1.5f;
            enemyBullet.transform.localScale = new Vector3(enemyBullet.transform.localScale.x * bulletScale, enemyBullet.transform.localScale.y * bulletScale);
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = 0;
            angle = Random.Range(-10, 10f);
            float angleInRadians = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotation * direction.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 90);
            enemyBullet.Project(rotation * direction);
            enemyBullet.damage = bulletDamage;
            //enemyBullet.bulletType = 1;
        }
    }
    private void Shoot_4()
    {
        EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyBullet.sprite[5];
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector2.left.normalized);
        Vector3 eulerRotation = targetRotation.eulerAngles;
        enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 180);
        enemyBullet.speed = baseUnitData.bulletSpeed;
        enemyBullet.transform.localScale = new Vector3(enemyBullet.transform.localScale.x * bulletScale, enemyBullet.transform.localScale.y * bulletScale);
        Vector3 direction = (target.position - transform.position).normalized;
        enemyBullet.Project(direction);
        enemyBullet.damage = bulletDamage;
        //enemyBullet.bulletType = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //baseUnitData.life--;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            float a = Random.Range(0f, 256f);
            float b = Random.Range(0f, 256f);
            float c = Random.Range(0f, 256f);
            gameManager.Explosive(collision.GetContact(0).point, new Color(a / 255f, b / 255f, 36f / c, 1.0f));
            if (shootMode[1])
            {
                Shoot_2();
            }
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                //gameManager.defeatedEmotion[0] += 1;
                gameManager.ifbossDefeadedCheck = true;
                Destroy(gameObject);
            }
        }
        /*if (collision.gameObject.layer == 6)
        {
            FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            Destroy(gameObject);
        }*/
    }

    /*private void OnDestroy()
    {
        GameManager.destroyCount++;
    }*/
}
