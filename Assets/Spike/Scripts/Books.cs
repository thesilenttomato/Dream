using UnityEngine;

public class Books : MonoBehaviour
{
    public GameManager gameManager;
    public Player player;
    public Transform center; 
    private float radius = 1f; 
    private float speed = 1f;
    public float angle;
    public Sprite[] sprites;

    public Animator animator;
    public bool book1;
    public bool book2;
    public bool book3;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (center == null)
        {
            center = transform.parent; 
        }
        animator.SetBool("book1", book1);
        animator.SetBool("book2", book2);
        animator.SetBool("book3", book3);
    }

    void Update()
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = center.position + new Vector3(x, y, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.withstandCount += 1;
        if (book1)
        {
            gameManager.Explosive(collision.GetContact(0).point, Color.blue);//ÑÕÉ«
        }
        if (book2)
        {
            gameManager.Explosive(collision.GetContact(0).point, Color.red);//ÑÕÉ«
        }
        if (book3)
        {
            gameManager.Explosive(collision.GetContact(0).point, new Color(1.0f, 0.5f, 0.0f));//ÑÕÉ«
        }

    }
}
