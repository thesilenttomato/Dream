using UnityEngine;
[CreateAssetMenu(fileName = "IntroSO", menuName = "IntroSO")]
public class IntroSO : ScriptableObject
{
    public Sprite sprite;
    public string title;
    [TextArea]
    public string description;
}
