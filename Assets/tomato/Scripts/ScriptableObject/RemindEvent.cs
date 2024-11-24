using UnityEngine;

[CreateAssetMenu(fileName = "RemindData", menuName = "Remind/RemindData")]
public class RemindData : ScriptableObject
{
    public ObjectEventSO remindEvent;
    public Sprite sprite;
    public string name;
    public string title;   
    [TextArea]
    public string myDescription;   
    [TextArea]
    public string enemyDescription;

    [TextArea]
    public string description;   
}