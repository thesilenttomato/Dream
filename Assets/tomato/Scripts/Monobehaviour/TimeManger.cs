using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public GameObject Remind;
    public void Timeup()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            return;
        }
        Remind.SetActive(true);
    }
}
