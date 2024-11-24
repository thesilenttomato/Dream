using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "YesterdayEvent", menuName = "yeseterday/YesterdayEvent")]
public class YesterdayEvent : ScriptableObject
{
   public List<YesterdayDataEntry> dayEvents_1st = new List<YesterdayDataEntry>();
   public List<YesterdayDataEntry> dayEvents_2nd = new List<YesterdayDataEntry>();
   public List<YesterdayDataEntry> dayEvents_3rd = new List<YesterdayDataEntry>();
   public List<YesterdayDataEntry> dayEvents_4th = new List<YesterdayDataEntry>();
   public List<YesterdayDataEntry> dayEvents_5th = new List<YesterdayDataEntry>();
}
[System.Serializable]
public class YesterdayDataEntry
{
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
