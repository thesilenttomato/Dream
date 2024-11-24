using Unity.VisualScripting;
using UnityEngine;

public class EmoManger : MonoBehaviour
{
    public EmoLibrary playerEmoLibrary;
    [ContextMenu("游戏开始")]

    public void OnDisable()
    {
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            emoDataEntry.amount = 0;
        }
    }

    private void show()
    {
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            if (emoDataEntry.emoType == EmoType.Fear)
            {
                Debug.Log("数量:"+ emoDataEntry.amount);
            }
        }
    }
}

