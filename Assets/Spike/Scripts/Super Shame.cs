using UnityEngine;

public class SuperShame : MonoBehaviour
{
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

    private bool flee = false;

    public Vector3 fleeDirection;
    private void Start()
    {
        baseUnitData = new BaseUnitData(20, 1, 2, 1, 100);
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.linearDamping = 2;
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        //existTime += Time.deltaTime;
        //baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        //float currentDistance = Vector3.Distance(transform.position, target.position);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, transform.position.y, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
            //}
        }
        if (transform.position.y > 6.55f || transform.position.y < -6.55f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, 0.0f), baseUnitData.movementSpeed * Time.deltaTime);
        }
        if(transform.position.x <= 9.5f && transform.position.x >= -9.5f && transform.position.y <= 6.55f && transform.position.y >= -6.55f)
        {
            time += Time.deltaTime;
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
        enemyBullet1.speed = baseUnitData.bulletSpeed;
        enemyBullet1.Project((target.position - transform.position).normalized);
        enemyBullet1.damage = baseUnitData.attack;

        float angle = 15;

        float angleInRadians = angle * Mathf.Deg2Rad;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 应用旋转
        Vector3 newDirection = rotation * (target.position - transform.position).normalized;
        //enemyBullet.bulletType = 2;

        EnemyBullet enemyBullet2 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet2.speed = baseUnitData.bulletSpeed;
        enemyBullet2.Project(newDirection);
        enemyBullet2.damage = baseUnitData.attack;

        angle = -15;

        angleInRadians = angle * Mathf.Deg2Rad;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newDirection = rotation * (target.position - transform.position).normalized;

        EnemyBullet enemyBullet3 = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet3.speed = baseUnitData.bulletSpeed;
        enemyBullet3.Project(newDirection);
        enemyBullet3.damage = baseUnitData.attack;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
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
