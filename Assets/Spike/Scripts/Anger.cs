using UnityEngine;

public class Anger : MonoBehaviour
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

    private float existTime1;
    private float existTime2;
    public float existTimeMax;

    private bool flee = false;
    public Vector3 fleeDirection;

    private bool angerState = false;
    public Animator animator;
    private bool change_1;
    private bool change_2;
    private float changeTime = 0;

    public enemySound enemySoundPrefab;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        baseUnitData = new BaseUnitData(5, 1, 3, 1, 125);
        if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 5)
        {
            baseUnitData.life = 7;
        }
        //_rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        animator.SetBool("change_1", change_1);
        animator.SetBool("change_2", change_2);
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        if (angerState == false)
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
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
            transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;

            existTime1 += Time.deltaTime;
            if (existTime1 >= 15)
            {
                if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (changeTime <= 0.5f)
        {
            changeTime += Time.deltaTime;
            change_1 = true;
        }
        else
        {
            change_2 = true;
            Vector3 direction = (target.position - transform.position).normalized;
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
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            //float currentDistance = Vector3.Distance(transform.position, target.position);

            transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);

            time += Time.deltaTime;
            if (time >= baseUnitData.attackInterval && flee == false)
            {
                time = 0;
                //Debug.Log("SB");
                Shoot();
                //Destroy(gameObject);
            }

            existTime2 += Time.deltaTime;
            if (existTime2 >= existTimeMax)
            {
                if (flee == false)
                {
                    flee = true;

                    direction = -(target.position - transform.position).normalized;

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
                if (fleeDirection.x < 0)
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.flipX = false;
                }
                else
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.flipX = true;
                }

                if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
                {
                    Destroy(gameObject);
                }
            }
        }

        //time += Time.deltaTime;
        /*if (time >= baseUnitData.attackInterval)
        {
            time = 0;
            //Debug.Log("SB");
            Shoot();
        }*/

    }

    private void Shoot()
    {
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        for (int i = 1; i <= 2; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[4];
            enemyBullet.speed = baseUnitData.bulletSpeed;
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = 0;
            angle = Random.Range(-10, 10f);
            float angleInRadians = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotation * direction.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 90);
            enemyBullet.Project(rotation * direction);
            enemyBullet.damage = baseUnitData.attack;
            //enemyBullet.bulletType = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (angerState == false)
            {
                angerState = true;
                existTimeMax = 20;
                if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[6]) < 8)
                {
                    existTimeMax = 25;
                }
                if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 8)
                {
                    existTimeMax = 30;
                }
                baseUnitData.life += 5;
                if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 6)
                {
                    baseUnitData.life += 3;
                }
                baseUnitData.movementSpeed += 1;
                if (Mathf.Abs(gameManager.emotionalQuantity[6]) >= 9)
                {
                    baseUnitData.movementSpeed += 0.5f;
                }
            }

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            if (change_2 == false)
            {
                gameManager.Explosive(collision.GetContact(0).point, new Color(126f / 255f, 156f / 255f, 93f / 255f, 1.0f));
            }
            else
            {
                gameManager.Explosive(collision.GetContact(0).point, new Color(173f / 255f, 60f / 255f, 42f / 255f, 1.0f));
            }
            enemySound enemySound = GetComponent<enemySound>();
            enemySound.Sound(Vector3.Distance(transform.position, target.position));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                gameManager.defeatedEmotion[6] += 1;
                tag = "Invincible Enemy";
                gameObject.layer = 13;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
                animator.enabled = false;
                Destroy(gameObject, 0.5f);
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
