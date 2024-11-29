using NUnit.Framework.Constraints;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosive;
    public bool[,] bulletType = new bool[8, 5];
    public int[] emotionalQuantity = new int[8];
    public int[] defeatedEmotion = new int[8];
    //public int playerLife = 0;
    //public int playeyLifeMax = 3;
    public bool[] bossFight = new bool[3];

    public IntVarible hpVarible;

    public int playeyLifeMax { get => hpVarible.maxVaule; }
    public int playerLife { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

    public IntVarible hourVarible;
    public int hour { get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }

    //public YesterdayLibrary yesterdayEvent;
    public EmoLibrary playerEmoLibrary;
    public WeapenLibrary weapenLibrary;

    public bool[] playerType = new bool[4];

    public IntVarible hero;
    public IntVarible lazyEnd;
    public IntVarible happyEnd;


    public BoolEventSO ifbossDefeaded;
    public bool ifbossDefeadedCheck;

    public void Awake()
    {
        /*if (hour == 7 && lazyEnd.currentVaule > 3)
        {
            bossFight[1] = true;
        }
        else if (hour == 7 && happyEnd.currentVaule > 3)
        {
            bossFight[2] = true;
        }
        else if (hour == 7)
        {
            bossFight[0] = true;
        }
        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            if (emoDataEntry.emoType == EmoType.Happness)
            {
                emotionalQuantity[0] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Calmness)
            {
                emotionalQuantity[1] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Sadness)
            {
                emotionalQuantity[2] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Fear)
            {
                emotionalQuantity[3] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Astonishment)
            {
                emotionalQuantity[4] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Shame)
            {
                emotionalQuantity[5] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Anger)
            {
                emotionalQuantity[6] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Hate)
            {
                emotionalQuantity[7] = emoDataEntry.amount;
            }
        }
        for (int i = 0; i < weapenLibrary.weapenList.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (weapenLibrary.weapenList[i].id == j + 1 && weapenLibrary.weapenList[i].state >= 0)
                {
                    bulletType[j, 0] = true;
                    bulletType[j, weapenLibrary.weapenList[i].state] = true;
                }
            }
        }
        playerType[hero.currentVaule] = true;*/

        //ifBossDefeated.
        //bossFight[0] = true;
        emotionalQuantity[0] = 20;
        bulletType[3, 0] = true;
        bulletType[3, 2] = true;
        bulletType[3, 4] = true;
        playerType[2] = true;
    }
    public void Start()
    {

    }
    public void Update()
    {
        if (hour == 7 && ifbossDefeadedCheck)
        {
            ifbossDefeaded.RaiseEvent(true, this);
        }
    }

    public void Explosive(Vector3 position, Color color)
    {
        explosive.transform.position = position;
        ParticleSystem particleSystem = explosive.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = color;
        explosive.Play();
    }
}
