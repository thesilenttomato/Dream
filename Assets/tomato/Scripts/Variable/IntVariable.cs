using UnityEngine;
[CreateAssetMenu(fileName = "IntVarible", menuName = "Varible/IntVarible")]
public class IntVarible : ScriptableObject
{
    public int maxVaule;
    public int currentVaule;
    public IntEventSO IntVauleChange;
    [TextArea]
    [SerializeField]private string description;
    public void SetValue(int vaule)
    {
        currentVaule = vaule;
        IntVauleChange?.RaiseEvent(vaule, this);

    }
}
