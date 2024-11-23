using UnityEngine;

public class UIManger : MonoBehaviour
{
    public GameObject Remind; 
    public void Timeup()
    {
        Remind.SetActive(true);
    }
}
