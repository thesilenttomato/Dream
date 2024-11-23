using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeapenLibrary", menuName = "Weapen/WeapenLibrary")]
public class WeapenLibrary : ScriptableObject
{
    public List<WeapenDataEntry> weapenLibrary;
    
}

[System.Serializable]
public class WeapenDataEntry
{
    public WeapenData weapenData;

}