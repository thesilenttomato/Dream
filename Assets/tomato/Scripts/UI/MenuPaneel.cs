using UnityEngine;
using UnityEngine.UIElements;

public class MenuPannel : MonoBehaviour
{
    public VisualElement root;
    private Button gameStartButton;
    private Button gameQuitButton;
    public ObjectEventSO NewGameEvent;

    public IntVarible hour;
    public IntVarible minute;
    public IntVarible Hp;

    public EmoLibrary playerEmoLibrary;
    public RemindLibrary remindLibrary;

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
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            emoDataEntry.amount = 0;
        }
        
        
        remindLibrary.remindPool.Clear();
        hour.currentVaule = 0;
        minute.currentVaule = 0;
        Hp.maxVaule = 3;
        NewGameEvent.RaiseEvent(null,this);
    }

    private void OnGameQuitButtonClicked()
    {
        Application.Quit();
    }
}
