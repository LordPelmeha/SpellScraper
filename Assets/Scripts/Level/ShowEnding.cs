using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShowEnding : ChangeMask
{
    public Image ending;

    public Sprite end1;
    public Sprite end2;

    //public TextMeshProUGUI MyText;
    //private string ending;


     private void Start()
    {
        //ending = PlayerPrefs.GetInt("CountEnd") >= 0 ? "хорошую" : "плохую";
        //MyText.text = $"Поздравляем! Вы прошли игру на {ending} концовку. Дальше будет больше.\nСпасибо, что играли!";
        if (PlayerPrefs.GetInt("CountEnd") >= 0)
            ending.sprite = end1;
        else
            ending.sprite = end2;
    }
    void Update()
    {
        
    }

}
