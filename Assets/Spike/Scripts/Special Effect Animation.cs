using UnityEngine;

public class SpecialEffectAnimation : MonoBehaviour
{
    public Animator animator;
    public bool calm_tail;
    private void Update()
    {
        animator.SetBool("calm_tail", calm_tail);
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
