using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroManger : MonoBehaviour
{
    private VisualElement root;
    private VisualElement gearBlack;
    private VisualElement coinBlack;
    private VisualElement codeBlack;
    private VisualElement wordBlack;
    private List<VisualElement> elements = new List<VisualElement>();
    private Button gear;
    private Button coin;
    private Button code;
    private Button word;
    private List<Button> buttons = new List<Button>();
    private Button confirmButton;
    private int Weapen;
    public IntVarible hero;
    public bool[] activeButtons = new bool[4]; 
    
    
    [Header("事件广播")]
    public IntEventSO charaEventSO;
    public ObjectEventSO LoadYseterday;
    public WeapenLibrary WeapenLibrary;
    public RemindLibrary RemindLibrary;
    public List<RemindData> RemindDatas = new List<RemindData>();
    public List<RemindData> impactRemindDatas = new List<RemindData>();

    private void Update()
    {
        if (Weapen == 0)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnClicked(coin,3);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnClicked(word,2);
            }
            
        }else if (Weapen == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnClicked(coin,3);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnClicked(word,2);
            }
        }else if (Weapen == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnClicked(gear,0);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnClicked(code,1);
            }
        }else if (Weapen == 3)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnClicked(gear,0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnClicked(code,1);
            }
        }

        if (Input.GetKeyDown(KeyCode.J)&& confirmButton.enabledSelf)
        {
            Confirm();
        }
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        gear = root.Q<Button>("gear");
        coin = root.Q<Button>("coin");
        code = root.Q<Button>("code");
        word = root.Q<Button>("word");
        confirmButton = root.Q<Button>("Confirm");
        buttons.Add(gear);
        buttons.Add(code);
        buttons.Add(word);
        buttons.Add(coin);
        gearBlack = root.Q<VisualElement>("gearBlack");
        coinBlack = root.Q<VisualElement>("coinBlack");
        codeBlack = root.Q<VisualElement>("codeBlack");
        wordBlack = root.Q<VisualElement>("wordBlack");
        elements.Add(gearBlack);
        elements.Add(codeBlack);
        elements.Add(wordBlack);
        elements.Add(coinBlack);
        InitializeButtonStates();
        gear.clicked += () => OnClicked(gear,0);
        coin.clicked += () => OnClicked(coin,3);
        code.clicked += () => OnClicked(code,1);
        word.clicked += () => OnClicked(word,2);
        confirmButton.clicked += () => Confirm();
        confirmButton.SetEnabled(false);

    }
    Dictionary<Button, (StyleLength width, StyleLength height, StyleLength left, StyleLength top)> originalButtonStates = new Dictionary<Button, (StyleLength width, StyleLength height, StyleLength left, StyleLength top)>();

// 初始化所有按钮的原始状态
    void InitializeButtonStates()
    {
        foreach (var button in buttons)
        {
            originalButtonStates[button] = (
                button.style.width,  // 原始宽度
                button.style.height, // 原始高度
                button.style.left,  // 原始 left 位置
                button.style.top   // 原始 top 位置
            );
            
        }
    }

    private void OnClicked(Button Button, int index)
    {
        Weapen = index;
        confirmButton.SetEnabled(true);
        if (!confirmButton.ClassListContains("turnbutton"))
        {
            confirmButton.ToggleInClassList("turnbutton");
        }


        for (int i = 0; i < buttons.Count; i++)
        {
            if (Button == buttons[i])
            {
                // 获取当前按钮的原始宽度和高度
                float originalWidth = buttons[i].resolvedStyle.width;
                float originalHeight = buttons[i].resolvedStyle.height;

                // 计算放大后的尺寸，放大25%
                float newWidth = originalWidth * 1.25f;
                float newHeight = originalHeight * 1.25f;

                // 计算放大偏移量
                float deltaWidth = (newWidth - originalWidth) / 2;
                float deltaHeight = (newHeight - originalHeight) / 2;

                // 设置放大后的尺寸
                buttons[i].style.width = newWidth;
                buttons[i].style.height = newHeight;
                elements[i].style.display = DisplayStyle.Flex;
                switch (i)
                {
                    case 0:
                        buttons[i].text = "机械系";
                        break; 
                    case 1:
                        buttons[i].text = "计算机系";
                        break; 
                    case 2:
                        buttons[i].text = "外国语系";
                        break; 
                    case 3:
                        buttons[i].text = "金融系";
                        break;
                    
                        
                }

                // 调整位置以确保中心放大效果
                buttons[i].style.translate = new Translate(-deltaWidth, -deltaHeight, 0);

                // 设置其他样式
                buttons[i].pickingMode = PickingMode.Ignore;
                if (buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
            }
            else
            {
                buttons[i].text = "";
                elements[i].style.display = DisplayStyle.None;
                // 其他按钮恢复原始大小
                buttons[i].style.width = originalButtonStates[buttons[i]].width;
                buttons[i].style.height = originalButtonStates[buttons[i]].height;
                buttons[i].style.left = originalButtonStates[buttons[i]].left;
                buttons[i].style.top = originalButtonStates[buttons[i]].top;
                buttons[i].style.translate = new Translate(0, 0, 0);

                // 设置其他样式
                buttons[i].pickingMode = PickingMode.Position;
                if (!buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
            }
        }
    }

    private void Confirm()
    {
        WeapenLibrary.weapenList.Clear();
        charaEventSO.RaiseEvent(Weapen ,this);
        hero.currentVaule = Weapen;
        for (int i = 0; i < RemindDatas.Count; i++)
        {
            if (i != Weapen)
            {
                RemindLibrary.remindPool.Add(RemindDatas[i]);
            }
        }
        for (int i = 0; i < impactRemindDatas.Count; i++)
        {
            RemindLibrary.remindPool.Add(impactRemindDatas[i]);
        }
        LoadYseterday.RaiseEvent(null,this);
        
        
    }
}
