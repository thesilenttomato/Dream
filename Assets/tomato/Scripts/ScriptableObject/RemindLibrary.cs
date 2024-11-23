using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RemindLibrary", menuName = "Remind/RemindLibrary")]
public class RemindLibrary : ScriptableObject
{
    public List<RemindData> remindList;
    public List<RemindData> remindPool;
    
}

