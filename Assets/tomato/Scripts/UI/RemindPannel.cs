
using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class RemindPannel : MonoBehaviour
{
    public  EmoLibrary playerEmo;
    private YesterdayDataEntry thisYesterdayData;
    public bool choceLeft;
    private List<Button > Buttons = new List<Button>();
    private VisualElement Root;
    private Button leftButton;
    private Button rightButton;
    private Button confirmButton;
    private VisualElement root;
    private VisualElement lifeContainer;
    private List<Button> buttons = new List<Button>();
    private Label title;
    private Label leftT;
    private Label leftE;
    private Label rightT;
    private Label rightE;
    public IntVarible hourVarible;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public int hour{ get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }
    [Header("广播事件")]
    public ObjectEventSO finishPick;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        lifeContainer = root.Q<VisualElement>("LifeContainer");
        leftButton = root.Q<Button>("Left");
        rightButton = root.Q<Button>("Right");
        confirmButton = root.Q<Button>("Confirm");
        title = root.Q<Label>("title");
        leftT  = root.Q<Label>("LeftContent");
        leftE = root.Q<Label>("LeftEff");
        rightT = root.Q<Label>("RightContent");
        rightE = root.Q<Label>("RightEff");
        buttons.Clear();
        buttons.Add(leftButton);
        buttons.Add(rightButton);
        confirmButton.clicked += () => Confirm();
        leftButton.clicked += () => OnClicked(leftButton, true);
        rightButton.clicked += () => OnClicked(rightButton, false);
        Time.timeScale = 0;
        show();
    }

    private void show()
    {
        
    }
  

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    private void OnClicked(Button Button,bool left)
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            
            if (Button == Buttons[i])
            {
               
                Buttons[i].style.borderBottomWidth=20f;
                Buttons[i].style.borderLeftWidth=20f;
                Buttons[i].style.borderRightWidth=20f;
                Buttons[i].style.borderTopWidth=20f; 
                Buttons[i].pickingMode = PickingMode.Ignore; 
                
                if (Buttons[i].ClassListContains("turnbutton"))
                {
                    Buttons[i].ToggleInClassList("turnbutton");
                }
                
            }
            else
            {
               
                Buttons[i].style.borderBottomWidth=0f;
                Buttons[i].style.borderLeftWidth=0f;
                Buttons[i].style.borderRightWidth=0f;
                Buttons[i].style.borderTopWidth=0f;
                Buttons[i].pickingMode = PickingMode.Position;
                if (!Buttons[i].ClassListContains("turnbutton"))
                {
                    Buttons[i].ToggleInClassList("turnbutton");
                }
            }
        }
    }
    private string ShowEmo(EmoType emoType)
    {
        string emo = "";
        switch (emoType)
        {
            case(EmoType.Happness):
                emo = "喜悦+";
                return emo;
            case(EmoType.Sadness):
                emo = "悲伤+";
                return emo;
            case(EmoType.Calmness):
                emo = "平静+";
                return emo;
            case(EmoType.Fear):
                emo = "恐惧+";
                return emo;
            case(EmoType.Hate):
                emo = "厌恶+";
                return emo;
            case(EmoType.Shame):
                emo = "羞愧+";
                return emo;
            case(EmoType.Anger):
                emo = "愤怒+";
                return emo;
            case(EmoType.Astonishment):
                emo = "惊讶+";
                return emo;
            case (EmoType.AddHour):
            {
                emo = "睡眠时间+";
                return emo;
            }
            case (EmoType.MinHour):
            {
                emo = "睡眠时间-";
                return emo;
            }
        }

        return null;
    }
    private void Confirm()
    {
        audioSource.PlayOneShot(audioClip);
        if (choceLeft)
        {
            var newEmo = new EmoDataEntry()
            {
                emoType = thisYesterdayData.leftEmoType,
                amount = thisYesterdayData.leftAmount,
            };
            string[] strings = newEmo.emoType.ToString().Split(',');
            for (int i = 0; i < strings.Length; i++)
            {
                string roomtype =  strings[i];
                EmoType emoType = (EmoType)Enum.Parse(typeof(EmoType), roomtype);
                var target = playerEmo.emoDataList.Find(t => t.emoType == emoType);
                if (target != null)
                {
                    target.amount+= newEmo.amount;
                }
                else
                {
                    if (emoType == EmoType.AddHour)
                    {
                        hour -= 1;
                        if (hour < 0)
                        {
                            hour += 24;
                        }
                    }
                    else if (emoType == EmoType.MinHour)
                    {
                        hour += 1;
                        if (hour >= 24)
                        {
                            hour -= 24;
                        }
                    }
                }
            }
            
        }
        else
        {
            var newEmo = new EmoDataEntry()
            {
                emoType = thisYesterdayData.rightEmoType,
                amount = thisYesterdayData.rightAmount,
            };
            string[] strings = newEmo.emoType.ToString().Split(',');
            for (int i = 0; i < strings.Length; i++)
            {
                string roomtype =  strings[i];
                EmoType emoType = (EmoType)Enum.Parse(typeof(EmoType), roomtype);
                var target = playerEmo.emoDataList.Find(t => t.emoType == emoType);
                if (target != null)
                {
                    target.amount+= newEmo.amount;
                }
                else
                {
                    if (emoType == EmoType.AddHour)
                    {
                        hour -= 1;
                        if (hour < 0)
                        {
                            hour += 24;
                        }
                    }
                    else if (emoType == EmoType.MinHour)
                    {
                        hour += 1;
                        if (hour >= 24)
                        {
                            hour -= 24;
                        }
                    }
                }
            }
        }
        finishPick.RaiseEvent(null,this);
    }
    
}
