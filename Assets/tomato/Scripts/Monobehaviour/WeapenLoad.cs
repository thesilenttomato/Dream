using UnityEngine;
using UnityEngine.UIElements;

public class weapenLoad : MonoBehaviour
{

    public Transform player; // 主角的 Transform
    public Vector2 offset = new Vector2(100, 100); // 右上角偏移量
    public Material radialProgressMaterial; // 绑定的圆弧材质
    public float fillDuration = 2f; // 圆弧填充时间
    private Canvas canvas;
    private RectTransform rectTransform;
    private bool isActive = false;
    private float currentProgress = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null || radialProgressMaterial == null)
        {
            Debug.LogError("Canvas 或材质未绑定！");
            return;
        }
        radialProgressMaterial.SetFloat("_Progress", 0);
        gameObject.SetActive(false); // 默认隐藏进度条
    }

    void Update()
    {
        if (!isActive) return;

        // 跟随主角位置更新
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 uiPos = screenPos + new Vector3(offset.x, offset.y, 0);
        rectTransform.position = uiPos;

        // 更新进度条填充
        currentProgress += Time.deltaTime / fillDuration;
        radialProgressMaterial.SetFloat("_Progress", Mathf.Clamp01(currentProgress));

        if (currentProgress >= 1f)
        {
            HideProgressBar(); // 填满后隐藏
        }
    }

    [ContextMenu("展示")]
    public void ShowProgressBar()
    {
        currentProgress = 0f;
        radialProgressMaterial.SetFloat("_Progress", 0);
        gameObject.SetActive(true);
        isActive = true;
    }

    public void HideProgressBar()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}




