using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialEffectAnimation : MonoBehaviour
{
    public SpecialEffectAnimation specialEffectAnimationPrefab;
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
    public bool yh;
    public Vector3 direction;
    public float DestroyTime = 99999;
    private float time;
    public void Start()
    {
        if (yanhua_first)
        {
            float a = Random.Range(5f, 8f);
            Invoke(nameof(YanhuaChange), a);
        }
    }
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
        animator.SetBool("yh", yh);

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
        if (yh)
        {
            DestroyTime = 0.667f;
        }
        if (time > DestroyTime)
        {
            if (yanhua)
            {
                YanhuaSummon();
            }
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
    private void YanhuaSummon()
    {
        SpecialEffectAnimation specialEffectAnimation = Instantiate(specialEffectAnimationPrefab, transform.position, Quaternion.identity);
        specialEffectAnimation.yh = true;
        specialEffectAnimation.yanhua = false;
        SpriteRenderer spriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -9;
        /*SpriteRenderer spriteRenderer = specialEffectAnimation.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;*/
    }
}
