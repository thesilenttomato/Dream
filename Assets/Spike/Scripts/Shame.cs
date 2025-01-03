using UnityEngine;

public class Shame : MonoBehaviour
{
    public GameManager gameManager;
    public Transform target;
    //public InvestigationBullet overloadBulletPrefab;
    public EnemyBullet enemyBulletPrefab;
    private Rigidbody2D _rigidbody;
    public SpecialEffectAnimation specialEffectAnimationPrefab;

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

    public Animator animator;
    private bool move;
    private bool attack;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        baseUnitData = new BaseUnitData(1, 1, 8, 0.8f, 0);
        if (Mathf.Abs(gameManager.emotionalQuantity[5]) >= 5)
        {
            baseUnitData.life = 2;
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
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
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
        if (transform.position.x > 10.0f || transform.position.x < -10.0f)
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

            //if (currentDistance > 3.0f)
            //{
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, transform.position.y, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            move = true;
            attack = false;
            //}
        }
        else
        {
            time += Time.deltaTime;
            move = false;
            attack = true;
            if (time > baseUnitData.attackInterval)
            {
                SuperShameSpawner superShameSpawner = FindFirstObjectByType<SuperShameSpawner>();
                superShameSpawner.count++;
                SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
                specialEffectAnimation.shame_smog = true;
                SpriteRenderer spriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = -9;
                Destroy(gameObject);
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
        EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet.speed = baseUnitData.bulletSpeed;
        enemyBullet.Project((target.position - transform.position).normalized);
        enemyBullet.bulletType = 2;
        enemyBullet.damage = baseUnitData.attack;
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
                gameManager.defeatedEmotion[5] += 1;
                tag = "Invincible Enemy";
                gameObject.layer = 13;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                animator.enabled = false;
                Destroy(gameObject, 0.1f);
                this.enabled = false;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
            specialEffectAnimation.shame_smog = true;
            SpriteRenderer spriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -9;
            Destroy(gameObject);
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
