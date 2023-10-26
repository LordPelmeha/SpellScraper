using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMask : MonoBehaviour
{
    public Image Masks;
    public Sprite fireMask;
    public Sprite waterMask;
    public Sprite earthMask;
    public Sprite airMask;
    private int scrollMask = 0;

    void Update()
    {
        //Изменение счётчика масок за счёт прокурутки колёсика
        if (Input.mouseScrollDelta.y > 0)
            scrollMask = (scrollMask + 1) % 4;
        else if (Input.mouseScrollDelta.y < 0)
        {
            if (scrollMask == 0)
                scrollMask = 4;
            scrollMask = (scrollMask - 1) % 4;
        }

        //Меняет спрайт маски в зависимости от нажатой клавиши
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Masks.sprite = fireMask;
            scrollMask = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Masks.sprite = waterMask;
            scrollMask = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Masks.sprite = earthMask;
            scrollMask = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Masks.sprite = airMask;
            scrollMask = 3;
        }
        //Меняет спрайт в зависимости от счётчика прокрутки мыши
        else if (scrollMask == 0)
            Masks.sprite = fireMask;
        else if (scrollMask == 1)
            Masks.sprite = waterMask;
        else if (scrollMask == 2)
            Masks.sprite = earthMask;
        else if (scrollMask == 3)
            Masks.sprite = airMask;
    }
}
