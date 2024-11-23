using System.Xml.Serialization;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float maxLifetime = 10.0f;
    private Rigidbody2D _rigidbody;
    public int bulletType = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (bulletType == 1)
        {
            _rigidbody.linearDamping = 2.5f;
            maxLifetime = 2f;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
        }
        Destroy(gameObject, maxLifetime);
    }

    public void Update()
    {

    }
    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
