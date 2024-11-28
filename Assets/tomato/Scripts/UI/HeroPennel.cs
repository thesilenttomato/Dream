using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroManger : MonoBehaviour
{
    private VisualElement root;
    private Button gear;
    private Button coin;
    private Button code;
    private Button word;
    private List<Button> buttons = new List<Button>();
    private Button confirmButton;
    private int Weapen;
    
    
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
        buttons.Add(coin);
        buttons.Add(code);
        buttons.Add(word);
        gear.clicked += () => OnClicked(gear,0);
        coin.clicked += () => OnClicked(coin,3);
        code.clicked += () => OnClicked(code,1);
        word.clicked += () => OnClicked(word,2);
        confirmButton.clicked += () => Confirm();
        confirmButton.SetEnabled(false);

    }
    private void OnClicked(Button Button,int index)
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
                buttons[i].style.width = 400;
                buttons[i].style.height = 400;
                buttons[i].pickingMode = PickingMode.Ignore; 
                
                if (buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
                ScaleChildElements(buttons[i], 1.3f);
            }
            else
            {
                buttons[i].style.width = 300;
                buttons[i].style.height = 300;
               
                buttons[i].pickingMode = PickingMode.Position;
                if (!buttons[i].ClassListContains("turnbutton"))
                {
                    buttons[i].ToggleInClassList("turnbutton");
                }
                ScaleChildElements(buttons[i], 1f);
            }
        }
    }
    private void ScaleChildElements(VisualElement parent, float size)
    {
        foreach (var child in parent.Children())
        {
            child.style.scale=(new Vector2(size,size));

            // 如果子物体有子物体，递归放大
            ScaleChildElements(child, size);
        }
    }

    private void Confirm()
    {
        WeapenLibrary.weapenList.Clear();
        charaEventSO.RaiseEvent(Weapen ,this);
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
