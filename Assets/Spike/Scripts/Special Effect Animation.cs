using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialEffectAnimation : MonoBehaviour
{
    public Animator animator;
    public bool calm_tail;
    public bool fear;
    public bool shame_smog;
    public Vector3 direction;
    public float DestroyTime = 99999;
    private float time;
    private void Update()
    {
        animator.SetBool("calm_tail", calm_tail);
        animator.SetBool("fear", fear);
        animator.SetBool("shame_smog", shame_smog);
        time += Time.deltaTime;
        if (fear)
        {
            transform.position += direction.normalized * 2 * Time.deltaTime;
            DestroyTime = 0.6f;
        }
        if (shame_smog)
        {
            DestroyTime = 0.25f;
        }

        if (time > DestroyTime)
        {
            Destroy(gameObject);
        }
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
