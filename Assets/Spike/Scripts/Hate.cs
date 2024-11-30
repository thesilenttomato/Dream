using DG.Tweening.Core.Easing;
using UnityEngine;

public class Hate : MonoBehaviour
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

    private float existTime;

    //public float existTimeMax;

    //private bool flee = false;

    public Vector3 fleeDirection;

    private Vector3 startPosition;

    private float shootIntervalMult = 1;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        baseUnitData = new BaseUnitData(15, 1, 6, 0.5f, 100);
        if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[7]) < 8)
        {
            baseUnitData.attackInterval = baseUnitData.attackInterval * 0.85f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 8)
        {
            baseUnitData.attackInterval = baseUnitData.attackInterval * 0.7f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 5)
        {
            baseUnitData.life = 20;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 6)
        {
            shootIntervalMult = 1.5f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[7]) >= 9)
        {
            transform.localScale = new Vector3(1, 1);
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.linearDamping = 2;
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        startPosition = transform.position;
        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        existTime += Time.deltaTime;
        //baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;
        //if (existTime <= existTimeMax)
        //{



        //if (currentDistance > 3.0f)
        //{
        if (startPosition.y > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -100, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 100, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        //}

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

        time += Time.deltaTime;
        if (time >= baseUnitData.attackInterval)
        {
            time = 0;
            //Debug.Log("SB");
            for (int i = 1; i <= 20; i++)
            {
                Invoke(nameof(Shoot), (i - 1) * shootIntervalMult * 0.1f);
            }
            //Destroy(gameObject);
        }
        //}
        //else
        //{
        /*if (flee == false)
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

        transform.position += fleeDirection * baseUnitData.movementSpeed * Time.deltaTime;*/
        //逃跑时方向旋转
        if (existTime > 15)
        {
            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
        //}
    }

    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        EnemyBullet enemyBullet1 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer1 = enemyBullet1.GetComponent<SpriteRenderer>();
        spriteRenderer1.sprite = enemyBullet1.sprite[5];
        Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, Vector2.left.normalized);
        Vector3 eulerRotation1 = targetRotation1.eulerAngles;
        enemyBullet1.transform.eulerAngles = eulerRotation1 + new Vector3(0, 0, 180);
        enemyBullet1.speed = baseUnitData.bulletSpeed;
        enemyBullet1.Project(Vector2.left);
        enemyBullet1.damage = baseUnitData.attack;

        EnemyBullet enemyBullet2 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer2 = enemyBullet2.GetComponent<SpriteRenderer>();
        spriteRenderer2.sprite = enemyBullet2.sprite[5];
        Quaternion targetRotation2 = Quaternion.LookRotation(Vector3.forward, Vector2.left.normalized);
        Vector3 eulerRotation2 = targetRotation2.eulerAngles;
        enemyBullet2.transform.eulerAngles = eulerRotation2 + new Vector3(0, 0, 180);
        enemyBullet2.speed = baseUnitData.bulletSpeed;
        enemyBullet2.Project(Vector2.right);
        enemyBullet2.damage = baseUnitData.attack;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(67f / 255f, 40f / 255f, 36f / 255f, 1.0f));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                gameManager.defeatedEmotion[7] += 1;
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
