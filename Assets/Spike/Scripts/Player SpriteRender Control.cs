using UnityEngine;

public class PlayerSpriteRenderControl : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;
    public Animator animator;
    public Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    private bool move;
    private bool stop;
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Start()
    {
        animator.SetBool("type_1", gameManager.playerType[0]);
        animator.SetBool("type_2", gameManager.playerType[1]);
        //animator.SetBool("type_3", gameManager.playerType[2]);
        animator.SetBool("type_4", gameManager.playerType[3]);
    }
    private void Update()
    {
        animator.SetBool("move", move);
        animator.SetBool("stop", stop);

        if (gameManager.playerType[3])
        {
            if (_rigidbody.linearVelocity.magnitude < 0.1f)
            {
                stop = true;
                move = false;
            }
            else
            {
                move = true;
                stop = false;
            }
        }
        transform.position = player.transform.position;

        if (gameManager.playerType[3] || gameManager.playerType[1])
        {
            if (player.transform.rotation.z < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        if (gameManager.playerType[0])
        {
            if (player.transform.rotation.z < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
