using UnityEngine;

public class Calmness : MonoBehaviour
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
    //private float time = 0;

    public Vector3 direction;

    private float existTime;
    private void Start()
    {
        baseUnitData = new BaseUnitData(2, 1, 10, 3, 100);
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.linearDamping = 2;
        //_rigidbody.AddForce(direction * baseUnitData.movementSpeed);
        //transform.localScale = Vector3.one * size;
        target = FindFirstObjectByType<Player>().target;

        //time = baseUnitData.attackInterval;
    }

    private void Update()
    {
        baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;

        //time += Time.deltaTime;
        if (baseUnitData.movementSpeed < 0.01f)
        {
            //time = 0;
            //Debug.Log("SB");
            Shoot();
            Destroy(gameObject);
        }
        existTime += Time.deltaTime;
        if (existTime >= 15)
        {
            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Shoot()
    {
        Vector2[] bulletDirections = new Vector2[]
        {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
        };
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        for (int i = 1; i <= 4; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            enemyBullet.speed = baseUnitData.bulletSpeed;
            enemyBullet.Project(bulletDirections[i - 1]);
            enemyBullet.damage = baseUnitData.attack;
        }
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
