using UnityEngine;

[CreateAssetMenu(fileName = "WeapenData", menuName = "Weapen/WeapenData")]
public class WeapenData : ScriptableObject
{
    public Sprite Sprite;
    public string name;
    [TextArea]
    public string description;

}
