using UnityEngine;

[CreateAssetMenu(fileName = "RemindData", menuName = "Remind/RemindData")]
public class RemindData : ScriptableObject
{
    public ObjectEventSO remindEvent;
    public Sprite sprite;
    [TextArea]
    public string title;
    public string leftContent;
    public EmoType leftEmoType;
    public int leftAmount;
    public string rightContent;
    public EmoType rightEmoType;
    public int rightAmount;

}