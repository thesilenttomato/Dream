using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public ParticleSystem explosion;
    public GameManager gameManager;
    //public SpriteRenderer spriteRenderer;
    //public Transform target;

    //public Sprite sprite1;
    //public Sprite sprite2;

    //public Text propCondition;
   // public GameObject propConditionGO;

    public float shootInterval = 0.25f;
    private float time;

    public float thrustSpeed = 1.25f;
    public float turnSpeed = 0.1f;
    private bool _thrusting;
    private bool _disThrusting;
    private float _turnDirection;

    public float collisionStrength = 200.0f;
    public float collisionRange = 4f;

    public float bounceStrength = 75.0f;

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
    }
    private void Start()
    {
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
        time = shootInterval;

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
        //CheckProp();
        //FreezeBuff();
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
        if (Input.GetKey(KeyCode.J))
        {
            time += Time.deltaTime;
            if (time - shootInterval > 0)
            {
                Shoot();
                time = 0;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Prop();
        }*/
        /*if (Input.GetKeyUp(KeyCode.J))
        {
            time = shootInterval;
        }*/
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }
        if (_disThrusting)
        {
            _rigidbody.AddForce(-transform.up * thrustSpeed * 0.25f);
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Plane" || collision.gameObject.tag == "Enemy Bullet" || collision.gameObject.tag == "Enemy Missile")
        {
            /*GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }*/
            //_rigidbody.velocity = Vector3.zero;
            //_rigidbody.angularVelocity = 0.0f;
            //gameObject.SetActive(false);

            Vector2 normal = collision.GetContact(0).normal;
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Plane")
            {
                _rigidbody.AddForce(normal * bounceStrength);
            }
            else if (collision.gameObject.tag == "Enemy Bullet")
            {
                _rigidbody.AddForce(normal * bounceStrength * 0.5f);
            }
            else if (collision.gameObject.tag == "Enemy Missile")
            {
                _rigidbody.AddForce(normal * bounceStrength * 0.75f);
            }

            //PlayerDied();
        }

        if (collision.gameObject.tag == "Boundary")
        {
            Vector2 normal = collision.GetContact(0).normal;
            _rigidbody.AddForce(normal * bounceStrength);
        }
    }

    public void Repulsion()
    {
        GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in allGameObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, this.transform.position);
            if (gameObject.tag == "Plane" && distance <= collisionRange)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.position - transform.position).normalized * collisionStrength * (1 - distance / collisionRange));
            }
            if (gameObject.tag == "Enemy" && distance <= collisionRange)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.position - transform.position).normalized * collisionStrength * (1 - distance / collisionRange));
            }
        }
    }

    /*public void PlayerDied()
    {
        Repulsion();

        explosion.transform.position = transform.position;
        explosion.Play();

        gameManager.lives--;
        gameManager.lifeText.text = "Life * " + gameManager.lives;

        if (gameManager.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }*/

    /*private void Respawn()  //复活
    {
        //player.transform.position = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        //gameObject.SetActive(true);
        spriteRenderer.sprite = sprite2;
        //thrustSpeed = thrustSpeed * 1.5f;
        straightSpeedMult += 0.5f;
        Buff();

        Invoke(nameof(TurnOnCollisions), gameManager.InvincibleTime);

        if (gameManager.InvincibleTime != 3.0f)
        {
            gameManager.InvincibleTime = 3.0f;
        }
    }*/
    /*private void Respawn_1()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        spriteRenderer.sprite = sprite2;
        Invoke(nameof(TurnOnCollisions_1), 5.0f);
    }*/

    /*private void TurnOnCollisions()
    {
        spriteRenderer.sprite = sprite1;
        //thrustSpeed = thrustSpeed * 2 / 3f;
        straightSpeedMult -= 0.5f;
        Buff();
        gameObject.layer = LayerMask.NameToLayer("Player");
    }*/
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

}

