using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YesterDayEventSO", menuName = "yeseterday/YesterDayEventSO")]
public class YesterDayEventSO : ScriptableObject
{
    public Sprite sprite;
    [TextArea]
    public string title;
    public string leftContent;
    public string rightContent;
    public RemindData leftEvent;
    public RemindData rightEvent;
    public List<EmoDataEntry> leftEffects = new List<EmoDataEntry>();
    public List<EmoDataEntry> rightEffects = new List<EmoDataEntry>();

}
