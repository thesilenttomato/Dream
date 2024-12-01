using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialEffectAnimation : MonoBehaviour
{
    public Animator animator;
    public bool calm_tail;
    public bool fear;
    public bool shame_smog;
    public bool yanhua_first;
    public bool yanhua;
    public Vector3 direction;
    public float DestroyTime = 99999;
    private float time;
    private void Update()
    {
        animator.SetBool("calm_tail", calm_tail);
        animator.SetBool("fear", fear);
        animator.SetBool("shame_smog", shame_smog);
        animator.SetBool("yanhua_first", yanhua_first);
        animator.SetBool("yanhua", yanhua);
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
        if (yanhua_first)
        {
            transform.position += direction.normalized * 2 * Time.deltaTime;
            Invoke(nameof(YanhuaChange), Random.Range(5f, 8f));
            time = 0;
        }

        if (yanhua)
        {
            transform.position += direction.normalized * 0.2f * Time.deltaTime;
            DestroyTime = 0.917f;
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
    private void YanhuaChange()
    {
        yanhua_first = false;
        yanhua = true;
    }
}
