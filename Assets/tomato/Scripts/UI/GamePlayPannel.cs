using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPannel : MonoBehaviour
{
   public UIManger UIManger;
   private VisualElement root;
   private VisualElement hpContainer;
   private VisualElement hourHand;
   private VisualElement minuteHand;
   private Label timeLabel;
   public VisualTreeAsset zzzTemple;
   public VisualTreeAsset waepenTemple;
   public WeapenLibrary weapenLibrary;
   public IntVarible hpVarible;
   private VisualElement weapenContainer;
   private Button ESC;
   public int maxHp { get => hpVarible.maxVaule; }
   public int currentHp { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

   public IntVarible hourVarible;
   public int hour { get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }
   public IntVarible roundTimeVarible;
   public int roundtime { get => roundTimeVarible.currentVaule; set => roundTimeVarible.SetValue(value); }
   private float elapsedTime;
   private float hpElapsedTime;

   public List<Sprite> HpSprites = new List<Sprite>();
   private int currentSpriteIndex = 0; // 当前使用的图片索引
   private void OnEnable()
   {
      
      root = GetComponent<UIDocument>().rootVisualElement;
      ESC = root.Q<Button>("ESC");
      ESC.clicked += () => OpenSetting();
      hpContainer = root.Q<VisualElement>("HpContainer");
      timeLabel = root.Q<Label>("Time");
      hourHand = root.Q<VisualElement>("HourHand");
      minuteHand = root.Q<VisualElement>("MinuteHand");
      weapenContainer = root.Q<VisualElement>("WeapenContainer");
      currentHp = maxHp;
      InitWeapen();
   }
   


   private void OpenSetting()
   {
      UIManger.OpenGameSettings();
   }

   public void InitWeapen()
   {
      weapenContainer.Clear();
      if (weapenLibrary != null)
      {
         for (int i = 0; i < weapenLibrary.weapenList.Count; i++)
         {
        
            VisualElement temple = waepenTemple.Instantiate();
            var  waepen  = temple.Q<VisualElement>("Weapen");
            waepen.style.backgroundImage = new StyleBackground(weapenLibrary.weapenList[i].Sprite);
            weapenContainer.Add(waepen);
         }
      }
     
   }
[ContextMenu("hurt")]
   public void hurt()
   {
      currentHp -= 1;
   }
   [ContextMenu("clock")]
   public void clock()
   {
      roundtime = 59;
   }
   public void Gethp()
   {
      int bigHp = Mathf.RoundToInt(maxHp * 1.5f);
      currentHp += bigHp-maxHp;
      hpVarible.maxVaule = bigHp;
   }

  

   public void UpdateHp()
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
      roundtime = 0;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape) )
      {
         OpenSetting();
      }
      elapsedTime += Time.deltaTime;
      hpElapsedTime += Time.deltaTime;
      // 每满一秒，增加 time
      if (elapsedTime >= 2f)
      {
         roundtime++;
         elapsedTime = 0f; // 重置累积时间
        
      }

      if (hpElapsedTime >= 0.5f)
      {
         UpdateZzzSprites();
         hpElapsedTime = 0f;
      }

      if (roundtime >= 60)
      {
         if (hour >= 23)
         {
            hour = 0;
            roundtime = 0;
         }
         else
         {
            hour++;
            roundtime = 0;
         }
         
      }

      

      UpdateTimeLabel();
      UpdateClock();
   }
   private void UpdateZzzSprites()
   {
      // 切换到下一张图片
      if (HpSprites.Count > 0)
      {
         currentSpriteIndex = (currentSpriteIndex + 1) % HpSprites.Count; // 循环索引
         Sprite nextSprite = HpSprites[currentSpriteIndex];

         // 更新所有 zzz 的背景图片
         foreach (var zzz in hpContainer.Children())
         {
            zzz.style.backgroundImage = new StyleBackground(nextSprite);
         }
      }
   }

   private void UpdateTimeLabel()
   {
      if (roundtime < 10)
      {
         timeLabel.text = $"{hour}:0{roundtime}";
      }
      else
      {
         timeLabel.text = $"{hour}:{roundtime}";
      }
      
   }
   private void UpdateClock()
   {
      // 分针每秒转动 6°
      float minuteAngle = roundtime * 6f;

      // 时针每小时转动 30°，每分钟进阶 0.5°
      float hourAngle = hour * 30f + (roundtime / 60f) * 30f;

      // 设置旋转角度
      minuteHand.style.rotate = new Rotate(new Angle(minuteAngle, AngleUnit.Degree));
      hourHand.style.rotate = new Rotate(new Angle(hourAngle, AngleUnit.Degree));
   }
   
   
   
}
