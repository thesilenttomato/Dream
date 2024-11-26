using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] bulletType = new bool[8, 5];
    public int[] emotionalQuantity = new int[8];
    public int[] defeatedEmotion = new int[8];
    public int playerLife = 0;
    public int playeyLifeMax = 3;

    public void Awake()
    {
        for (int i = 0; i < emotionalQuantity.Length; i++) 
        {
            emotionalQuantity[i] = 1;
        }

        bulletType[7, 0] = true;
        bulletType[7, 2] = true;
        bulletType[7, 4] = true;

    }
    public void Start()
    {
        playerLife = playeyLifeMax;
    }
}
