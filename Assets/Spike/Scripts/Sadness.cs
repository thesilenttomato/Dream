using UnityEngine;

public class Sadness : MonoBehaviour
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

    public float distance = 5.5f;
    public float shootDistance = 7.5f;

    private float existTime;

    public float existTimeMax;

    private bool flee = false;

    public Vector3 fleeDirection;

    private float scaleMult = 1;
    public Animator animator;
    private bool move;
    private bool attack;
    private bool stopAndShoot;
    private float PremoveSpeed = 0;
    //private float 
    private void Start()
    {
        baseUnitData = new BaseUnitData(1, 1, 8, 1, 100);
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager.emotionalQuantity[1] >= 3 && gameManager.emotionalQuantity[1] < 6)
        {
            scaleMult = 1.5f;
        }
        if (gameManager.emotionalQuantity[1] >= 6 && gameManager.emotionalQuantity[1] < 8)
        {
            scaleMult = 2;
        }
        if (gameManager.emotionalQuantity[1] >= 8)
        {
            scaleMult = 2.5f;
        }
        if (gameManager.emotionalQuantity[1] >= 9)
        {
            baseUnitData.life = 3;
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
        existTime += Time.deltaTime;
        //baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;
        if (existTime <= existTimeMax)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            if (flee == false)
            {
                if (direction.x < 0)
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.flipX = true;
                }
                else
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.flipX = false;
                }
            }
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            float currentDistance = Vector3.Distance(transform.position, target.position);

            if (currentDistance > distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, -baseUnitData.movementSpeed * Time.deltaTime * 0.5f);
            }

            time += Time.deltaTime;
            if (currentDistance <= shootDistance && time >= baseUnitData.attackInterval && !stopAndShoot)
            {
                PremoveSpeed = baseUnitData.movementSpeed;
                stopAndShoot = true;
                time = 0;
                //Debug.Log("SB");
                Invoke(nameof(Shoot), 0.8f);
                //Destroy(gameObject);
            }
            if (stopAndShoot)
            {
                baseUnitData.movementSpeed = 0;
                move = false;
                attack = true;
                Invoke(nameof(MoveAgain), 1.6f);
            }
        }
        else
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

                // Ó¦ÓÃÐý×ª
                fleeDirection = rotation * direction;
            }
            move = true;
            attack = false;
            transform.position += fleeDirection * baseUnitData.movementSpeed * Time.deltaTime;
            if (fleeDirection.x < 0)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = false;
            }

            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveAgain()
    {
        move = true;
        attack = false;
        baseUnitData.movementSpeed = PremoveSpeed;
        stopAndShoot = false;
    }
    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyBullet.sprite[2];
        enemyBullet.speed = baseUnitData.bulletSpeed;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (target.position - transform.position).normalized);
        Vector3 eulerRotation = targetRotation.eulerAngles;
        enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 180);
        enemyBullet.Project((target.position - transform.position).normalized);
        //enemyBullet.bulletType = 2;
        enemyBullet.transform.localScale = new Vector3(enemyBullet.transform.localScale.x * 2 * scaleMult, enemyBullet.transform.localScale.y * 2 * scaleMult);
        enemyBullet.damage = baseUnitData.attack;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(233f / 255f, 190f / 255f, 87f / 255f, 1.0f));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                gameManager.defeatedEmotion[1] += 1;
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
