using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RemindPannel : MonoBehaviour
{
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

    public int hour
    {
        get => hourVarible.currentVaule;
        set => hourVarible.SetValue(value);
    }

    [Header("广播事件")] public ObjectEventSO finishPick;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
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
        remindLibrary.remindPool.Remove(thisRemindData);
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
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
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
        audioSource.PlayOneShot(audioClip);
        if (choceLeft)
        {
            if (thisRemindData.leftEmoList.Count != 0)
            {
                for (int i = 0; i < thisRemindData.leftEmoList.Count; i++)
                {
                    var newEmo = new EmoDataEntry()
                    {
                        emoType = thisRemindData.leftEmoList[i].emoType,
                        amount = thisRemindData.leftEmoList[i].amount,
                    };
                    var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
                    if (target != null)
                    {
                        target.amount += newEmo.amount;
                    }
                }
            }
            if (thisRemindData.leftRemindEvent.Count != 0)
            {
                for (int i = 0; i < thisRemindData.leftRemindEvent.Count; i++)
                {
                    thisRemindData.leftRemindEvent[i].RaiseEvent(thisRemindData.relateLeftInt,this);
                }
            }
        }
        else
        {
            if (thisRemindData.rightEmoList.Count != 0)
            {
                for (int i = 0; i < thisRemindData.rightEmoList.Count; i++)
                {
                    var newEmo = new EmoDataEntry()
                    {
                        emoType = thisRemindData.rightEmoList[i].emoType,
                        amount = thisRemindData.rightEmoList[i].amount,
                    };
                    var target = playerEmo.emoDataList.Find(t => t.emoType == newEmo.emoType);
                    if (target != null)
                    {
                        target.amount += newEmo.amount;
                    }
                }
            }
            if (thisRemindData.rightRemindEvent.Count != 0)
            {
                for (int i = 0; i < thisRemindData.rightRemindEvent.Count; i++)
                {
                        thisRemindData.rightRemindEvent[i].RaiseEvent(thisRemindData.relateRightInt,this);
                }
            }
        }
        finishPick.RaiseEvent(null, this);
    }
}
