using UnityEngine;

[CreateAssetMenu(fileName = "WeapenData", menuName = "Weapen/WeapenData")]
public class WeapenData : ScriptableObject
{
    public float ATL;  //攻击间隔
    public float RG; //射程
    public float SR; //溅射范围
    public Sprite standSprite;
    public string name;
    [TextArea]
    public string description;

}
