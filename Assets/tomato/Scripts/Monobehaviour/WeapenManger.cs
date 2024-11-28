using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeapenManger : MonoBehaviour
{
   public WeapenLibrary Playerlibrary;
   public WeapenLibrary Alllibrary;
   public RemindLibrary weapenRemindlibrary;
   public RemindLibrary remindlibrary;
   public RemindLibrary Night3;
   public RemindLibrary Night4;
   public RemindLibrary Night56;
   public int chara;

   /*public ObjectEventSO test;
[ContextMenu("Test")]
   public void Eventtest()
   {
      test.RaiseEvent(null,this);
   }*/
   public void GetWeapen(int i)
   {
      Debug.Log(i);
      Playerlibrary.weapenList.Add(Alllibrary.weapenList[i]);
      if (Playerlibrary.weapenList.Count==2)
      {
         RemoveDuplicateRemindData();
      }
      int[][] remindIndices = {
         new int[] {0, 3, 5, 7},  // 对应 chara == 0
         new int[] {1, 2, 5, 7},  // 对应 chara == 1
         new int[] {1, 3, 4, 7},  // 对应 chara == 2
         new int[] {1, 3, 5, 6}   // 对应 chara == 3
      };

      if (i < 4)
      {
         int index = remindIndices[chara][i];
         remindlibrary.remindPool.Add(Night3.remindPool[index]);
      }
      else
      {
         remindlibrary.remindPool.Add(Night4.remindPool[i-4]);
      }
   }

   public void initChara(int i)
   {
      chara = i;
      GetWeapen(i);
      
   }
   [ContextMenu("Test")]
   public void RemoveDuplicateRemindData()
   {
      // 使用 HashSet 提高查重效率
      HashSet<RemindData> weapenRemindSet = new HashSet<RemindData>(weapenRemindlibrary.remindPool);

      // 去除重复项
      remindlibrary.remindPool = remindlibrary.remindPool
         .Where(remindData => !weapenRemindSet.Contains(remindData))
         .ToList();

     
   }

   public void EvoA(int i)
   {
      for (int j = 0; j < Playerlibrary.weapenList.Count; j++)
      {
         if (Playerlibrary.weapenList[j].id  == i+1)
         {
            if (Playerlibrary.weapenList[j].state == 0)
            {
               Playerlibrary.weapenList[j].state = 1;
               remindlibrary.remindPool.Add(Night56.remindPool[j]);
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
         if (Playerlibrary.weapenList[j].id  == i+1)
         {
            if (Playerlibrary.weapenList[j].state == 0)
            {
               Playerlibrary.weapenList[j].state = 3;
               remindlibrary.remindPool.Add(Night56.remindPool[j]);
            }
            else if ( Playerlibrary.weapenList[j].state == 3)
            {
               Playerlibrary.weapenList[j].state = 4;
            }
            
         }
      }
   }
}
