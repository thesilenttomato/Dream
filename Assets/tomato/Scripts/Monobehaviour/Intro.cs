using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Intro : MonoBehaviour
{
   private VisualElement root;
   private VisualElement face;
   private Label title;
   private Label description;

   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
   }
}
