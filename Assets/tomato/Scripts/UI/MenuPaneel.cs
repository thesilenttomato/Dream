using UnityEngine;
using UnityEngine.UIElements;

public class MenuPannel : MonoBehaviour
{
    public VisualElement root;
    private Button gameStartButton;
    private Button gameQuitButton;
    public ObjectEventSO NewGameEvent;

    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        gameStartButton = root.Q<Button>("GameStart");
        gameQuitButton = root.Q<Button>("GameQuit");
        gameStartButton.clicked += () => OnGameStartButtonClicked();
        gameQuitButton.clicked += () => OnGameQuitButtonClicked();
    }

    private void OnGameStartButtonClicked()
    {
        Debug.Log("111");
        NewGameEvent.RaiseEvent(null,this);
    }

    private void OnGameQuitButtonClicked()
    {
        Application.Quit();
    }
}
