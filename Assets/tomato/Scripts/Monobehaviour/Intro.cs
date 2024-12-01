using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Intro : MonoBehaviour
{
   private VisualElement root;
   private VisualElement face;
   private Label title;
   private Label description;
   private Button next;
   private Button back;
   private int index;
   public List<IntroSO> contents;

   private void OnEnable()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      face = root.Q<VisualElement>("face");
      title = root.Q<Label>("title");
      description = root.Q<Label>("description");
      next = root.Q<Button>("next");
      back = root.Q<Button>("back");
      index = 0;
      next.clicked += () => Next();
      back.clicked += () => Back();
      Show(index);
   }

   private void Back()
   {
      this.gameObject.SetActive(false);
   }

   private void Next()
   {
      index += 1;
      if (index >= contents.Count)
      {
         index = 0;
      }
      Show(index);
        
   }

   private void Show(int index)
   {
      face.style.backgroundImage = new StyleBackground(contents[index].sprite);
      title.text = contents[index].title;
      description.text = contents[index].description;
   }
}
