using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPannel : MonoBehaviour
{
   private VisualElement root;

   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      
   }
}
