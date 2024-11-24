using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YesterdadPannel : MonoBehaviour
{
    public  EmoLibrary playerEmo;
    public YesterdayEvent yesterdayEvent;
    public int index;
   private YesterdayDataEntry thisYesterdayData;
   public bool choceLeft;
   public ObjectEventSO GameStartSO;
  
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
        leftE.text = ShowEmo(thisYesterdayData.leftEmoType);
        rightE.text = ShowEmo(thisYesterdayData.rightEmoType);
        confirmButton.SetEnabled(false);
        if (confirmButton.ClassListContains("turnbutton"))
        {
            confirmButton.ToggleInClassList("turnbutton");
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].style.borderBottomWidth = 0f;
            buttons[i].style.borderLeftWidth = 0f;
            buttons[i].style.borderRightWidth = 0f;
            buttons[i].style.borderTopWidth = 0f;
            buttons[i].pickingMode = PickingMode.Position;
            if (!buttons[i].ClassListContains("turnbutton"))
            {
                buttons[i].ToggleInClassList("turnbutton");
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
        }

        return null;
    }
   

    private void Confirm()
    {
        if (choceLeft)
        {
            var newEmo = new EmoDataEntry()
            {
                emoType = thisYesterdayData.leftEmoType,
                amount = thisYesterdayData.leftAmount,
            };
            var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
            if (target != null)
            {
                target.amount+= newEmo.amount;
            }
            else
            {
                playerEmo.emoDataList.Add(newEmo);
            }
        }
        else
        {
            var newEmo = new EmoDataEntry()
            {
                emoType = thisYesterdayData.rightEmoType,
                amount = thisYesterdayData.rightAmount,
            };
            var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
            if (target != null)
            {
                target.amount+= newEmo.amount;
            }
            else
            {
                playerEmo.emoDataList.Add(newEmo);
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
        confirmButton.SetEnabled(true);
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
               
                buttons[i].style.borderBottomWidth=20f;
                buttons[i].style.borderLeftWidth=20f;
                buttons[i].style.borderRightWidth=20f;
                buttons[i].style.borderTopWidth=20f; 
                buttons[i].pickingMode = PickingMode.Ignore; 
                
                if (buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
                
            }
            else
            {
               
                buttons[i].style.borderBottomWidth=0f;
                buttons[i].style.borderLeftWidth=0f;
                buttons[i].style.borderRightWidth=0f;
                buttons[i].style.borderTopWidth=0f;
                buttons[i].pickingMode = PickingMode.Position;
                if (!buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
            }
        }
    }
}
