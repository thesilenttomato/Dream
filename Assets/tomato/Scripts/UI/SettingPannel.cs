using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingPannel : MonoBehaviour
{
    public ObjectEventSO LoadMneu;
    private VisualElement root;
    private Button backButton;
    private Button quitButton;
    private Button finishButton;
    private Slider volumeSlider;
    public SoundManger soundManager;
    private Button[] soundButtons = new Button[10]; 
    public IntVarible soundVarible; 
    public GameObject settingPanel;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        root = this.GetComponent<UIDocument>().rootVisualElement;
        finishButton = root.Q<Button>("BackGame");
        backButton = root.Q<Button>("BackMenu");
        quitButton = root.Q<Button>("QuitGame");
        finishButton.clicked += () => FinshSettings();
        backButton.clicked += () => BackToMenu();
        quitButton.clicked += () => QuitGame();
        
        for (int i = 0; i < soundButtons.Length; i++)
        {
            soundButtons[i] = root.Q<Button>((i + 1).ToString());
            int buttonIndex = i; // 捕获当前索引
            soundButtons[i].clicked += () => OnSoundButtonClick(buttonIndex + 1);
        }
    }

    private void OnDisable()
    {
        if (!settingPanel.activeSelf)
        {
            Time.timeScale = 1f; 
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FinshSettings();
        }
    }

    private void BackToMenu()
    {
        gameObject.SetActive(false);
        LoadMneu.RaiseEvent(null,this);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void FinshSettings()
    {
        this.gameObject.SetActive(false);
    }
    private void OnSoundButtonClick(int buttonNumber)
    {
        soundVarible.currentVaule = buttonNumber;
        // 更新选中值（0.1 ~ 1.0，对应按钮编号）
       float  selectedValue = buttonNumber * 0.1f;

        // 更新按钮颜色
        for (int i = 0; i < soundButtons.Length; i++)
        {
            if (i < buttonNumber - 1)
            {
                soundButtons[i].style.backgroundColor = new Color(43f / 255f, 100f / 255f, 255f / 255f, 1.0f);; 
            }
            else if (i > buttonNumber - 1)
            {
                soundButtons[i].style.backgroundColor =  new Color(00f,01/255f,255f / 255f,1f); // 大于选中编号的按钮变黑
            }
            else
            {
                soundButtons[i].style.backgroundColor =
                    new Color(73f / 255f, 74f / 255f, 235f / 255f, 1.0f); // 当前选中的按钮灰色
            }
        }

        soundManager.ChangeVolume(selectedValue);
    }
}
