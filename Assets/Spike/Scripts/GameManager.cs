using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] bulletType = new bool[8, 5];
    public int[] emotionalQuantity = new int[8];

    public void Awake()
    {
        for (int i = 0; i < emotionalQuantity.Length; i++) 
        {
            emotionalQuantity[i] = 1;
        }

        bulletType[1, 0] = true;
        //bulletType[i, 2] = true;
        //bulletType[i, 4] = true;

    }
}
