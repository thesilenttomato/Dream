using Spine;
using UnityEngine;

public class Fear : MonoBehaviour
{
    public GameManager gameManager;
    public Transform target;
    public Player player;
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

    public float range = 6f;
    //public float shootDistance = 7.5f;

    private float existTime;

    public float existTimeMax;

    private bool flee = false;

    public Vector3 fleeDirection;

    private int force = 40;

    public SpecialEffectAnimation specialEffectAnimationPrefab;
    private bool fearShow = false;
    private void Start()
    {
        baseUnitData = new BaseUnitData(3, 1, 12, 0.75f, 75);
        gameManager = FindFirstObjectByType<GameManager>();
        if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[3]) < 8)
        {
            baseUnitData.attackInterval = baseUnitData.attackInterval * 0.85f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 8)
        {
            baseUnitData.attackInterval = baseUnitData.attackInterval * 0.7f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 6)
        {
            force = 48;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 5 && Mathf.Abs(gameManager.emotionalQuantity[3]) < 9)
        {
            baseUnitData.movementSpeed = 1.25f;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[3]) >= 17)
        {
            baseUnitData.movementSpeed = 1.75f;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.linearDamping = 2;
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;
        player = GameObject.FindFirstObjectByType<Player>();
        

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
        if (existTime <= existTimeMax)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            if (flee == false)
            {
                if (direction.x > 0)
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
            float currentDistance = Vector3.Distance(transform.position, target.position);

            transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);

            time += Time.deltaTime;
            //if (player.gameObject.layer == 6)
            //{
            if (currentDistance <= range)
            {
                time += Time.deltaTime;
                if(time>= baseUnitData.attackInterval - 0.3f && fearShow == false)
                {
                    fearShow = true;
                    int a = Random.Range(10, 20);
                    for (int i = 1; i <= a; i++)
                    {
                        SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
                        specialEffectAnimation.fear = true;
                        SpriteRenderer spriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
                        spriteRenderer.sortingOrder = -9;
                        Vector2 newDirection = Random.insideUnitCircle.normalized;
                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
                        Vector3 eulerRotation = targetRotation.eulerAngles;
                        specialEffectAnimation.transform.eulerAngles = eulerRotation;
                        specialEffectAnimation.direction = newDirection;
                        //Invoke(nameof(specialEffectAnimation.DestroyGameObject),0.6f); 
                    }
                }
                if (time >= baseUnitData.attackInterval)
                {
                    fearShow = false;
                    time = 0;
                    player.GetComponent<Rigidbody2D>().AddForce(direction * force);
                }
            }
            //}
            //time += Time.deltaTime;
            /*if (currentDistance <= shootDistance && time >= baseUnitData.attackInterval)
            {
                time = 0;
                //Debug.Log("SB");
                Shoot();
                //Destroy(gameObject);
            }*/
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

                fleeDirection = rotation * direction;
            }

            transform.position += fleeDirection * baseUnitData.movementSpeed * Time.deltaTime;
            if (fleeDirection.x > 0)
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

    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet.speed = baseUnitData.bulletSpeed;
        enemyBullet.Project((target.position - transform.position).normalized);
        enemyBullet.damage = baseUnitData.attack;
        //enemyBullet.bulletType = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(108f / 255f, 190f / 255f, 153f / 255f, 1.0f));
            enemySound enemySound = GetComponent<enemySound>();
            enemySound.Sound(Vector3.Distance(transform.position, target.position));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                gameManager.defeatedEmotion[3] += 1;
                tag = "Invincible Enemy";
                gameObject.layer = 13;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                Animator animator = GetComponent<Animator>();
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
