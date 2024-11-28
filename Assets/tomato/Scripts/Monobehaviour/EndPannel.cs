using System;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class EndPannel : MonoBehaviour
{
    private VisualElement root;
    private Button end;
    private VisualElement spirt;
    private Label title;
    private Label content;
    private EndGame endGame;
    public ObjectEventSO LoadMenu;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        end = root.Q<Button>("again");
        spirt = root.Q<VisualElement>("LifeContainer");
        content = spirt.Q<Label>("Content");
        title = spirt.Q<Label>("title");
        end.clicked += () => endTheGame();
        show();
    }

    private void show()
    {
        spirt.style.backgroundImage = new StyleBackground(endGame.sprite);
        content.text = endGame.description;
        title.text = endGame.title;
    }

    private void endTheGame()
    {
        LoadMenu.RaiseEvent(null,this);
    }
}
