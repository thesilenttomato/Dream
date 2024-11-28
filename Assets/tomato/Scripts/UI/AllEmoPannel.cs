using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AllEmoPannel : MonoBehaviour
{
    private VisualElement root;
    private Label[] labels = new Label[8];
    private Button backButton;
    public EmoLibrary playerEmoLibrary;
    
    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        for (int i = 0; i < 8; i++)
        {
            labels[i] = root.Q<Label>((i).ToString());
            labels[i].text = ShowEmo(playerEmoLibrary.emoDataList[i].emoType,playerEmoLibrary.emoDataList[i].amount) ;
        }
        backButton = root.Q<Button>("Back");
        backButton.clicked += () => Back();
        
    }



    private void Back()
    {
        this.gameObject.SetActive(false);
    }

    private string ShowEmo(EmoType emoType, int amount)
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
}
