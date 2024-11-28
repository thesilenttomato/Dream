using Unity.Burst.Intrinsics;
using UnityEngine;

public class Books : MonoBehaviour
{
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
    }
}
