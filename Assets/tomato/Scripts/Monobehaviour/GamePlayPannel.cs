using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPannel : MonoBehaviour
{
   private VisualElement root;
   private VisualElement hpContainer;
   public VisualTreeAsset zzzTemple;
   public IntVarible hpVarible;
   public int maxHp { get => hpVarible.maxVaule; }
   public int currentHp { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      hpContainer = root.Q<VisualElement>("HpContainer");

      InitHp();
   }

   public void InitHp()
   {
      // 清空 hpContainer 现有的子元素（防止重复生成）
      hpContainer.Clear();
      for (int i = 0; i < currentHp; i++)
      {
        
         // 创建新的 zzz 元素（克隆原始模板 zzz）
         VisualElement temple = zzzTemple.Instantiate();
         var  zzz  = temple.Q<VisualElement>("zzz");
         hpContainer.Add(zzz);
      }
   }

   [ContextMenu("测试")]
   public void Play()
   {
      currentHp -= 1;
   }
}
