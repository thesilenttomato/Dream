using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //public ParticleSystem explosion;
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer_me;
    public SpriteRenderer spriteRenderer_other;
    private SpriteRenderer spriteRenderer;
    public Transform target;
    public Animator animator;
    public Sprite[] directionSprite;
    //public Sprite sprite2;

    //public Text propCondition;
    // public GameObject propConditionGO;

    /*private float shootInterval_1 = 0.25f;
    private float time_1;
    private float damage_1 = 0;

    private float shootInterval_2 = 0.25f;
    private float time_2;
    private float damage_2 = 0;*/

    public float[] time = new float[8];
    private float[] damage = new float[8] { 2, 1, 1, 1, 1, 2, 1, 1 };
    private float[] shootInterval = new float[8] { 0.5f, 1, 0.25f, 0.6f, 0.35f, 0.6f, 0.12f, 0.6f };

    private int count_1;
    private int count_1Max = 10;
    private float big_1damage = 2;
    private float big_1scale = 6;
    private int bullet_2Min = 4;
    private int bullet_2Max = 6;
    private int bullet_2Scale = 1;
    private int letterCount = 0;
    private int letterBdamage = 1;
    private int letterCScale = 1;
    private int bullet_4amount = 3;
    private int bullet_4angle = 10;
    private int bullet_4shootTime = 1;
    private float clockTime = 0;
    private float clockTimeMax = 80;
    private int bullet_6Mass = 20;
    private int bullet_6Scale = 3;
    private float bullet_7MaxLifetime = 1.3f;
    private float bullet_7Speed = 500;
    public Books booksPrefab;
    private float withstandTime = 0;
    public int withstandCount = 0;
    private int withstandCountMax = 999999;

    public float thrustSpeed = 1.25f;
    public float turnSpeed = 0.1f;
    private bool _thrusting;
    private bool _disThrusting;
    private float _turnDirection;

    public float collisionStrength = 200.0f;
    public float collisionRange = 4f;

    public float bounceStrength = 50f;

    private float invincibleTime = 5;

    // public Sprite spriteDirection;
    /*public Frost frost;
    public bool ifFrost = false;
    public int frostTime = 1;
    */

    /*private float straightSpeedMult = 1;
    private float turnSpeedMult = 1;
    private float shootIntervalMult = 1;
    private float straightSpeedPlus = 0;
    private float turnSpeedPlus = 0;
    private float shootIntervalPlus = 0;
    private float preStraightSpeedMult = 1;
    private float preTurnSpeedMult = 1;
    private float preShootIntervalMult = 1;*/
    //private float preStraightSpeedPlus = 0;
    //private float preTurnSpeedPlus = 0;
    //private float preShootIntervalPlus = 0;

    //private float mult = 1.0f;

    /*private bool ifProp = false;
    private int killNeed;
    private int preDestroyedEnemy;
    private float propCount = 0;
    private float CD;*/

    private Rigidbody2D _rigidbody;

    public Bullet bulletPerfab;
    /*public ReturnedBullet returnedBulletPrefab;
    public TraceableBullet traceableBulletPrefab;
    public WallBullet wallBulletPrefab;
    public ExplosiveBullet explosiveBulletPrefab;
    public TriangleBullet triangularBulletPrefab;

    public Mine minePrefab;*/

    //private PlayerCards playerCards;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (gameManager.playerType[0])
        {
            animator.enabled = false;
            SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = directionSprite[0];
        }
        if (gameManager.playerType[1])
        {
            animator.enabled = false;
            SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = directionSprite[1];
        }
        if (gameManager.playerType[3])
        {
            animator.enabled = false;
            SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = directionSprite[2];
        }
        //animator.SetBool("type_1", gameManager.playerType[0]);
        //animator.SetBool("type_2", gameManager.playerType[1]);
        animator.SetBool("type_3", gameManager.playerType[2]);
        //animator.SetBool("type_4", gameManager.playerType[3]);
        if (gameManager.playerType[2])
        {
            spriteRenderer = spriteRenderer_me;
        }
        else
        {
            spriteRenderer_me.sortingOrder = -1;
            spriteRenderer = spriteRenderer_other;
        }
        /*thrustSpeed = thrustSpeed * (1 + straightSpeedPlus);
        turnSpeed = turnSpeed * (1 + turnSpeedPlus);
        //FindShootInterval();
        shootInterval = shootInterval * (1 + shootIntervalPlus);*/
        //Buff();
        /*if (playerCards.bullet[1] == true)
        {
            shootInterval = 0.5f;
        }
        if (playerCards.bullet[2] == true)
        {
            shootInterval = 0.35f;
        }
        if (playerCards.bullet[3] == true)
        {
            shootInterval = 0.8f;
        }
        if (playerCards.bullet[4] == true)
        {
            if (playerCards.bulletSpecial[0] == true)
            {
                shootInterval = 1.2f;
            }
            else
            {
                shootInterval = 0.7f;
            }
        }*/
        for (int i = 0; i < time.Length; i++)
        {
            time[i] = shootInterval[i];
        }

        if (gameManager.bulletType[0, 1])
        {
            count_1Max = 8;
        }
        if (gameManager.bulletType[0, 2])
        {
            count_1Max = 5;
        }
        if (gameManager.bulletType[0, 3])
        {
            big_1scale = 8;
        }
        if (gameManager.bulletType[0, 4])
        {
            big_1scale = 9;
            big_1damage = 3;
        }

        if (gameManager.bulletType[1, 1])
        {
            bullet_2Max = 8;
        }
        if (gameManager.bulletType[1, 2])
        {
            bullet_2Max = 10;
        }
        if (gameManager.bulletType[1, 3])
        {
            bullet_2Scale = 2;
        }
        if (gameManager.bulletType[1, 4])
        {
            bullet_2Scale = 3;
        }

        if (gameManager.bulletType[2, 1])
        {
            letterCScale = 2;
        }
        if (gameManager.bulletType[2, 2])
        {
            letterCScale = 2;
            letterBdamage = 2;
        }

        if (gameManager.bulletType[3, 1])
        {
            shootInterval[3] = 0.9f;
            bullet_4amount = 5;
            bullet_4angle = 8;
        }
        if (gameManager.bulletType[3, 2])
        {
            shootInterval[3] = 1.3f;
            bullet_4amount = 7;
            bullet_4angle = 6;
        }
        if (gameManager.bulletType[3, 3])
        {
            bullet_4shootTime = 2;
            shootInterval[3] = 1f;
        }
        if (gameManager.bulletType[3, 4])
        {
            bullet_4shootTime = 3;
            shootInterval[3] = 1.3f;
        }

        if (gameManager.bulletType[4, 1])
        {
            clockTimeMax = 65;
        }
        if (gameManager.bulletType[4, 2])
        {
            clockTimeMax = 45;
        }
        if (gameManager.bulletType[4, 3])
        {
            clockTimeMax = 55;
            shootInterval[4] = 0.1f;
        }
        if (gameManager.bulletType[4, 4])
        {
            clockTimeMax = 75;
            shootInterval[4] = 0.1f;
        }

        if (gameManager.bulletType[5, 1])
        {
            damage[5] = 1;
            shootInterval[5] = 0.3f;
        }
        if (gameManager.bulletType[5, 2])
        {
            damage[5] = 1;
            shootInterval[5] = 0.25f;
            bullet_6Scale = 4;
        }
        if (gameManager.bulletType[5, 3])
        {
            bullet_6Mass = 30;
        }
        if (gameManager.bulletType[5, 4])
        {
            bullet_6Mass = 40;
        }

        if (gameManager.bulletType[6, 1])
        {
            bullet_7MaxLifetime = 1.8f;
        }
        if (gameManager.bulletType[6, 2])
        {
            bullet_7MaxLifetime = 2.3f;
        }
        if (gameManager.bulletType[6, 3])
        {
            bullet_7Speed = 600;
        }
        if (gameManager.bulletType[6, 4])
        {
            bullet_7Speed = 750;
        }

        if (gameManager.bulletType[7, 0])
        {
            Books book1 = Instantiate(booksPrefab, transform.position, Quaternion.identity);
            //SpriteRenderer spriteRenderer = book1.GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = book1.sprites[0];
            book1.book1 = true;
            book1.transform.parent = transform;
            book1.angle = Mathf.PI / 2;
            book1.player = this;
        }
        if (gameManager.bulletType[7, 1])
        {
            Books book2 = Instantiate(booksPrefab, transform.position, Quaternion.identity);
            //SpriteRenderer spriteRenderer = book2.GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = book2.sprites[1];
            book2.book2 = true;
            book2.transform.parent = transform;
            book2.angle = 3 * Mathf.PI / 2;
            book2.player = this;
        }
        if (gameManager.bulletType[7, 2])
        {
            Books book3 = Instantiate(booksPrefab, transform.position, Quaternion.identity);
            //SpriteRenderer spriteRenderer = book3.GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = book3.sprites[1];
            book3.book2 = true;
            book3.transform.parent = transform;
            book3.angle = 2 * Mathf.PI / 3 + Mathf.PI / 2;
            book3.player = this;
            Books book4 = Instantiate(booksPrefab, transform.position, Quaternion.identity);
            //SpriteRenderer spriteRenderer2 = book4.GetComponent<SpriteRenderer>();
            //spriteRenderer2.sprite = book4.sprites[2];
            book4.book3 = true;
            book4.transform.parent = transform;
            book4.angle = 4 * Mathf.PI / 3 + Mathf.PI / 2;
            book4.player = this;
        }
        if (gameManager.bulletType[7, 3])
        {
            withstandTime = 0.05f;
            withstandCountMax = 10;
        }
        if (gameManager.bulletType[7, 4])
        {
            withstandTime = 0.08f;
            withstandCountMax = 7;
        }

        //time_1 = shootInterval_1;
        //time_2 = shootInterval_2;

        /*if (playerCards.prop[0] == true)
        {
            killNeed = 10;
            if (playerCards.propSpecial[1] == true)
            {
                killNeed = 7;
            }
        }
        if (playerCards.prop[1] == true)
        {
            CD = 20;
        }
        if (playerCards.prop[2] == true)
        {
            ifProp = true;
        }
        if (playerCards.prop[3] == true)
        {
            CD = 5;
        }*/
        //playerCards = FindObjectOfType<PlayerCards>();
    }

    /*private void FindShootInterval()
    {
        if (PlayerCards.bullet[1] == true)
        {
            shootInterval = 0.5f;
        }
        if (PlayerCards.bullet[2] == true)
        {
            shootInterval = 0.35f;
        }
        if (PlayerCards.bullet[3] == true)
        {
            shootInterval = 0.8f;
        }
        if (PlayerCards.bullet[4] == true)
        {
            if (PlayerCards.bulletSpecial[0] == true)
            {
                shootInterval = 1.2f;
            }
            else
            {
                shootInterval = 0.7f;
            }
        }
    }*/
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W);
        _disThrusting = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            _turnDirection = 0.0f;
        }

        if (_thrusting && _disThrusting)
        {
            _thrusting = false;
            _disThrusting = false;
        }
        for (int i = 0; i < time.Length; i++)
        {
            if (gameManager.bulletType[i, 0] == true)
            {
                time[i] += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (time[0] > shootInterval[0])
            {
                time[0] = 0;
                Shoot_1();
            }
            if (time[1] > shootInterval[1])
            {
                time[1] = 0;
                int a = Random.Range(bullet_2Min, bullet_2Max + 1);
                for (int i = 0; i < a; i++)
                {
                    Invoke(nameof(Shoot_2), i * 0.08f);
                }
            }
            if (time[2] > shootInterval[2])
            {
                time[2] = 0;
                Shoot_3();
            }
            if (time[3] > shootInterval[3])
            {
                time[3] = 0;
                for (int i = 0; i < bullet_4shootTime; i++)
                {
                    Invoke(nameof(Shoot_4), i * 0.1f);
                }
            }
            if (time[4] > shootInterval[4])
            {
                time[4] = 0;
                Shoot_5();
            }
            if (time[5] > shootInterval[5])
            {
                time[5] = 0;
                Shoot_6();
            }
            if (time[6] > shootInterval[6])
            {
                time[6] = 0;
                Shoot_7();
            }
            if (time[7] > shootInterval[7])
            {
                time[7] = 0;
                Shoot_8();
            }
            /*time_1 += Time.deltaTime;
            if (time_1 - shootInterval_1 > 0)
            {
                Shoot();
                time_1 = 0;
            }
            time_2 += Time.deltaTime;
            if (time_2 - shootInterval_2 > 0)
            {
                Shoot();
                time_2 = 0;
            }*/
        }

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Prop();
        }*/
        /*if (Input.GetKeyUp(KeyCode.J))
        {
            time = shootInterval;
        }*/
        if (gameManager.bulletType[4, 0])
        {
            clockTime += Time.deltaTime;
            if (gameManager.bulletType[4, 1] || gameManager.bulletType[4, 0] || gameManager.bulletType[4, 2])
            {
                if (clockTime >= clockTimeMax)
                {
                    shootInterval[4] = 0.1f;
                }
            }
            if (gameManager.bulletType[4, 3] || gameManager.bulletType[4, 4])
            {
                if (clockTime >= clockTimeMax)
                {
                    shootInterval[4] = 0.35f;
                }
            }
        }
        if (gameManager.bulletType[7, 3] || gameManager.bulletType[7, 4])
        {
            if (withstandCount > withstandCountMax)
            {
                withstandCount = withstandCountMax;
            }
            shootInterval[7] = 0.7f - withstandTime * withstandCount;
        }
    }

    private void Shoot_1()
    {
        count_1++;
        if (count_1 <= count_1Max)
        {
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[0];
            bullet.Project(transform.up);
            bullet.damage = damage[0];
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1.5f, bullet.transform.localScale.y * 1.5f);
        }
        else
        {
            count_1 = 0;
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[0];
            bullet.Project(transform.up);
            bullet.damage = big_1damage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * big_1scale, bullet.transform.localScale.y * big_1scale);
        }
    }
    private void Shoot_2()
    {
        Bullet bullet = Instantiate(bulletPerfab, transform);
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        int a = Random.Range(1, 3);
        spriteRenderer.sprite = bullet.sprites[a];
        bullet.Project(transform.up);
        bullet.damage = damage[1];
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * bullet_2Scale, bullet.transform.localScale.y * bullet_2Scale);
    }

    private void Shoot_3()
    {
        if (letterCount == 0)
        {
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[3];
            bullet.Project(transform.up);
            bullet.damage = damage[2];
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);

            //spriteRenderer.color = Color.red;
            letterCount++;
            return;
        }
        if (letterCount == 1)
        {
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[4];
            bullet.Project(transform.up);
            bullet.damage = letterBdamage;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);
            letterCount++;
            if (gameManager.bulletType[2, 4])
            {
                Bullet bullet2 = Instantiate(bulletPerfab, transform);
                SpriteRenderer spriteRenderer2 = bullet2.GetComponent<SpriteRenderer>();
                spriteRenderer2.sprite = bullet.sprites[4];
                bullet2.Project(-transform.up);
                bullet2.damage = letterBdamage;
                bullet2.transform.localScale = new Vector3(bullet2.transform.localScale.x * 1, bullet2.transform.localScale.y * 1);
            }
            return;
        }
        if (letterCount == 2)
        {
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[5];
            bullet.Project(transform.up);
            bullet.damage = damage[2];
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * letterCScale, bullet.transform.localScale.y * letterCScale);
            letterCount = 0;
            if (gameManager.bulletType[2, 4] || gameManager.bulletType[2, 3])
            {
                Bullet bullet2 = Instantiate(bulletPerfab, transform);
                SpriteRenderer spriteRenderer2 = bullet2.GetComponent<SpriteRenderer>();
                spriteRenderer2.sprite = bullet.sprites[5];
                bullet2.Project(-transform.up);
                bullet2.damage = damage[2];
                bullet2.transform.localScale = new Vector3(bullet2.transform.localScale.x * letterCScale, bullet2.transform.localScale.y * letterCScale);
            }
            return;
        }

    }
    private void Shoot_4()
    {
        for (int j = 0; j < bullet_4amount; j++)
        {
            Bullet bullet = Instantiate(bulletPerfab, transform);
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bullet.sprites[6];
            bullet.damage = damage[3];

            float angle = (j - (bullet_4amount / 2)) * bullet_4angle;
            float angleInRadians = angle * Mathf.Deg2Rad;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 newDirection = rotation * transform.up;
            bullet.Project(newDirection);
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);
        }
    }

    private void Shoot_5()
    {
        Bullet bullet = Instantiate(bulletPerfab, transform);
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bullet.sprites[7];
        float angle = Random.Range(-5f, 5f);
        float angleInRadians = angle;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.Project(rotation * transform.up);
        bullet.damage = damage[4];
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);
    }

    private void Shoot_6()
    {
        Bullet bullet = Instantiate(bulletPerfab, transform);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.mass = rigidbody2D.mass * bullet_6Mass;
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bullet.sprites[8];
        bullet.speed = bullet.speed * bullet_6Mass;
        bullet.Project(transform.up);
        bullet.damage = damage[5];
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * bullet_6Scale, bullet.transform.localScale.y * bullet_6Scale);
    }

    private void Shoot_7()
    {
        Bullet bullet = Instantiate(bulletPerfab, transform);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.linearDamping = 3;
        int a = Random.Range(1, 11);
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        if (a == 1)
        {
            spriteRenderer.sprite = bullet.sprites[10];
        }
        else
        {
            spriteRenderer.sprite = bullet.sprites[9];
        }
        bullet.maxLifetime = bullet_7MaxLifetime;
        bullet.speed = bullet_7Speed;
        bullet.Project(Random.insideUnitCircle.normalized);
        bullet.damage = damage[6];
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);
    }
    private void Shoot_8()
    {
        Bullet bullet = Instantiate(bulletPerfab, transform);
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bullet.sprites[11];
        bullet.Project(transform.up);
        bullet.damage = damage[7];
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y * 1);
    }
    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }
        if (_disThrusting)
        {
            float dotProduct = Vector3.Dot(_rigidbody.linearVelocity, transform.up);
            float cosTheta = dotProduct / (_rigidbody.linearVelocity.magnitude * transform.up.magnitude);
            float theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
            if (theta < 90)
            {
                _rigidbody.AddForce(-transform.up * thrustSpeed * 0.35f);
            }
            else if (theta > 90.0f)
            {
                _rigidbody.AddForce(-transform.up * thrustSpeed * 0.15f);
            }

        }
        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        /*if (PlayerCards.bullet[0] == true)
        {
            ReturnedBullet returnedBullet = Instantiate(returnedBulletPrefab, transform);
            returnedBullet.Project(transform.up, PlayerCards.bulletSpecial);
        }
        else if (PlayerCards.bullet[1] == true)
        {
            TraceableBullet traceableBullet = Instantiate(traceableBulletPrefab, transform);
            traceableBullet.Project(transform.up, PlayerCards.bulletSpecial);
        }
        else if (PlayerCards.bullet[2] == true)
        {
            WallBullet wallBullet = Instantiate(wallBulletPrefab, transform);
            wallBullet.Project(transform.up, PlayerCards.bulletSpecial);
        }
        else if (PlayerCards.bullet[3] == true)
        {
            ExplosiveBullet explosiveBullet = Instantiate(explosiveBulletPrefab, transform);
            explosiveBullet.Project(transform.up, PlayerCards.bulletSpecial);
        }
        else if (PlayerCards.bullet[4] == true)
        {
            if (PlayerCards.bulletSpecial[1] == true)
            {
                switch (mult)
                {
                    case 0.5f: mult = 1.0f; break;
                    case 1.0f: mult = 1.5f; break;
                    case 1.5f: mult = 0.5f; break;
                }
            }
            TriangleBullet triangleBullet_1 = Instantiate(triangularBulletPrefab, transform);
            triangleBullet_1.Project_1(transform.up, mult);
            TriangleBullet triangleBullet_2 = Instantiate(triangularBulletPrefab, transform);
            triangleBullet_2.Project_2(transform.up, mult);
            TriangleBullet triangleBullet_3 = Instantiate(triangularBulletPrefab, transform);
            triangleBullet_3.Project_3(transform.up, mult);
            if (PlayerCards.bulletSpecial[0] == true)
            {
                TriangleBullet triangleBullet_4 = Instantiate(triangularBulletPrefab, transform);
                triangleBullet_4.Project_4(transform.up);
                TriangleBullet triangleBullet_5 = Instantiate(triangularBulletPrefab, transform);
                triangleBullet_5.Project_5(transform.up);
            }
        }
        else
        {*/
        Bullet bullet = Instantiate(bulletPerfab, transform);
        bullet.Project(transform.up);
        bullet.damage = 1;
        //}

    }

    /*private void CheckProp()
    {
        if (playerCards.prop[0] == true)
        {
            CheckProp_1();
        }
        else if (playerCards.prop[1] == true)
        {
            CheckProp_2();
        }
        else if (playerCards.prop[2] == true)
        {
            CheckProp_3();
        }
        else if (playerCards.prop[3] == true)
        {
            CheckProp_4();
        }
        else if (playerCards.prop[4] == true)
        {
            CheckProp_5();
        }
        else if (playerCards.prop[5] == true)
        {
            CheckProp_6();
        }
        else
        {
            propConditionGO.SetActive(false);
        }
    }

    private void CheckProp_1()
    {
        if (preDestroyedEnemy != GameManager.destroyCount && ifProp == false)
        {
            propCount++;
        }
        preDestroyedEnemy = GameManager.destroyCount;
        if (propCount >= killNeed)
        {
            propCount = 0;
            ifProp = true;
        }
        if (ifProp == false)
        {
            propCondition.text = (killNeed - propCount).ToString();
        }
        else
        {
            propCondition.text = "Ready";
        }
    }
    private void Prop_1()
    {
        gameManager.maxLives++;
        gameManager.lives++;
        gameManager.lifeText.text = "Life * " + gameManager.lives;
        ifProp = false;
        //回血反馈
        if (playerCards.propSpecial[0] == true)
        {
            gameManager.InvincibleTime += 5;
            //Respawn_1();
        }
    }
    private void CheckProp_2()
    {
        if (ifProp == false)
        {
            propCount += Time.deltaTime;
            propCondition.text = (CD - Mathf.Floor(propCount)).ToString();
            if (propCount >= CD)
            {
                propCount = 0;
                ifProp = true;
                propCondition.text = "Ready";
                CD = 20;
            }
        }
        if (ifProp == true && playerCards.propSpecial[1] == true)  //文本优化
        {
            if (preDestroyedEnemy != GameManager.destroyCount)
            {
                propCount = propCount + 2;
            }
            preDestroyedEnemy = GameManager.destroyCount;
        }

    }
    private void Prop_2()
    {
        if (playerCards.propSpecial[1] == true)
        {
            propCount = propCount - 2;
        }
        shootIntervalMult += 1;
        Buff();
        if (playerCards.propSpecial[0] == true)
        {
            straightSpeedMult += 0.5f;
            Buff();
            Repulsion();
            Invoke(nameof(Repulsion), 1.0f);
            Invoke(nameof(Repulsion), 2.0f);
            Invoke(nameof(Repulsion), 3.0f);
            Invoke(nameof(Repulsion), 4.0f);
            Invoke(nameof(Repulsion), 5.0f);
        }
        Invoke(nameof(TurnOffProp_2), 5);
    }
    private void TurnOffProp_2()
    {
        ifProp = false;
        if (playerCards.propSpecial[1] == true)
        {
            preDestroyedEnemy = 0;
        }
        shootIntervalMult -= 1;
        Buff();
        if (playerCards.propSpecial[0] == true)
        {
            straightSpeedMult -= 0.5f;
            Buff();
        }
    }
    private void CheckProp_3()
    {
        if (playerCards.propSpecial[0] == false)
        {
            if (gameManager.enemyAmount * 0.75f <= gameManager.enemyCount)
            {
                ifProp = false;
                propCondition.text = "Not available";
            }
        }

        if (ifProp)
        {
            if (playerCards.propSpecial[0] == false)
            {
                propCondition.text = "Ready (" + Mathf.Floor(gameManager.enemyAmount * 0.75f) + ")";
            }
            else
            {
                propCondition.text = "Ready";
            }
        }
        else
        {
            if (preDestroyedEnemy == 1)
            {
                propCount += Time.deltaTime;
                propCondition.text = Mathf.Floor(31 - propCount).ToString();

            }
            else
            {
                propCondition.text = "Not available";
            }
            //if (gameManager.enemyAmount == gameManager.enemyCount)
            //{
            //    propCondition.text = "Not available";
            // }
        }
    }
    private void Prop_3()
    {
        preDestroyedEnemy = 1;
        ifProp = false;
        shootIntervalMult += 2;
        Buff();
        Invoke(nameof(GameOver), 30);
        if (playerCards.propSpecial[1] == true)
        {
            Prop_3Special_1();
            Invoke(nameof(Prop_3Special_1), 5);
            Invoke(nameof(Prop_3Special_1), 10);
            Invoke(nameof(Prop_3Special_1), 15);
            Invoke(nameof(Prop_3Special_1), 20);
            Invoke(nameof(Prop_3Special_1), 25);
        }
    }
    private void Prop_3Special_1()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Plane");
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.transform.position.x <= 11.25 && obj.transform.position.x >= -11.25 && obj.transform.position.y >= -7.05 && obj.transform.position.y <= 7.05)
            {
                Bullet bullet = Instantiate(bulletPerfab, transform);
                bullet.Project((obj.transform.position - transform.position).normalized);
            }
            
        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.transform.position.x <= 11.25 && obj.transform.position.x >= -11.25 && obj.transform.position.y >= -7.05 && obj.transform.position.y <= 7.05)
            {
                Bullet bullet = Instantiate(bulletPerfab, transform);
                bullet.Project((obj.transform.position - transform.position).normalized);
            }
        }
    }
    private void CheckProp_4()
    {
        if (ifProp == false)
        {
            propCount += Time.deltaTime;
            propCondition.text = (CD - Mathf.Floor(propCount)).ToString();
            if (propCount >= CD)
            {
                propCount = 0;
                ifProp = true;
                propCondition.text = "Ready";
                CD = 5;
            }
        }
    }
    private void Prop_4()
    {
        ifProp = false;
        Instantiate(minePrefab, transform);
    }
    private void CheckProp_5()
    {

    }
    private void CheckProp_6()
    {

    }

    private void Prop()
    {
        if (playerCards.prop[0] == true && ifProp == true)
        {
            Prop_1();
        }
        else if (playerCards.prop[1] == true && ifProp == true)
        {
            Prop_2();
        }
        else if (playerCards.prop[2] == true && ifProp == true)
        {
            Prop_3();
        }
        else if (playerCards.prop[3] == true && ifProp == true)
        {
            Prop_4();
        }
        else if (playerCards.prop[4] == true && ifProp == true)
        {
            Prop_5();
        }
        else if (playerCards.prop[5] == true && ifProp == true)
        {
            Prop_6();
        }
    }

    
    private void Prop_5()
    {

    }
    private void Prop_6()
    {

    }*/

    /*private void FreezeBuff()
    {
        if (ifFrost && frostTime > 0)
        {
            frostTime--;
            turnSpeedMult -= frost.frostMult;
            straightSpeedMult -= frost.frostMult;
            shootIntervalMult -= frost.frostMult;
            Buff();
        }
        else if (ifFrost == false && frostTime == 0)
        {
            frostTime = 1;
            turnSpeedMult += frost.frostMult;
            straightSpeedMult += frost.frostMult;
            shootIntervalMult += frost.frostMult;
            Buff();
        }
    }*/

    /*private void Buff()
    {
        if (preStraightSpeedMult != straightSpeedMult)
        {
            thrustSpeed = thrustSpeed / preStraightSpeedMult;
            thrustSpeed = thrustSpeed * straightSpeedMult;
        }
        if (preTurnSpeedMult != turnSpeedMult)
        {
            turnSpeed = turnSpeed / preTurnSpeedMult;
            turnSpeed = turnSpeed * turnSpeedMult;
        }
        if (preShootIntervalMult != shootIntervalMult)
        {
            shootInterval = (1.0f / ((1.0f / shootInterval) / preShootIntervalMult));
            shootInterval = (1.0f / ((1.0f / shootInterval) * shootIntervalMult));
        }
        preStraightSpeedMult = straightSpeedMult;
        preTurnSpeedMult = turnSpeedMult;
        preShootIntervalMult = shootIntervalMult;

    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy Bullet" || collision.gameObject.tag == "Enemy Missile")
        {
            /*GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }*/
            //_rigidbody.velocity = Vector3.zero;
            //_rigidbody.angularVelocity = 0.0f;
            //gameObject.SetActive(false);

            //Quaternion quaternion = gameObject.transform.rotation;
            Vector2 normal = collision.GetContact(0).normal;
            if (collision.gameObject.tag == "Enemy")
            {
                _rigidbody.AddForce(normal * bounceStrength);
                gameManager.playerLife -= 1;
                if (gameManager.playerType[0])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.443f, 0.443f, 0.443f, 1.0f));
                }
                else if (gameManager.playerType[1])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.2549f, 0.3992f, 0.4314f));
                }
                else if (gameManager.playerType[2])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.85098f, 0.70196f, 0.56078f, 1.0f));
                }
                else if (gameManager.playerType[3])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.7098f, 0.4431f, 0.1922f));
                }
                else
                {
                    gameManager.Explosive(collision.GetContact(0).point, Color.white);//颜色
                }

                //_rigidbody.linearVelocity = normal * 5;
            }
            else if (collision.gameObject.tag == "Enemy Bullet")
            {
                _rigidbody.AddForce(normal * bounceStrength * 0.5f);
                EnemyBullet bullet = collision.gameObject.GetComponent<EnemyBullet>();
                gameManager.playerLife -= (int)Mathf.Round(bullet.damage);
                if (gameManager.playerType[0])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.443f, 0.443f, 0.443f, 1.0f));
                }
                else if (gameManager.playerType[1])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.2549f, 0.3992f, 0.4314f));
                }
                else if (gameManager.playerType[2])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.85098f, 0.70196f, 0.56078f, 1.0f));
                }
                else if (gameManager.playerType[3])
                {
                    gameManager.Explosive(collision.GetContact(0).point, new Color(0.7098f, 0.4431f, 0.1922f));
                }
                else
                {
                    gameManager.Explosive(collision.GetContact(0).point, Color.white);//颜色
                }
                //_rigidbody.linearVelocity = normal * 2.5f;
            }
            /*else if (collision.gameObject.tag == "Enemy Missile")
            {
                _rigidbody.AddForce(normal * bounceStrength * 0.75f);
                //_rigidbody.linearVelocity = normal * 3.75f;
            }*/
            //gameObject.transform.rotation = quaternion;
            //life -= 1;
            PlayerDied();
        }

        if (collision.gameObject.tag == "Boundary")
        {
            Vector2 normal = collision.GetContact(0).normal;
            _rigidbody.AddForce(normal * bounceStrength);
            //_rigidbody.AddForce(-_rigidbody.linearVelocity * bounceStrength);
            //_rigidbody.linearVelocity = normal * 5;
        }
    }

    public void Repulsion()
    {
        GameObject[] allGameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject gameObject in allGameObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, this.transform.position);
            if (gameObject.tag == "Enemy" && distance <= collisionRange)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.position - transform.position).normalized * collisionStrength * (1 - distance / collisionRange));
            }
        }
    }

    public void PlayerDied()
    {
        Repulsion();

        //explosion.transform.position = transform.position;
        //explosion.Play();

        //gameManager.lives--;
        //gameManager.lifeText.text = "Life * " + gameManager.lives;

        /*if (gameManager.lives <= 0)
        {
            GameOver();
        }
        else
        {*/
        Respawn();
        //}
    }

    private void Respawn()  //复活
    {
        //player.transform.position = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Invincible Player");
        //gameObject.SetActive(true);
        //spriteRenderer.sprite = sprite2;
        //thrustSpeed = thrustSpeed * 1.5f;
        //straightSpeedMult += 0.5f;
        //Buff();

        StartCoroutine(FadeInOut(invincibleTime));
        Invoke(nameof(TurnOnCollisions), invincibleTime);

        /*if (gameManager.InvincibleTime != 3.0f)
        {
            gameManager.InvincibleTime = 3.0f;
        }*/
    }
    /*private void Respawn_1()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        spriteRenderer.sprite = sprite2;
        Invoke(nameof(TurnOnCollisions_1), 5.0f);
    }*/

    private void TurnOnCollisions()
    {
        //spriteRenderer.sprite = sprite1;
        //thrustSpeed = thrustSpeed * 2 / 3f;
        //straightSpeedMult -= 0.5f;
        //Buff();
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    /*private void TurnOnCollisions_1()
    {
        spriteRenderer.sprite = sprite1;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }*/

    /*private void GameOver()
    {
        gameManager.Lose();
    }*/


    /*IEnumerator CountTime(float time)
    {
        yield return new WaitForSeconds(time);
    }*/
    private IEnumerator FadeInOut(float count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Fade(1.0f, 0.1f, 0.5f);
            yield return Fade(0.1f, 1.0f, 0.5f);
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            Color currentColor = spriteRenderer.material.color;
            currentColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.material.color = currentColor;
            yield return null;
        }

        spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, endAlpha);
    }
}

