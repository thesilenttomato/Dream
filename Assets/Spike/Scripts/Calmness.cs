using UnityEngine;

public class Calmness : MonoBehaviour
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

    private int bulletAmount = 4;

    private bool shootCheck = false;
    public Animator animator;
    public SpecialEffectAnimation specialEffectAnimationPrefab;
    public SpecialEffectAnimation specialEffectAnimation;
    //public 
    private Vector3 prePosition;
    private void Start()
    {
        baseUnitData = new BaseUnitData(8, 1, 10, 3, 100);
        gameManager = FindFirstObjectByType<GameManager>();
        specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
        specialEffectAnimation.calm_tail = true;
        specialEffectAnimation.transform.parent = transform;
        SpriteRenderer specialEffectAnimationSpriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
        specialEffectAnimationSpriteRenderer.sortingOrder = -9;
        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
        //Vector3 eulerRotation = targetRotation.eulerAngles;
        //specialEffectAnimation.transform.localEulerAngles = eulerRotation;
        if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 3 && Mathf.Abs(gameManager.emotionalQuantity[2]) < 6)
        {
            bulletAmount = 5;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 6 && Mathf.Abs(gameManager.emotionalQuantity[2]) < 8)
        {
            bulletAmount = 6;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 8)
        {
            bulletAmount = 8;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 5)
        {
            baseUnitData.life = 12;
        }
        if (Mathf.Abs(gameManager.emotionalQuantity[2]) >= 9)
        {
            transform.localScale = new Vector3(1, 1);
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
        baseUnitData.movementSpeed -= Time.deltaTime * 0.25f;
        /*Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float currentDistance = Vector3.Distance(transform.position, target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);*/
        //specialEffectAnimation.transform.localRotation = Quaternion.LookRotation(_rigidbody.linearVelocity,Vector3.up);
        //specialEffectAnimation.transform.eulerAngles = 

        if (specialEffectAnimation != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -direction.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            specialEffectAnimation.transform.localEulerAngles = eulerRotation + new Vector3(0, 0, 90);
        }

        transform.position += direction * baseUnitData.movementSpeed * Time.deltaTime;

        //time += Time.deltaTime;
        if (baseUnitData.movementSpeed < 0.01f)
        {
            if (!shootCheck)
            {
                if (specialEffectAnimation != null)
                {
                    //specialEffectAnimation.transform.parent = null;
                    specialEffectAnimation.DestroyGameObject();
                }
                shootCheck = true;
                animator.SetBool("attack", true);
                tag = "Invincible Enemy";
                gameObject.layer = 13;
                Invoke(nameof(Shoot), 0.4f);
                //Shoot();
                Destroy(gameObject, 0.96f);
            }
        }
        existTime += Time.deltaTime;
        if (existTime >= 15)
        {
            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y > 7.55f || transform.position.y < -7.55f)
            {
                Destroy(gameObject);
            }
        }
        //prePosition = transform.position;
    }

    private void Shoot()
    {
        /*Vector2[] bulletDirections = new Vector2[]
        {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
        };*/
        //InvestigationBullet overloadBullet = Instantiate(overloadBulletPrefab, transform.position, transform.rotation);
        //overloadBullet.Project(transform.up);
        float angle = 360 / bulletAmount;

        for (int i = 0; i < bulletAmount; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemyBullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemyBullet.sprite[1];
            enemyBullet.speed = baseUnitData.bulletSpeed;

            float angleInRadians = angle * i;
            Quaternion rotation = Quaternion.AngleAxis(angleInRadians, Vector3.forward);

            Vector3 newDirection = rotation * Vector3.up;
            //Debug.Log(newDirection);
            enemyBullet.Project(newDirection.normalized);

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, newDirection.normalized);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            enemyBullet.transform.eulerAngles = eulerRotation + new Vector3(0, 0, 135);

            enemyBullet.damage = baseUnitData.attack;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            baseUnitData.life -= bullet.damage;
            gameManager.Explosive(collision.GetContact(0).point, new Color(1f / 255f, 87f / 255f, 142f / 255f, 1.0f));
            enemySound enemySound = GetComponent<enemySound>();
            enemySound.Sound(Vector3.Distance(transform.position, target.position));
            //FindFirstObjectByType<GameManager>().OverloadDestroyed(this);
            if (baseUnitData.life <= 0)
            {
                if (specialEffectAnimation != null)
                {
                    specialEffectAnimation.DestroyGameObject();
                }
                gameManager.defeatedEmotion[2] += 1;
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
