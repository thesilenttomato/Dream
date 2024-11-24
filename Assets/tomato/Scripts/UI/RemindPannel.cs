
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class RemindPannel : MonoBehaviour
{
    private List<Button > Buttons = new List<Button>();
    private VisualElement Root;
    private Button leftButton;
    private Button rightButton;
    private Button confirmButton;
    [Header("广播事件")]
    public ObjectEventSO finishPick;
    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        leftButton = Root.Query<Button>("Left");
        rightButton = Root.Query<Button>("Right");
        confirmButton = Root.Query<Button>("Confirm");
        Buttons.Clear();
        Buttons.Add(leftButton);
        Buttons.Add(rightButton);
        leftButton.clicked += () => OnClicked(leftButton, true);
        rightButton.clicked += () => OnClicked(rightButton, false); 
//        confirmButton.clicked += OnConfirmButtonClicked;
        
        Time.timeScale = 0;
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
    private void OnConfirmButtonClicked()
    {
        // rootElement.style.display = DisplayStyle.None;
        finishPick.RaiseEvent(null,this);
    }
    
}
