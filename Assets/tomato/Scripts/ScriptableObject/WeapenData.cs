using UnityEngine;

[CreateAssetMenu(fileName = "WeapenData", menuName = "Weapen/WeapenData")]
public class WeapenData : ScriptableObject
{
    public int id;
    public int state;  // -1表示未持有,0表示持有，1是A1强化,2是A2，3是B1，4是B2
    public Sprite Sprite;
    public string name;
    [TextArea]
    public string description;

    public float ATK;
    public float ATL;
    public float V;



}
