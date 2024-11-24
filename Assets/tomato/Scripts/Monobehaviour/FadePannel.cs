using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class FadePannel : MonoBehaviour
{
    private VisualElement fade;

    private void Awake()
    {
        fade = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("fade");
    }

   public void FadeIn(float duration)
    {
        DOVirtual.Float(0f, 1f, duration, vaule =>
        {
            fade.style.opacity = vaule;
        });
    }
    public void FadeOut(float duration)
    {
        DOVirtual.Float(1f, 0f, duration, vaule =>
        {
            fade.style.opacity = vaule;
        });
    }
}
