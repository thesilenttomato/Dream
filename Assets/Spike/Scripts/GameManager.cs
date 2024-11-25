using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] bulletType = new bool[8, 5];

    public void Awake()
    {
        /*for (int i = 0; i < 8; i++)
        {
            bulletType[i, 0] = true;
            bulletType[i, 2] = true;
            bulletType[i, 4] = true;
        }*/

    }
}
