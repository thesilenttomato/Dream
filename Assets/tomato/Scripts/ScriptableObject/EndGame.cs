using UnityEngine;
[CreateAssetMenu(fileName = "EndGame", menuName = "End/EndGame")]
public class EndGame : ScriptableObject
{
    public Sprite sprite;
    [TextArea]
    public string description;

    public string title;
}