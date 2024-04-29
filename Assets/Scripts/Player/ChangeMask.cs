using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMask : MonoBehaviour
{
    public Image Masks;

    public Sprite fireMask;
    public Sprite airMask;
    public Sprite waterMask;
    public Sprite earthMask;

    public Sprite[] forBossSprite;

    public Sprite brokenFireMask;
    public Sprite brokenAirMask;
    public Sprite brokenWaterMask;
    public Sprite brokeEearthMask;


    private MiniBoss boss;
    private bool isBossOnLevel;
    protected static int scrollMask;
    protected static int countEnd = 0;
    [SerializeField] Player player;
    private bool checkHPDecrease = true;
    private void Start()
    {
        isBossOnLevel = GameObject.FindWithTag("Enemy")!=null;
        if (isBossOnLevel) 
            boss = GetComponent<MiniBoss>();
    }
    void Update()
    {
        if (isBossOnLevel) 
            getBossMagic();
        if (player.health == 0.5)
        {
            if (checkHPDecrease)
                breakMask();
            checkHPDecrease = false;
        }
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
            Masks.sprite = airMask;
            scrollMask = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Masks.sprite = waterMask;
            scrollMask = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Masks.sprite = earthMask;
            scrollMask = 3;
        }
        //Меняет спрайт в зависимости от счётчика прокрутки мыши
        else if (scrollMask == 0)
            Masks.sprite = fireMask;
        else if (scrollMask == 1)
            Masks.sprite = airMask;
        else if (scrollMask == 2)
            Masks.sprite = waterMask;
        else if (scrollMask == 3)
            Masks.sprite = earthMask;
    }
    public int getMagic()
    {
        return scrollMask;
    }
    public int getEnding()
    {
        return countEnd;
    }
    private void breakMask()
    {
        fireMask = brokenFireMask;
        airMask = brokenAirMask;
        waterMask = brokenWaterMask;
        earthMask = brokeEearthMask;
    }
    private void getBossMagic()
    {
        Masks.sprite = forBossSprite[(int)boss.magicType + scrollMask];
    }
}
