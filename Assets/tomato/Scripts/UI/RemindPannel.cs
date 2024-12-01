using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RemindPannel : MonoBehaviour
{
    
    public UIDocument UIDocument;
    private VisualElement emoContainer;
    public EmoLibrary playerEmo;
    public RemindLibrary remindLibrary;
    private RemindData thisRemindData;

    public bool choceLeft;
    private List<Button> Buttons = new List<Button>();
    private VisualElement Root;
    private Button leftButton;
    private Button rightButton;
    private Button confirmButton;
    private VisualElement root;
    private VisualElement lifeContainer;
    private Label title;
    private Label leftT;
    private Label leftE;
    private Label rightT;
    private Label rightE;
    public IntVarible hourVarible;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public VisualTreeAsset EmoTemple;
    public List<EmoType> changedEmolist = new List<EmoType>();
    public EmoLibrary emoDLibrary;
    
    public int hour
    {
        get => hourVarible.currentVaule;
        set => hourVarible.SetValue(value);
    }

    [Header("广播事件")] public ObjectEventSO finishPick;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        emoContainer = UIDocument.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("EmoContainer");
        lifeContainer = root.Q<VisualElement>("LifeContainer");
        leftButton = root.Q<Button>("Left");
        rightButton = root.Q<Button>("Right");
        confirmButton = root.Q<Button>("Confirm");
        title = root.Q<Label>("title");
        leftT = root.Q<Label>("LeftContent");
        leftE = root.Q<Label>("LeftEff");
        rightT = root.Q<Label>("RightContent");
        rightE = root.Q<Label>("RightEff");
       
        Buttons.Clear();
        Buttons.Add(leftButton);
        Buttons.Add(rightButton);
        confirmButton.clicked += () => Confirm();
        leftButton.clicked += () => OnClicked(leftButton, true);
        rightButton.clicked += () => OnClicked(rightButton, false);
        Time.timeScale = 0;
        Show();
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

        
    }


    private void Show()
    {

        thisRemindData = remindLibrary.remindPool[Random.Range(0, remindLibrary.remindPool.Count)];
        
        lifeContainer.style.backgroundImage = new StyleBackground(thisRemindData.sprite);
        title.text = thisRemindData.title;
        leftT.text = thisRemindData.leftContent;
        rightT.text = thisRemindData.rightContent;
        leftE.text = thisRemindData.leftPlayerContent;
        rightE.text = thisRemindData.rightPlayerContent;
        confirmButton.SetEnabled(false);
        if (confirmButton.ClassListContains("turnbutton"))
        {
            confirmButton.ToggleInClassList("turnbutton");
        }

        for (int i = 0; i < Buttons.Count; i++)
        {
            var labels = Buttons[i].Query<Label>().ToList();
            foreach (var label in labels)
            {
                label.style.fontSize = 60f;
            }

            Buttons[i].pickingMode = PickingMode.Position;
            if (!Buttons[i].ClassListContains("turnbutton"))
            {
                Buttons[i].ToggleInClassList("turnbutton");
            }
        }

        EmoD();
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void EmoD()
    {
        for (int i  = 0; i  < emoDLibrary.emoDataList.Count; i ++)
        {
            if (playerEmo.emoDataList[i].amount != 0)
            {
                switch (i)
            {
                case 0:
                    if (emoDLibrary.emoDataList[i].amount <= 2 && playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 6&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 1:
                    if (emoDLibrary.emoDataList[i].amount <= 2 && playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 4&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 2:
                    if (emoDLibrary.emoDataList[i].amount <= 2 && playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 6&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 3:
                    if (emoDLibrary.emoDataList[i].amount <= 1&& playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 4&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 4:
                    if (emoDLibrary.emoDataList[i].amount <= 2&& playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 6&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 5:
                    if (emoDLibrary.emoDataList[i].amount <= 6&& playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 12&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 6:
                    if (emoDLibrary.emoDataList[i].amount > 1&& playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount < 1&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;
                case 7:
                    if (emoDLibrary.emoDataList[i].amount <= 2&& playerEmo.emoDataList[i].amount !=1)
                    {
                        playerEmo.emoDataList[i].amount--;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, -1);
                    }
                    else if (emoDLibrary.emoDataList[i].amount >= 2&& playerEmo.emoDataList[i].amount !=-1)
                    {
                        playerEmo.emoDataList[i].amount++;
                        ShowEmoChange(playerEmo.emoDataList[i].emoType, 1);
                    }
                    break;    
            }
            }
            emoDLibrary.emoDataList[i].amount = 0;
        }

        
    }

    private void OnClicked(Button Button, bool left)
    {
        confirmButton.SetEnabled(true);
        audioSource.PlayOneShot(audioClip);
        if (left)
        {
            choceLeft = true;
        }
        else
        {
            choceLeft = false;
        }

        for (int i = 0; i < Buttons.Count; i++)
        {

            if (Button == Buttons[i])
            {

                var labels = Buttons[i].Query<Label>().ToList();
                foreach (var label in labels)
                {

                    label.style.fontSize = 80f;
                }

                Buttons[i].pickingMode = PickingMode.Ignore;

                if (Buttons[i].ClassListContains("turnbutton"))
                {
                    Buttons[i].ToggleInClassList("turnbutton");
                }

            }
            else
            {
                var labels = Buttons[i].Query<Label>().ToList();
                foreach (var label in labels)
                {

                    label.style.fontSize = 60f;
                }

                Buttons[i].pickingMode = PickingMode.Position;
                if (!Buttons[i].ClassListContains("turnbutton"))
                {
                    Buttons[i].ToggleInClassList("turnbutton");
                }
            }
        }
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
       
   }

   

   public int GetAmountByEmoType(EmoType emoType)
   
   
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
                emo = "睡眠时间"+amountString;
                return emo;
            }
            
        }

        return null;
    }
    private void Confirm() 
    {
        audioSource.PlayOneShot(audioClip);

        // Decide which set of data to process based on the player's choice
        var selectedEmoList = choceLeft ? thisRemindData.leftEmoList : thisRemindData.rightEmoList;
        var selectedRemindEvent = choceLeft ? thisRemindData.leftRemindEvent : thisRemindData.rightRemindEvent;
        var relateInt = choceLeft ? thisRemindData.relateLeftInt : thisRemindData.relateRightInt;

        // Process EmoList (Emotions)
        ProcessEmoList(selectedEmoList);

        // Raise relevant events
        RaiseRemindEvents(selectedRemindEvent, relateInt);
        remindLibrary.remindPool.Remove(thisRemindData);

        finishPick.RaiseEvent(null, this);
        
    }

    private void ProcessEmoList(List<EmoDataEntry> emoList)
    {
        if (emoList.Count == 0) return;

        emoContainer.Clear();
        foreach (var effect in emoList)
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
        }

        
    }

    private void RaiseRemindEvents(List<IntEventSO> remindEvents, int relateInt)
    {
        Debug.Log("relateInt"+relateInt);
        if (remindEvents.Count > 0)
        {
            for (int i = 0; i < remindEvents.Count; i++)
            {
                remindEvents[i].RaiseEvent(relateInt, this);
            }
        }
        else
        {
            Debug.LogWarning("No remind events to raise.");
        }
    }

}
