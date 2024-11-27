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
    public List<WeapenData> WeapenDatas = new List<WeapenData>();

    public ObjectEventSO LoadYseterday;
    public WeapenLibrary WeapenLibrary;

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
        gear.clicked += () => OnClicked(gear,1);
        coin.clicked += () => OnClicked(coin,4);
        code.clicked += () => OnClicked(code,2);
        word.clicked += () => OnClicked(word,3);
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
            }
        }
    }

    private void Confirm()
    {
        WeapenLibrary.weapenList.Clear();
        WeapenLibrary.weapenList.Add(WeapenDatas[Weapen - 1]);
        WeapenLibrary.weapenList[0].state = 0;
        LoadYseterday.RaiseEvent(null,this);
    }
}
