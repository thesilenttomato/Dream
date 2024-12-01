using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EmoManger : MonoBehaviour
{
    public List<int> selectedIndexes = new List<int>();
    public EmoLibrary playerEmoLibrary;
    public UIDocument uDocument;
    private VisualElement emoContainer;
    public VisualTreeAsset EmoTemple;
    public List<EmoType> changedEmolist = new List<EmoType>();
    public IntVarible hp;
    public IntVarible hour;
    public UIManger UIManger;

    [ContextMenu("游戏开始")]
    public void gameStart()
    {
        emoContainer = uDocument.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("EmoContainer");
    }

    public void ShowEmoChange(EmoType emoType, int amount)
   {
       
       VisualElement temple = EmoTemple.Instantiate();
       var  root  = temple.Q<VisualElement>("root");
       var  Emo  = root.Q<Label>("ChangedEmo");
       var  Amount  = root.Q<Label>("ChangedAmount");

       Emo.text = ShowEmo(emoType, GetAmountByEmoType(emoType));
       if (amount > 0)
       {
           Amount.text = "+"+amount.ToString();
           Amount.style.color = new StyleColor(Color.red);
       }
       else
       {
           
           Amount.text = amount.ToString();
           Amount.style.color = new StyleColor(Color.green);
       }
      
       emoContainer.Add(root);
       
   }
    


    public IEnumerator ShowEmoChangeCoroutine()
   {
       // Wait for 1 seconds
       yield return new WaitForSeconds(1f);

       // Call ShowEmoEnd after the delay
       ShowEmoEnd(changedEmolist);
   }
   public void ShowEmoEnd(List<EmoType> emoTypeList)
   {
       emoContainer.Clear();
       for (int i = 0; i < emoTypeList.Count; i++)
       {
           EmoType emoType = emoTypeList[i];
           VisualElement temple = EmoTemple.Instantiate();
           var  root  = temple.Q<VisualElement>("root");
           var  Emo  = root.Q<Label>("ChangedEmo");
           var  Amount  = root.Q<Label>("ChangedAmount");
           Emo.text = ShowEmo(emoType, GetAmountByEmoType(emoType));
           Amount.text = "";
           Amount.text = "";
           emoContainer.Add(root);
       }
       changedEmolist.Clear();
       StartCoroutine(ShowEmoClearCoroutine());
   }
   
public IEnumerator ShowEmoClearCoroutine()
{
    // Wait for 1 seconds
    yield return new WaitForSeconds(1f);

    // Call ShowEmoEnd after the delay
    emoContainer.Clear();
}

   

   public int GetAmountByEmoType(EmoType emoType)
   
   
   {
       foreach (EmoDataEntry entry in playerEmoLibrary.emoDataList)
       {
           if (entry.emoType == emoType)
           {
               return entry.amount;
           }
       }
        
       Debug.LogWarning($"没有找到匹配的 EmoType: {emoType}");
       return -1; // 返回 -1 表示未找到匹配项
   }

    public string ShowEmo(EmoType emoType, int amount)
    {
        string emo = "";
        string amountString = amount.ToString();
        switch (emoType)
        {
            case(EmoType.Happness):
                emo = "喜悦:"+amountString;
                return emo;
            case(EmoType.Sadness):
                emo = "悲伤:"+amountString;
                return emo;
            case(EmoType.Calmness):
                emo = "平静:"+amountString;
                return emo;
            case(EmoType.Fear):
                emo = "恐惧:"+amountString;
                return emo;
            case(EmoType.Hate):
                emo = "厌恶:"+amountString;
                return emo;
            case(EmoType.Shame):
                emo = "羞愧:"+amountString;
                return emo;
            case(EmoType.Anger):
                emo = "愤怒:"+amountString;
                return emo;
            case(EmoType.Astonishment):
                emo = "惊讶:"+amountString;
                return emo;
            case (EmoType.AddHour):
            {
                emo = "睡眠时间"+amountString;
                return emo;
            }
            
        }

        return null;
    }
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
        
        int amount;
        if (playerEmoLibrary.emoDataList[i].amount > 0)
        {
            amount = -playerEmoLibrary.emoDataList[i].amount*2;
        }
        else if (playerEmoLibrary.emoDataList[i].amount < 0)
        {
            amount = playerEmoLibrary.emoDataList[i].amount*2;
        }
        else
        {
            return;
        }
        ShowEmoChange(playerEmoLibrary.emoDataList[i].emoType, amount);
        playerEmoLibrary.emoDataList[i].amount = playerEmoLibrary.emoDataList[i].amount * -1;
        ShowEmoChangeCoroutine();
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

        ShowEmoChangeCoroutine();
    }

    private void reduce()
    {
        
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            if (playerEmoLibrary.emoDataList[i].amount > 0)
            {
                playerEmoLibrary.emoDataList[i].amount -= 1;
                ShowEmoChange(playerEmoLibrary.emoDataList[i].emoType, -1);
                changedEmolist.Add(playerEmoLibrary.emoDataList[i].emoType);
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
                
                ShowEmoChange(playerEmoLibrary.emoDataList[i].emoType, -playerEmoLibrary.emoDataList[j].amount);
                playerEmoLibrary.emoDataList[j].amount = 0;
                changedEmolist.Add(playerEmoLibrary.emoDataList[i].emoType);
            }else if (playerEmoLibrary.emoDataList[j].amount > 0)
            {
                playerEmoLibrary.emoDataList[j].amount -= 2;
                ShowEmoChange(playerEmoLibrary.emoDataList[i].emoType, -2);
                changedEmolist.Add(playerEmoLibrary.emoDataList[i].emoType);
            }else if (playerEmoLibrary.emoDataList[j].amount < 0)
            {
                playerEmoLibrary.emoDataList[j].amount += 2;
                ShowEmoChange(playerEmoLibrary.emoDataList[i].emoType, 2);
                changedEmolist.Add(playerEmoLibrary.emoDataList[i].emoType);
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
    public void at7()
    {
        if (hour.currentVaule >= 7 && hour.currentVaule <= 12)
        {
            ReduceEmo();
        }
        UIManger.OpenEmoPannel();
    }
    public void ReduceEmo()
    {
        for (int i  = 0; i  < playerEmoLibrary.emoDataList.Count; i ++)
        {
            if (Mathf.Abs(playerEmoLibrary.emoDataList[i].amount) <= 6)
            {
                
                playerEmoLibrary.emoDataList[i].amount = 0;
                
            }
            else
            {
                if (playerEmoLibrary.emoDataList[i].amount > 0)
                {
                    
                    playerEmoLibrary.emoDataList[i].amount -= 6;
                }
                else
                {
                    
                    playerEmoLibrary.emoDataList[i].amount += 6;
                }
            }
        }
        
    }

    public void HpPlus()
    {
        hp.maxVaule += 1;
    }

    
}

