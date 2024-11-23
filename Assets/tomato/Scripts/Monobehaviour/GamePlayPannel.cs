using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPannel : MonoBehaviour
{
   private VisualElement root;
   private VisualElement hpContainer;
   private Label timeLabel;
   public VisualTreeAsset zzzTemple;
   public IntVarible hpVarible;
   
   public int maxHp { get => hpVarible.maxVaule; }
   public int currentHp { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

   public IntVarible hourVarible;
   public int hour { get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }
   public IntVarible roundTimeVarible;
   public int time { get => roundTimeVarible.currentVaule; set => roundTimeVarible.SetValue(value); }
   private float elapsedTime;
   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      hpContainer = root.Q<VisualElement>("HpContainer");
      timeLabel = root.Q<Label>("Time");
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

   private void InitTime()
   {
      hour = 0;
      time = 0;
   }

   private void Update()
   {
      elapsedTime += Time.deltaTime;

      // 每满一秒，增加 time
      if (elapsedTime >= 2f)
      {
         time++;
         elapsedTime = 0f; // 重置累积时间
         
      }

      if (time >= 60)
      {
         hour++;
         time = 0;
      }

      UpdateTimeLabel();
   }

   private void UpdateTimeLabel()
   {
      if (time < 10)
      {
         timeLabel.text = $"{hour}:0{time}";
      }
      else
      {
         timeLabel.text = $"{hour}:{time}";
      }
      
   }

   [ContextMenu("测试")]
   public void Play()
   {
      currentHp -= 1;
   }
}
