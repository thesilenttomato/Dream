using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EmoDataData", menuName = "Emo/EmoDataLibrary")]
public class EmoLibrary : ScriptableObject
{
    public List<EmoDataEntry> emoDataList = new List<EmoDataEntry>();
}
[System.Serializable]
public class EmoDataEntry
{
    public EmoType emoType;
    public int amount;
    
}