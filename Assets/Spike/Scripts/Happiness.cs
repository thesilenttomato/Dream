using Unity.VisualScripting;
using UnityEngine;
public class Happiness : MonoBehaviour
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
    //private float time = 0;

    public Vector3 direction;
    private float existTime;

    private float maximumCycleTime = 2.4f;
    private float attackTime = 3;
    private float ShootJumpTimeMax = 3;
    private int ShootJumpTime = 0;
    private float stopTime;
    private bool shootCheck = false;
    //private float restTimeMax = 1;
    //private float restTime = 0;
    public Animator animator;
    private bool move;
    private bool attack;
    private bool stop;
    private void Start()
    {
        baseUnitData = new BaseUnitData(1, 1, 10, 2, 200);
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager.emotionalQuantity[0] >= 3 && gameManager.emotionalQuantity[0] < 8)
        {
            baseUnitData.bulletSpeed = 250;
        }
        if (gameManager.emotionalQuantity[0] >= 8)
        {
            baseUnitData.bulletSpeed = 300;
        }
        if (gameManager.emotionalQuantity[0] >= 9)
        {
            baseUnitData.movementSpeed = 2.5f;
        }
        if (gameManager.emotionalQuantity[0] >= 11)
        {
            ShootJumpTimeMax = 2;
        }
        if (gameManager.emotionalQuantity[0] >= 5)
        {
            baseUnitData.life = 3;
        }
        //_rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        animator.SetBool("move", move);
        animator.SetBool("attack", attack);
        animator.SetBool("stop", stop);
        //Vector3 direction = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        /*float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        stopTime += Time.deltaTime;
        if (ShootJumpTime < ShootJumpTimeMax)
        {
            if (stopTime <= maximumCycleTime - 1.2f)
            {
                if (stopTime >= 0.2f && stopTime <= 1)
                {
                    transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;
                }
                move = true;
                attack = false;
                stop = false;
            }
            else if (stopTime > maximumCycleTime - 1.2f && stopTime <= maximumCycleTime)
            {
                move = false;
                attack = false;
                stop = true;
            }
            else if (stopTime > maximumCycleTime)
            {
                ShootJumpTime++;
                stopTime = 0;
            }
        }
        else
        {
            if (stopTime <= maximumCycleTime - 1.2f)
            {
                if (stopTime >= 0.2f && stopTime <= 1)
                {
                    transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;
                }
                move = true;
                attack = false;
                stop = false;
            }
            else if (stopTime > maximumCycleTime - 1.2f && stopTime <= maximumCycleTime)
            {
                move = false;
                attack = false;
                stop = true;
            }
            else if (stopTime > maximumCycleTime && stopTime <= maximumCycleTime + 0.8f)
            {
                if (!shootCheck)
                {
                    Invoke(nameof(Shoot), 0.6f);
                    shootCheck = true;
                }
                move = false;
                attack = true;
                stop = false;
            }
            else if (stopTime > maximumCycleTime + 0.8f)
            {
                ShootJumpTime = 0;
                stopTime = 0;
                shootCheck = false;
            }
        }


        /*time += Time.deltaTime;
        if (time >= baseUnitData.attackInterval)
        {
            time = 0;
            //Debug.Log("SB");
            Shoot();
        }*/
        existTime += Time.deltaTime;
        if (existTime >= 25)
        {
            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        for (int i = 1; i <= 10; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[0];
            enemyBullet.speed = baseUnitData.bulletSpeed;
            Vector2 newDirection = Random.insideUnitCircle.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 135);
            enemyBullet.Project(newDirection);
            enemyBullet.bulletType = 1;
            enemyBullet.damage = baseUnitData.attack;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //baseUnitData.life--;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(224f / 255f, 214f / 255f, 176f / 255f, 1.0f));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                gameManager.defeatedEmotion[0] += 1;
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
