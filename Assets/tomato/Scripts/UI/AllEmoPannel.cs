using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AllEmoPannel : MonoBehaviour
{
    private VisualElement root;
    private Label[] labels = new Label[8];
    private Button backButton;
    public EmoLibrary playerEmoLibrary;
    public EmoLibrary EmoDLibrary;
    
    public GameObject remindPannel;
    public GameObject SettingPannel;
    private Label final;
    public IntVarible hour;
    public IntVarible happy;
    public IntVarible lazy;
    
    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        final = root.Q<Label>("final");
        for (int i = 0; i < 8; i++)
        {
            labels[i] = root.Q<Label>((i).ToString());
            labels[i].text = ShowEmo(playerEmoLibrary.emoDataList[i].emoType,playerEmoLibrary.emoDataList[i].amount) ;
            if (SceneManager.GetActiveScene().name == "Fight")
            {
                labels[i].text+= " 本层击杀数:"+ EmoDLibrary.emoDataList[i].amount;
            }
            
        }
        backButton = root.Q<Button>("Back");
        backButton.clicked += () => Back();
        Time.timeScale = 0f;
        if (hour.currentVaule >= 7 && hour.currentVaule <= 12)
        {
            final.style.display = DisplayStyle.Flex;
            if (happy.currentVaule >= 2)
            {
                final.text = "接下来,是狂喜之时,喜悦到达了定点";
            }else if (lazy.currentVaule >= 2)
            {
                final.text = "心好累,恐惧,厌恶,羞耻,愤怒到达了定点";
            }
            else
            {
                final.text = "你所累积的情绪促成了情绪综合体的形成，所有情绪绝对值-6";
            }
            
        }
        else
        {
            final.style.display = DisplayStyle.None;
        }
    }

    private void OnDisable()
    {
        if (!remindPannel.activeSelf && !SettingPannel.activeSelf)
        {
            Time.timeScale = 1f;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Back();
        }
    }


    private void Back()
    {
        this.gameObject.SetActive(false);
    }

    private string ShowEmo(EmoType emoType, int amount)
    {
        string emo = "";
        string amountString = amount.ToString();
       
        switch (emoType)
        {
            case(EmoType.Happness):
                emo = "喜悦:"+amountString;
                return emo;
            case(EmoType.Sadness):
                emo = "悲伤:"+amountString;
                return emo;
            case(EmoType.Calmness):
                emo = "平静:"+amountString;
                return emo;
            case(EmoType.Fear):
                emo = "恐惧:"+amountString;
                return emo;
            case(EmoType.Hate):
                emo = "厌恶:"+amountString;
                return emo;
            case(EmoType.Shame):
                emo = "羞耻:"+amountString;
                return emo;
            case(EmoType.Anger):
                emo = "愤怒:"+amountString;
                return emo;
            case(EmoType.Astonishment):
                emo = "惊讶:"+amountString;
                return emo;
            case (EmoType.AddHour):
            {
                emo = "睡眠时间"+amountString;
                return emo;
            }
            
        }

        return null;
    }
    
}
