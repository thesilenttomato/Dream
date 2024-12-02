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

    public int totalKind = 0;

    public int playeyLifeMax { get => hpVarible.maxVaule; }
    public int playerLife { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

    public IntVarible hourVarible;
    public int hour { get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }

    //public YesterdayLibrary yesterdayEvent;
    public EmoLibrary playerEmoLibrary;
    public WeapenLibrary weapenLibrary;
    public EmoLibrary defeatedEmotionLibrary;

    public bool[] playerType = new bool[4];

    public IntVarible hero;
    public IntVarible lazyEnd;
    public IntVarible happyEnd;


    public BoolEventSO ifbossDefeaded;
    public bool ifbossDefeadedCheck;

    public GameObject boundary_1;
    public GameObject boundary_2;

    public void Awake()
    {
        //bossFight[1] = true;
        if (hour == 7 && lazyEnd.currentVaule > 1)
        {
            bossFight[1] = true;
        }
        else if (hour == 7 && happyEnd.currentVaule > 1)
        {
            bossFight[2] = true;
        }
        else if (hour == 7)
        {
            bossFight[0] = true;
        }
        if (bossFight[0])
        {
            for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
            {
                if (Mathf.Abs(playerEmoLibrary.emoDataList[i].amount) <= 6)
                {

                    playerEmoLibrary.emoDataList[i].amount = 0;

                }
                else
                {
                    if (playerEmoLibrary.emoDataList[i].amount > 0)
                    {

                        playerEmoLibrary.emoDataList[i].amount -= 6;
                    }
                    else
                    {

                        playerEmoLibrary.emoDataList[i].amount += 6;
                    }
                }
            }
        }
        if (bossFight[1])
        {
            for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
            {
                if (i == 3 || i == 5 || i == 6 || i == 7)
                {
                    playerEmoLibrary.emoDataList[i].amount = 10;
                }
                else
                {
                    playerEmoLibrary.emoDataList[i].amount = 0;
                }
            }
        }
        if (bossFight[2])
        {
            for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
            {
                if (i == 0)
                {
                    playerEmoLibrary.emoDataList[i].amount = 100;
                }
                else
                {
                    playerEmoLibrary.emoDataList[i].amount = 0;
                }
            }
        }

        for (int i = 0; i < playerEmoLibrary.emoDataList.Count; i++)
        {
            EmoDataEntry emoDataEntry = playerEmoLibrary.emoDataList[i];
            if (emoDataEntry.emoType == EmoType.Happness)
            {
                emotionalQuantity[0] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Sadness)
            {
                emotionalQuantity[1] = emoDataEntry.amount;
            }
            if (emoDataEntry.emoType == EmoType.Calmness)
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
        playerType[hero.currentVaule] = true;

        float screenRatio = (float)Screen.width / Screen.height;
        boundary_1.transform.position = new Vector3(boundary_1.transform.position.x * screenRatio / (16f / 9f), boundary_1.transform.position.y);
        boundary_2.transform.position = new Vector3(boundary_2.transform.position.x * screenRatio / (16f / 9f), boundary_2.transform.position.y);
        //Debug.Log(screenRatio);


        //ifBossDefeated.
        //bossFight[0] = true;
        //bossFight[2] = true;
        /*emotionalQuantity[0] = 100;
        emotionalQuantity[1] = 20;
        emotionalQuantity[2] = 20;
        emotionalQuantity[3] = 20;
        emotionalQuantity[4] = 20;
        emotionalQuantity[5] = 20;
        emotionalQuantity[6] = 20;
        emotionalQuantity[7] = 20;*/
        /*bulletType[3, 0] = true;
        bulletType[3, 2] = true;
        bulletType[3, 4] = true;
        bulletType[7, 0] = true;
        bulletType[7, 2] = true;
        bulletType[7, 4] = true;
        playerType[2] = true;*/

        for (int i = 0; i < emotionalQuantity.Length; i++)
        {
            if (emotionalQuantity[i] != 0)
            {
                totalKind += 1;
            }
        }
    }
    public void Start()
    {

    }
    public void Update()
    {
        if (hour == 7 && ifbossDefeadedCheck)
        {
            ifbossDefeaded.RaiseEvent(true, this);
            ifbossDefeadedCheck = false;
        }
        for (int i = 0; i < 8; i++)
        {
            if (defeatedEmotion[i] > defeatedEmotionLibrary.emoDataList[i].amount)
            {
                defeatedEmotionLibrary.emoDataList[i].amount = defeatedEmotion[i];
            }

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
