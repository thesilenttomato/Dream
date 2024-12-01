using System;
using System.Collections;
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
   [Header("事件广播")]
   public ObjectEventSO GameStartSO;
   public ObjectEventSO allEmoSO;
   public IntVarible hourVarible;
   public AudioSource audioSource;
   public AudioClip button1;
   public List<EmoType> changedEmolist = new List<EmoType>();
   public int hour{ get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }
  
    private VisualElement root;
    private VisualElement lifeContainer;
    private VisualElement emoContainer;
    public VisualTreeAsset EmoTemple;
    private Button leftButton;
    private Button rightButton;
    private Button confirmButton;
    private List<Button> buttons = new List<Button>();
    private Label title;
    private Label leftT;
    private Label rightT;
    public List<YesterDayEventSO> impactedDayReminds = new List<YesterDayEventSO>();
    public List<RemindData> impactedNightReminds = new List<RemindData>();
    private Button AllEmoButton;
    private VisualElement hourHand;
    private Label timeLabel;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        emoContainer = root.Q<VisualElement>("EmoContainer");
        lifeContainer = root.Q<VisualElement>("LifeContainer");
        leftButton = root.Q<Button>("Left");
        rightButton = root.Q<Button>("Right");
        confirmButton = root.Q<Button>("Confirm");
        title = root.Q<Label>("title");
        leftT  = root.Q<Label>("LeftContent");
        rightT = root.Q<Label>("RightContent");
        hourHand = root.Q<VisualElement>("HourHand");
        timeLabel = root.Q<Label>("Time");
        
        AllEmoButton = root.Q<Button>("Emo");
        AllEmoButton.clicked += () => OpenEmoPannel();
        buttons.Clear();
        buttons.Add(leftButton);
        buttons.Add(rightButton);
        confirmButton.clicked += () => Confirm();
        leftButton.clicked += () => OnClicked(leftButton, true);
        rightButton.clicked += () => OnClicked(rightButton, false);
        index = 1;
        show();
    }
    private void UpdateClock()
    {
        
        // 时针每小时转动 30°，每分钟进阶 0.5°
        float hourAngle = hour * 30f ;
        
        hourHand.style.rotate = new Rotate(new Angle(hourAngle, AngleUnit.Degree));
    }
    private void UpdateTimeLabel()
    {
        
        timeLabel.text = $"{hour}:00";
        
      
    }

    private void OpenEmoPannel()
    {
        allEmoSO.RaiseEvent(null,this);
    }
    public void RemoveMatchingRemindData()
    {
        // 遍历 impactedDayReminds 找到与 thisYesterdayData 一致的索引
        for (int i = 0; i < impactedDayReminds.Count; i++)
        {
            if (impactedDayReminds[i] == thisYesterdayData)
            {
                // 确保索引在 impactedNightReminds 范围内
                if (i < impactedNightReminds.Count)
                {
                    RemindData elementB = impactedNightReminds[i];
                    // 检查 remindLibrary.remindPool 中是否包含元素 B
                    if (remindLibrary.remindPool.Contains(elementB))
                    {
                        remindLibrary.remindPool.Remove(elementB);
                    }
                }
            }
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnClicked(leftButton, true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnClicked(rightButton, false);
        }

        if (Input.GetKeyDown(KeyCode.J) && (confirmButton.enabledSelf))
        {
            Confirm();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenEmoPannel();
        }
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

        RemoveMatchingRemindData();
        lifeContainer.style.backgroundImage = new StyleBackground(thisYesterdayData.sprite);
        title.text = thisYesterdayData.title;
        leftT.text = thisYesterdayData.leftContent;
        rightT.text = thisYesterdayData.rightContent;
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
   private IEnumerator ShowEmoChangeCoroutine()
   {
       // Wait for 0.5 seconds
       yield return new WaitForSeconds(0.5f);

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
   }

   private int GetAmountByEmoType(EmoType emoType)
   {
       
       foreach (EmoDataEntry entry in playerEmo.emoDataList)
       {
           if (entry.emoType == emoType)
           {
               return entry.amount;
           }
           
       }
        
       Debug.LogWarning($"没有找到匹配的 EmoType: {emoType}");
       return -1; // 返回 -1 表示未找到匹配项
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
                emo = "羞耻:"+amountString;
                return emo;
            case(EmoType.Anger):
                emo = "愤怒:"+amountString;
                return emo;
            case(EmoType.Astonishment):
                emo = "惊讶:"+amountString;
                return emo;
            case (EmoType.AddHour):
            {
                emo = "睡眠时间:"+amountString;
                return emo;
            }
            
        }

        return null;
    }
   

    private void Confirm()
    {
        
        audioSource.PlayOneShot(button1);
        List<EmoDataEntry> effects = choceLeft ? thisYesterdayData.leftEffects : thisYesterdayData.rightEffects;
        var eventToAdd = choceLeft ? thisYesterdayData.leftEvent : thisYesterdayData.rightEvent;

        ProcessEmoEffects(effects);

        if (eventToAdd != null)
        {
            remindLibrary.remindPool.Add(eventToAdd);
        }

        if (index == 5)
        {
            GameStartSO.RaiseEvent(null, this);
        }
        else
        {
            index++;
            show();
        }
    }

    private void ProcessEmoEffects(List<EmoDataEntry> effects)
    {
        emoContainer.Clear();
        foreach (var effect in effects)
        {
            var newEmo = new EmoDataEntry()
            {
                emoType = effect.emoType,
                amount = effect.amount,
            };

            var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
            if (target != null)
            {
                ShowEmoChange(newEmo.emoType, newEmo.amount);
                changedEmolist.Add(newEmo.emoType);
                target.amount += newEmo.amount;
                
            }
            else
            {
                HandleSpecialEmoType(newEmo);
            }
            
        }
        StartCoroutine(ShowEmoChangeCoroutine());
    }

    private void HandleSpecialEmoType(EmoDataEntry emo)
    {
        if (emo.emoType == EmoType.AddHour)
        {
            AdjustHour(emo.amount);
        }
    }

    private void AdjustHour(int amount)
    {
        if (amount > 0)
        {
            hour -= 1;
            
            if (hour < 0)
            {
                hour += 24;
            }
        }
        else if (amount < 0)
        {
            hour += 1;
            
            if (hour >= 24)
            {
                hour -= 24;
            }
            
        }
        UpdateClock();
        UpdateTimeLabel();
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
