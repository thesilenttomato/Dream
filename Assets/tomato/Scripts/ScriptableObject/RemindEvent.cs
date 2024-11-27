using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemindData", menuName = "Remind/RemindData")]
public class RemindData : ScriptableObject
{
    
    public Sprite sprite;
    [TextArea]
    public string title;
    public string leftContent;
    public string leftPlayerContent;
    public List<EmoDataEntry> leftEmoList = new List<EmoDataEntry>();
    public List<ObjectEventSO> leftRemindEvent;
    public string rightContent;
    public string rightPlayerContent;
    public List<EmoDataEntry> rightEmoList = new List<EmoDataEntry>();
    public List<ObjectEventSO> rightRemindEvent;

}