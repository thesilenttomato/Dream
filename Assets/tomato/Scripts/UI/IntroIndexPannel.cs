using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroIndexPannel : MonoBehaviour
{
    private VisualElement root;
    private Button WeapenButton;
    private Button EnemyButton;
    private Button BackButton;

    public List<IntroSO> Weapens;
    public List<IntroSO> Enemys;
    public Intro intro;

    private void OnEnable()
    {
        root = this.GetComponent<UIDocument>().rootVisualElement;
        
        WeapenButton = root.Q<Button>("Weapen");
        EnemyButton =  root.Q<Button>("Enemy");
        BackButton = root.Q<Button>("Back");
        WeapenButton.clicked += () => OnButtonClicked(true);
        EnemyButton.clicked += () => OnButtonClicked(false);
        BackButton.clicked += () => Back();

    }

    private void Update()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void OnButtonClicked(bool weapen)
    {
        if (weapen)
        {
            intro.contents = Weapens;
        }
        else
        {
            intro.contents = Enemys;
        }
        intro.gameObject.SetActive(true);
    }

    private void Back()
    {
        this.gameObject.SetActive(false);
    }
}
