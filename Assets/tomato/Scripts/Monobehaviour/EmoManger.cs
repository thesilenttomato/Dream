using Unity.VisualScripting;
using UnityEngine;

public class EmoManger : MonoBehaviour
{
    public EmoLibrary startEmoLibrary;
    public EmoLibrary playerEmoLibrary;
    [ContextMenu("游戏开始")]
    public void GameStart()
    {
        for (int i = 0; i < startEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = startEmoLibrary.emoDataList[i];
            playerEmoLibrary.emoDataList.Add(emoDataEntry);
        }
    }

    public void OnDisable()
    {
        playerEmoLibrary.emoDataList.Clear();
    }
}

