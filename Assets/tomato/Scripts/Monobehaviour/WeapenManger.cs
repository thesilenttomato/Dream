using UnityEngine;

public class WeapenManger : MonoBehaviour
{
   public WeapenLibrary Playerlibrary;
   public WeapenLibrary Alllibrary;

   public ObjectEventSO test;
[ContextMenu("Test")]
   public void Eventtest()
   {
      test.RaiseEvent(null,this);
   }
   public void GetWeapen(int i)
   {
      Playerlibrary.weapenList.Add(Alllibrary.weapenList[i]);
   }

   public void EvoA(int i)
   {
      for (int j = 0; j < Playerlibrary.weapenList.Count; j++)
      {
         if (Playerlibrary.weapenList[j].id - 1 == i)
         {
            if (Playerlibrary.weapenList[j].state == 0)
            {
               Playerlibrary.weapenList[j].state = 1;
            }
            else if (Playerlibrary.weapenList[j].state == 1)
            {
               Playerlibrary.weapenList[j].state = 2;
            }
            
         }
      }
   }
   public void EvoB(int i)
   {
      for (int j = 0; j < Playerlibrary.weapenList.Count; j++)
      {
         if (Playerlibrary.weapenList[j].id - 1 == i)
         {
            if (Playerlibrary.weapenList[j].state == 0)
            {
               Playerlibrary.weapenList[j].state = 3;
            }
            else if ( Playerlibrary.weapenList[j].state == 3)
            {
               Playerlibrary.weapenList[j].state = 4;
            }
            
         }
      }
   }
}
