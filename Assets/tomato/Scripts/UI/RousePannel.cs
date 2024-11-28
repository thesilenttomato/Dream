using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RousedPannel : MonoBehaviour
{
   public ObjectEventSO loadFight;
   public ObjectEventSO loadMneuSO;
   private VisualElement root;
   private Button backToMenu;
   private Button again;
   public IntVarible hpVarible;
   public int maxHp { get => hpVarible.maxVaule; }
   public int currentHp { get => hpVarible.currentVaule; set => hpVarible.SetValue(value); }

   public IntVarible hourVarible;
   public int hour { get => hourVarible.currentVaule; set => hourVarible.SetValue(value); }

   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      backToMenu = root.Q<Button>("BackToMenu");
      again = root.Q<Button>("again");
      again.clicked += () => Countinue();
      backToMenu.clicked += () => loadMenu();
      currentHp = maxHp;
      hour += 2;
      if (hour >= 24) { hour -= 24; }
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         loadMenu();
      }

      if (Input.GetKeyDown(KeyCode.J))
      {
         Countinue();
      }
   }

   private void Countinue()
   {
      
      this.gameObject.SetActive(false);
      loadFight.RaiseEvent(null,this);
   }

   private void loadMenu()
   {
      this.gameObject.SetActive(false);
      loadMneuSO.RaiseEvent(null,this);
   }
}
