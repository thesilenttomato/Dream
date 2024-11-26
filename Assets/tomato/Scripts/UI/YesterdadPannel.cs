using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class YesterdadPannel : MonoBehaviour
{
    public  EmoLibrary playerEmo;
    public RemindLibrary remindLibrary;
    public YesterdayLibrary yesterdayEvent;
    public int index;
   private YesterDayEventSO thisYesterdayData;
   public bool choceLeft;
   public ObjectEventSO GameStartSO;
   public IntVarible hourVarible;
   public AudioSource audioSource;
   public AudioClip button1;
   public int hour{ get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }
  
    private VisualElement root;
    private VisualElement lifeContainer;
    private Button leftButton;
    private Button rightButton;
    private Button confirmButton;
    private List<Button> buttons = new List<Button>();
    private Label title;
    private Label leftT;
    private Label leftE;
    private Label rightT;
    private Label rightE;

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
        index = 1;
        show();
    }

    [ContextMenu("show")]
    private void show()
    {
        
        switch (index)
        {
            case 1:
                 thisYesterdayData = yesterdayEvent.dayEvents_1st[Random.Range(0, yesterdayEvent.dayEvents_1st.Count)];
                break;
            case 2:
                 thisYesterdayData = yesterdayEvent.dayEvents_2nd[Random.Range(0, yesterdayEvent.dayEvents_2nd.Count)];
                break;
            case 3:
                 thisYesterdayData = yesterdayEvent.dayEvents_3rd[Random.Range(0, yesterdayEvent.dayEvents_3rd.Count)];
                break;
            case 4:
                 thisYesterdayData = yesterdayEvent.dayEvents_4th[Random.Range(0, yesterdayEvent.dayEvents_4th.Count)];
                break;
            case 5:
                 thisYesterdayData = yesterdayEvent.dayEvents_5th[Random.Range(0, yesterdayEvent.dayEvents_5th.Count)];
                break;
        }

        lifeContainer.style.backgroundImage = new StyleBackground(thisYesterdayData.sprite);
        title.text = thisYesterdayData.title;
        leftT.text = thisYesterdayData.leftContent;
        rightT.text = thisYesterdayData.rightContent;
        leftE.text = "";
        for (int i = 0; i < thisYesterdayData.leftEffects.Count; i++)
        {
            EmoType emoType = thisYesterdayData.leftEffects[i].emoType;
            leftE.text += ShowEmo(emoType,thisYesterdayData.leftEffects[i].amount);
        }
        rightE.text = "";
        for (int i = 0; i < thisYesterdayData.rightEffects.Count; i++)
        {
            EmoType emoType = thisYesterdayData.rightEffects[i].emoType;
            rightE.text += ShowEmo(emoType,thisYesterdayData.rightEffects[i].amount);
        }
        confirmButton.SetEnabled(false);
        if (confirmButton.ClassListContains("turnbutton"))
        {
            confirmButton.ToggleInClassList("turnbutton");
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            var labels = buttons[i].Query<Label>().ToList();
            foreach (var label in labels)
            {
                label.style.fontSize = 60f; 
            }
            buttons[i].pickingMode = PickingMode.Position;
            if (!buttons[i].ClassListContains("turnbutton"))
            {
                buttons[i].ToggleInClassList("turnbutton");
            }
        }

    }
   /* private EmoType GetRandomRoomType(EmoType falgs)
    {
        string[] strings = falgs.ToString().Split(',');
        string roomtype =  strings[Random.Range(0, strings.Length)];
        EmoType emoType = (EmoType)Enum.Parse(typeof(EmoType), roomtype);
        return emoType;
    }*/

    private string ShowEmo(EmoType emoType, int amount)
    {
        string emo = "";
        string amountString = "";
        if (amount > 0)
        {
            amountString = "+"+amount.ToString();
        }
        else
        {
            amountString = amount.ToString();
        }
        switch (emoType)
        {
            case(EmoType.Happness):
                emo = "喜悦"+amountString;
                return emo;
            case(EmoType.Sadness):
                emo = "悲伤"+amountString;
                return emo;
            case(EmoType.Calmness):
                emo = "平静"+amountString;
                return emo;
            case(EmoType.Fear):
                emo = "恐惧"+amountString;
                return emo;
            case(EmoType.Hate):
                emo = "厌恶"+amountString;
                return emo;
            case(EmoType.Shame):
                emo = "羞愧"+amountString;
                return emo;
            case(EmoType.Anger):
                emo = "愤怒"+amountString;
                return emo;
            case(EmoType.Astonishment):
                emo = "惊讶"+amountString;
                return emo;
            case (EmoType.AddHour):
            {
                emo = "睡眠时间"+amountString;
                return emo;
            }
            
        }

        return null;
    }
   

    private void Confirm()
    {
        audioSource.PlayOneShot(button1);
        if (choceLeft)
        {
            for (int i = 0; i < thisYesterdayData.leftEffects.Count; i++)
            {
                var newEmo = new EmoDataEntry()
                {
                    emoType = thisYesterdayData.leftEffects[i].emoType,
                    amount = thisYesterdayData.leftEffects[i].amount,
                };
                var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
                if (target != null)
                {
                    
                    target.amount+= newEmo.amount;
                }
                else
                {
                   
                    if (newEmo.emoType == EmoType.AddHour && newEmo.amount >0)
                    {
                        hour -= 1;
                        if (hour < 0)
                        {
                            hour += 24;
                        }
                    }
                    else if (newEmo.emoType == EmoType.AddHour && newEmo.amount < 0)
                    {
                        hour += 1;
                        if (hour >= 24)
                        {
                            hour -= 24;
                        }
                    }
                }
            }
            if (thisYesterdayData.leftEvent != null)
            {
                remindLibrary.remindPool.Add(thisYesterdayData.leftEvent);
            }
        }
        
        else
        {
            for (int i = 0; i < thisYesterdayData.rightEffects.Count; i++)
            {
                var newEmo = new EmoDataEntry()
                {
                    emoType = thisYesterdayData.rightEffects[i].emoType,
                    amount = thisYesterdayData.rightEffects[i].amount,
                };
                var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
                if (target != null)
                {
                    target.amount+= newEmo.amount;
                }
                else
                {
                   
                    if (newEmo.emoType == EmoType.AddHour && newEmo.amount > 0)
                    {
                        hour -= 1;
                        if (hour < 0)
                        {
                            hour += 24;
                        }
                    }
                    else if (newEmo.emoType == EmoType.AddHour && newEmo.amount < 0)
                    {
                        hour += 1;
                        if (hour >= 24)
                        {
                            hour -= 24;
                        }
                    }
                }
            }

            if (thisYesterdayData.rightEvent != null)
            {
                remindLibrary.remindPool.Add(thisYesterdayData.rightEvent);
            }
            
        }
        
        if (index == 5)
        {
            GameStartSO.RaiseEvent(null,this);
        }
        else
        {
            index++;
            show();
        }
    }
    
    private void OnClicked(Button Button,bool left)
    {
        audioSource.PlayOneShot(button1);
        confirmButton.SetEnabled(true);
        if (!confirmButton.ClassListContains("turnbutton"))
        {
            confirmButton.ToggleInClassList("turnbutton");
        }
        if (left)
        {
            choceLeft = true;
        }
        else
        {
            choceLeft = false;
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            
            if (Button == buttons[i])
            {
               
                var labels = buttons[i].Query<Label>().ToList();
                foreach (var label in labels)
                {
                    label.style.fontSize = 100f; 
                }

                buttons[i].pickingMode = PickingMode.Ignore; 
                
                if (buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
                
            }
            else
            {
               
                var labels = buttons[i].Query<Label>().ToList();
                foreach (var label in labels)
                {
                    label.style.fontSize = 60f; 
                }
                buttons[i].pickingMode = PickingMode.Position;
                if (!buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
            }
        }
    }
}
