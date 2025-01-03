using UnityEngine;
using UnityEngine.UIElements;

public class MenuPannel : MonoBehaviour
{
    public WeapenLibrary Alllibrary;
    public VisualElement root;
    private Button gameStartButton;
    private Button gameQuitButton;
    public ObjectEventSO NewGameEvent;
    public GameObject GuidePannel;
    public GameObject IntroPannel;

    public IntVarible hour;
    public IntVarible minute;
    public IntVarible Hp;
    public IntVarible failTime;
    public IntVarible happy;
    public IntVarible lazy;
    public IntVarible audio;

    public EmoLibrary playerEmoLibrary;
    public RemindLibrary remindLibrary;
    
    public AudioClip buttonSound;
    public AudioSource audioSource;
    private Button Guidebutton;
    private Button Introbutton;
    public EmoLibrary emoDLibrary;
    public IntVarible TheEndgame;

    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        gameStartButton = root.Q<Button>("GameStart");
        gameQuitButton = root.Q<Button>("GameQuit");
        Guidebutton = root.Q<Button>("Guide");
        Introbutton = root.Q<Button>("Intro");
        gameStartButton.clicked += () => OnGameStartButtonClicked();
        gameQuitButton.clicked += () => OnGameQuitButtonClicked();
        Guidebutton.clicked += () => Guide();
        Introbutton.clicked += () => Intro();
    }

    private void Guide()
    {
        GuidePannel.SetActive(true);
    }

    private void Intro()
    {
        IntroPannel.SetActive(true);
    }

    private void OnGameStartButtonClicked()
    {
        audioSource.PlayOneShot(buttonSound);
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            emoDataEntry.amount = 0;
            emoDLibrary.emoDataList[i].amount = 0;
        }

        for (int i = 0; i < Alllibrary.weapenList.Count; i++)
        {
            Alllibrary.weapenList[i].state = 0;
        }
        remindLibrary.remindPool.Clear();
        hour.currentVaule = 0;
        minute.currentVaule = 0;
        Hp.maxVaule = 5;
        failTime.currentVaule = 0;
        happy.currentVaule = 0;
        lazy.currentVaule = 0;
        audio.currentVaule = 10;
        TheEndgame.currentVaule = 0;
        NewGameEvent.RaiseEvent(null,this);
    }

    private void OnGameQuitButtonClicked()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }
}
