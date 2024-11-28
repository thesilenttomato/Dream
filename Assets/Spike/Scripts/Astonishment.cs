using UnityEngine;
using UnityEngine.UIElements;

public class Astonishment : MonoBehaviour
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

    public float chargeDistance = 4f;
    private Vector3 chargePosition;
    private bool chargeState = false;
    //public float shootDistance = 7.5f;

    private float existTime;

    public float existTimeMax;

    private bool flee = false;

    public Vector3 fleeDirection;

    public bool chargeToDeath = false;
    private void Start()
    {
        baseUnitData = new BaseUnitData(2, 1, 8, 1.5f, 0);
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager.emotionalQuantity[4] >= 5 && gameManager.emotionalQuantity[4] < 15)
        {
            chargeDistance = chargeDistance * 1.15f;
        }
        if (gameManager.emotionalQuantity[4] >= 15)
        {
            chargeDistance = chargeDistance * 1.3f;
        }
        if (gameManager.emotionalQuantity[4] >= 9)
        {
            baseUnitData.movementSpeed = 1.75f;
        }
        if (gameManager.emotionalQuantity[4] >= 17)
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

            float currentDistance = Vector3.Distance(transform.position, target.position);

            //if (currentDistance > distance)
            //{
                //transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);
            //}
            //else
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, target.position, -baseUnitData.movementSpeed * Time.deltaTime * 0.5f);
            //}

            time += Time.deltaTime;
            if (currentDistance <= chargeDistance && time >= baseUnitData.attackInterval)
            {
                if (chargeState == false)
                {
                    //Debug.Log("SB");
                    chargeState = true;
                    Invoke(nameof(ChangeChargeState), 3);
                    time = 0;
                    chargePosition = target.position;
                    _rigidbody.AddForce((chargePosition - transform.position).normalized * 2500);
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, (chargePosition - transform.position).normalized);
                }

                //Debug.Log("SB");
                //Shoot();
                //Destroy(gameObject);
            }
            else
            {
                if (chargeState == false)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                    transform.position = Vector3.MoveTowards(transform.position, target.position, baseUnitData.movementSpeed * Time.deltaTime);
                }
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

                // 应用旋转
                fleeDirection = rotation * direction;
            }

            transform.position += fleeDirection * baseUnitData.movementSpeed * Time.deltaTime;
            //逃跑时方向旋转

            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ChangeChargeState()
    {
        chargeState = false;
        if (chargeToDeath)
        {
            Destroy(gameObject);
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
            gameManager.Explosive(collision.GetContact(0).point, Color.white);//颜色
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                if (chargeToDeath == false)
                {
                    gameManager.defeatedEmotion[4] += 1;
                }
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
