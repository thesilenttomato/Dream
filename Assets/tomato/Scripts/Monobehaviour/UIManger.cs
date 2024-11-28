using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public GameObject Remind;
    public GameObject GamePlay;
    public GameObject GameSettings;
    public GameObject rousePannel;
    public GameObject allEmoPannel;
    public void Timeup()
    {
       
        Remind.SetActive(true);
    }

    public void OpenEmoPannel()
    {
        allEmoPannel.SetActive(true);
    }

    public void FinishRemind()
    {
        Remind.SetActive(false);
    }

    public void OpenGamePlay()
    {
        GamePlay.SetActive(true);
    }

    public void CloseGamePlay()
    {
        GamePlay.SetActive(false);
    }

    public void OpenGameSettings()
    {
        GameSettings.SetActive(true);
    }

    public void OpenRousePannel()
    {
        CloseGamePlay();
        rousePannel.SetActive(true);
    }
}
