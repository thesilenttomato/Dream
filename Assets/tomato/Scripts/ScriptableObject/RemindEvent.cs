using UnityEngine;

[CreateAssetMenu(fileName = "RemindData", menuName = "Remind/RemindData")]
public class RemindData : ScriptableObject
{
    public Sprite sprite;
    public string name;
    
    [TextArea]
    public string description;    
    [TextArea]
    public string myDescription;   
    [TextArea]
    public string enemyDescription;

}