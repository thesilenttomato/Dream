using UnityEngine;

public class EndManger : MonoBehaviour
{
    public EndGame endGame1;
    public EndGame endGame2_1;
    public EndGame endGame2_2;
    public EndGame endGame2_3;
    public EndGame endGame2_4;
    public EndGame endGame3;
    public EndGame endGame4;
    public EndGame endGame5;
    public EndGame endGame6;
    public EndGame endGame7;
    public EndGame endGame;
    public EmoLibrary playerEmo;

    public IntVarible lazy;
    public IntVarible happy;
    
    public UIManger uiManger;
    public void choceEnd(int i )
    {
        switch ( i)
        {
            case 0:
                if (playerEmo.emoDataList[0].amount + playerEmo.emoDataList[2].amount + playerEmo.emoDataList[4].amount >=
                    playerEmo.emoDataList[1].amount + playerEmo.emoDataList[3].amount + playerEmo.emoDataList[5].amount + playerEmo.emoDataList[6].amount + playerEmo.emoDataList[7].amount)
                {
                    endGame = endGame2_2;
                }
                else
                {
                    endGame = endGame2_4;
                }
                break;
            case 1:
                endGame = endGame1;
                break;
            case 2:
                if (playerEmo.emoDataList[0].amount + playerEmo.emoDataList[2].amount + playerEmo.emoDataList[4].amount >=
                    playerEmo.emoDataList[1].amount + playerEmo.emoDataList[3].amount + playerEmo.emoDataList[5].amount + playerEmo.emoDataList[6].amount + playerEmo.emoDataList[7].amount)
                {
                    endGame = endGame2_1;
                }
                else
                {
                    endGame = endGame2_3;
                }
                break;
            case 3:
                endGame = endGame3;
                break;
            case 4:
                endGame = endGame4;
                break;
            case 5:
                endGame = endGame5;
                break;
            case 6:
                endGame = endGame6;
                break;
            case 7:
                endGame = endGame7;
                break;
        }
        uiManger.OpenEndPannel();
    }

    public void AddLazy()
    {
        lazy.currentVaule += 1;
    }public void AddHappy()
    {
        happy.currentVaule += 1;
    }
}
