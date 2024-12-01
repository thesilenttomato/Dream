using DG.Tweening.Core.Easing;
using UnityEngine;

public class SuperShame : MonoBehaviour
{
    public GameManager gameManager;
    public Transform target;
    //public InvestigationBullet overloadBulletPrefab;
    public EnemyBullet enemyBulletPrefab;
    private Rigidbody2D _rigidbody;

    /* public float speed = 1.5f;
     private float size = 0.75f;
     public int life = 1;

     public float shootDistance = 5f;
     public float shootInterval = 2.5f;*/

    public BaseUnitData baseUnitData;
    private float time = 0;

    public Vector3 direction;

    //public float distance = 5.5f;
    //public float shootDistance = 7.5f;

    //private float existTime;

    //public float existTimeMax;

    //private bool flee = false;

    public Vector3 fleeDirection;

    private float angle = 15;
    public Animator animator;
    private bool move;
    private bool attack;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        baseUnitData = new BaseUnitData(16, 1, 3, 1, 100);
        if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 3)
        {
            baseUnitData.life = 25;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 8)
        {
            baseUnitData.attackInterval = 2;
            angle = 10;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.linearDamping = 2;
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        animator.SetBool("move", move);
        animator.SetBool("attack", attack);
        //existTime += Time.deltaTime;
        //baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        Vector3 direction = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //float currentDistance = Vector3.Distance(transform.position, target.position);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;
        //if (existTime <= existTimeMax)
        //{
        //Vector3 direction = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //float currentDistance = Vector3.Distance(transform.position, target.position);

        /*if (currentDistance > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, -baseUnitData.movementSpeed * Time.deltaTime * 0.5f);
        }*/

        //Vector3 direction = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //float currentDistance = Vector3.Distance(transform.position, target.position);
        if (transform.position.x > 9.5f || transform.position.x < -9.5f)
        {

            //if (currentDistance > 3.0f)
            //{
            if (transform.position.x > 0)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = true;
                //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
            }
            else
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = false;
                //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, transform.position.y, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            move = true;
            attack = false;
            //}
        }
        if (transform.position.y > 6.55f || transform.position.y < -6.55f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            move = true;
            attack = false;
        }
        if (transform.position.x <= 9.5f && transform.position.x >= -9.5f && transform.position.y <= 6.55f && transform.position.y >= -6.55f)
        {
            if (transform.position.x > 0)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = true;
                //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
            }
            else
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = false;
                //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
            }
            time += Time.deltaTime;
            move = false;
            attack = true;
            if (time > baseUnitData.attackInterval)
            {
                Shoot();
                time = 0;
            }
        }

        /*if (transform.position.y > 6.4f || transform.position.y < -6.4f)
        {
            if (currentDistance > 3.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, 0.0f), speed * Time.deltaTime);
            }
        }*/


        /*if (currentDistance <= shootDistance && time >= baseUnitData.attackInterval)
        {
            time = 0;
            //Debug.Log("SB");
            Shoot();
            //Destroy(gameObject);
        }*/
        //}
        /*else
        {
            if (flee == false)
            {
                flee = true;

                Vector3 direction = -(target.position - transform.position).normalized;

                float angle = 0;

                while (angle == 0)
                {
                    angle = Random.Range(-1, 2) * Random.Range(15f, 25f);
                }

                float angleInRadians = angle * Mathf.Deg2Rad;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // 应用旋转
                fleeDirection = rotation * direction;
            }

            transform.position += fleeDirection * baseUnitData.movementSpeed * Time.deltaTime;
            //逃跑时方向旋转

            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }*/
    }

    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        EnemyBullet enemyBullet1 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer1 = enemyBullet1.GetComponent<SpriteRenderer>();
        spriteRenderer1.sprite = enemyBullet1.sprite[3];
        enemyBullet1.speed = baseUnitData.bulletSpeed;
        enemyBullet1.Project((target.position - transform.position).normalized);
        enemyBullet1.damage = baseUnitData.attack;

        //float angleInRadians = angle * Mathf.Deg2Rad;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 应用旋转
        Vector3 newDirection = rotation * (target.position - transform.position).normalized;
        //enemyBullet.bulletType = 2;

        EnemyBullet enemyBullet2 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer2 = enemyBullet2.GetComponent<SpriteRenderer>();
        spriteRenderer2.sprite = enemyBullet2.sprite[3];
        enemyBullet2.speed = baseUnitData.bulletSpeed;
        enemyBullet2.Project(newDirection);
        enemyBullet2.damage = baseUnitData.attack;

        //angleInRadians = -angle * Mathf.Deg2Rad;
        rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        newDirection = rotation * (target.position - transform.position).normalized;

        EnemyBullet enemyBullet3 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer3 = enemyBullet3.GetComponent<SpriteRenderer>();
        spriteRenderer3.sprite = enemyBullet3.sprite[3];
        enemyBullet3.speed = baseUnitData.bulletSpeed;
        enemyBullet3.Project(newDirection);
        enemyBullet3.damage = baseUnitData.attack;

        /*if (gameManager.emotionalQuantity[5] >= 8)
        {
            rotation = Quaternion.AngleAxis(2 * angle, Vector3.forward);
            newDirection = rotation * (target.position - transform.position).normalized;

            EnemyBullet enemyBullet4 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer4 = enemyBullet4.GetComponent<SpriteRenderer>();
            spriteRenderer4.sprite = enemyBullet4.sprite[3];
            enemyBullet4.speed = baseUnitData.bulletSpeed;
            enemyBullet4.Project(newDirection);
            enemyBullet4.damage = baseUnitData.attack;

            rotation = Quaternion.AngleAxis(-2 * angle, Vector3.forward);
            newDirection = rotation * (target.position - transform.position).normalized;

            EnemyBullet enemyBullet5 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer5 = enemyBullet5.GetComponent<SpriteRenderer>();
            spriteRenderer5.sprite = enemyBullet5.sprite[3];
            enemyBullet5.speed = baseUnitData.bulletSpeed;
            enemyBullet5.Project(newDirection);
            enemyBullet5.damage = baseUnitData.attack;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(70f / 255f, 67f / 255f, 93f / 255f, 1.0f));
            enemySound enemySound = GetComponent<enemySound>();
            enemySound.Sound(Vector3.Distance(transform.position, target.position));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                tag = "Invincible Enemy";
                gameObject.layer = 13;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                animator.enabled = false;
                Destroy(gameObject, 0.1f);
                this.enabled = false;
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
