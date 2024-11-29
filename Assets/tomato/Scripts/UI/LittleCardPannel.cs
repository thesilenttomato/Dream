using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LittleCardPannel : MonoBehaviour
{
    private VisualElement root;
    private Label title;
    private Label content;
    private Button next;
    private Button back;
    private int index;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        title = root.Q<Label>("title");
        content = root.Q<Label>("content");
        next = root.Q<Button>("Next");
        back = root.Q<Button>("Back");
        next.clicked += () => Next();
        back.clicked += () => Back();
        Show(0);
    }

    private void Next()
    {
        
    }

    private void Back()
    {
        
    }


    private void Show(int i)
    {
        
    }
}
