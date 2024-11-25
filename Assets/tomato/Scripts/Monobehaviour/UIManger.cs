using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public GameObject Remind;
    public GameObject GamePlay;
    public GameObject GameSettings;
    public void Timeup()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            return;
        }
        Remind.SetActive(true);
    }

    public void FinishRemind()
    {
        Remind.SetActive(false);
    }

    public void CloseGamePlay()
    {
        GamePlay.SetActive(false);
    }

    public void OpenGameSettings()
    {
        GameSettings.SetActive(true);
    }
}
