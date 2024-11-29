using UnityEngine;

public class EndManger : MonoBehaviour
{
    public EndGame endGame1;
    public EndGame endGame2_1;
    public EndGame endGame2_2;
    public EndGame endGame2_3;
    public EndGame endGame2_4;
    public EndGame endGame3;
    public EndGame endGame;
    public EmoLibrary playerEmo;
    
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
            
        }
        uiManger.OpenEndPannel();
    }
}
