using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShowEnding : ChangeMask
{
    public TextMeshProUGUI MyText;
    private string ending;

     private void Start()
    {
        ending = getEnding() >= 0 ? "хорошую" : "плохую";
        MyText.text = $"Поздравлем! Вы прошли игру на {ending} концовку. Дальше будет больше.\nСпасибо, что играли!";
    }
    void Update()
    {
        
    }

}
