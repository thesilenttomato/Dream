using UnityEngine;

public class Books : MonoBehaviour
{
    public Player player;
    public Transform center; 
    private float radius = 1f; 
    private float speed = 1f;
    public float angle; 

    void Start()
    {
        if (center == null)
        {
            center = transform.parent; 
        }
    }

    void Update()
    {
        // 每帧更新角度
        angle += speed * Time.deltaTime;

        // 计算新的位置
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // 设置新的位置
        transform.position = center.position + new Vector3(x, y, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.withstandCount += 1;
    }
}
