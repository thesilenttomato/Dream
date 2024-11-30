using UnityEngine;

public class SpecialEffectAnimation : MonoBehaviour
{
    public Animator animator;
    public bool calm_tail;
    public bool fear;
    private void Update()
    {
        animator.SetBool("calm_tail", calm_tail);
        animator.SetBool("fear", fear);
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
