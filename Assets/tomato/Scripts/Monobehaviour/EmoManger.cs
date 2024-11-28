using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EmoManger : MonoBehaviour
{
    public List<int> selectedIndexes = new List<int>();
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

    public void ChangeEmoData(int i)
    {
        playerEmoLibrary.emoDataList[i].amount = playerEmoLibrary.emoDataList[i].amount * -1;
    }

    public void emoReduce(int i )
    {
        if ( i == 1)
        {
            reduce();
        }
        else
        {
            bigReduce();
        }
    }

    private void reduce()
    {
        
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            if (playerEmoLibrary.emoDataList[i].amount > 0)
            {
                playerEmoLibrary.emoDataList[i].amount -= 1;
            }
        }
    }

    private void bigReduce()
    {
        SelectRandomIndexes(4);
        for (int i = 0; i < selectedIndexes.Count; i++)
        {
            int j = selectedIndexes[i];
            if (math.abs(playerEmoLibrary.emoDataList[j].amount) == 1 || playerEmoLibrary.emoDataList[j].amount == 0)
            {
                playerEmoLibrary.emoDataList[j].amount = 0;
            }else if (playerEmoLibrary.emoDataList[j].amount > 0)
            {
                playerEmoLibrary.emoDataList[j].amount -= 2;
            }else if (playerEmoLibrary.emoDataList[j].amount < 0)
            {
                playerEmoLibrary.emoDataList[j].amount += 2;
            }
           
        }
    }
    void SelectRandomIndexes(int numberOfIndexes)
    {
        selectedIndexes.Clear();  // 清空之前的选择

        // 需要至少numberOfIndexes个不同的索引
        if (playerEmoLibrary.emoDataList.Count < numberOfIndexes)
        {
            Debug.LogError("列表的元素不足以选择这么多索引");
            return;
        }

        // 使用HashSet来保证索引不重复
        HashSet<int> selectedSet = new HashSet<int>();

        while (selectedSet.Count < numberOfIndexes)
        {
            int randomIndex = Random.Range(0, playerEmoLibrary.emoDataList.Count);
            selectedSet.Add(randomIndex);
        }

        // 将选择的索引从HashSet转移到列表中
        selectedIndexes.AddRange(selectedSet);
    }

    
}

