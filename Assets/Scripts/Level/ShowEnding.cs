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
        ending = getEnding() >= 0 ? "�������" : "������";
        MyText.text = $"����������! �� ������ ���� �� {ending} ��������. ������ ����� ������.\n�������, ��� ������!";
    }
    void Update()
    {
        
    }

}
