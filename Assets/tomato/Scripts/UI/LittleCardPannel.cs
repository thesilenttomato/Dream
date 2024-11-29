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
    [TextArea]
    public List<string> contents = new List<string>();
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
        Show(index);
    }

    private void Next()
    {
        index += 1;
        if (index >= contents.Count)
        {
            index = 0;
        }
        Show(index);
        
    }

    private void Back()
    {
        this.gameObject.SetActive(false);
    }


    private void Show(int i)
    {
        content.text=contents[i];
        title.text = "提示"+i.ToString();
        
    }
}
