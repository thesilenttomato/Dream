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
    public bool ss_1;
    public bool ss_2;
    public bool ss_3;
    public bool ss_4;
    public bool ss_5;
    public bool ss_6;
    public bool ss_7;
    public bool ss_8;
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
        animator.SetBool("ss_1", ss_1);
        animator.SetBool("ss_2", ss_2);
        animator.SetBool("ss_3", ss_3);
        animator.SetBool("ss_4", ss_4);
        animator.SetBool("ss_5", ss_5);
        animator.SetBool("ss_6", ss_6);
        animator.SetBool("ss_7", ss_7);
        animator.SetBool("ss_8", ss_8);

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
        if (ss_1 || ss_2 || ss_3 || ss_4 || ss_5 || ss_6 || ss_7 || ss_8)
        {
            DestroyTime = 1.167f;
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
