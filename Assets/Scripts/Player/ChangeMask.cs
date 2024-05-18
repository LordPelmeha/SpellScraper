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

    public Sprite[] brokenForBossSprite;

    private MiniBoss boss;
    private bool isBossOnLevel;
    protected static int scrollMask;
    protected static int countEnd = 0;
    [SerializeField] Player player;
    private bool checkHPDecrease = true;
    private void Start()
    {
        isBossOnLevel = GameObject.Find("Boss") != null;
        if (isBossOnLevel)
            boss = GameObject.FindWithTag("Enemy").GetComponent<MiniBoss>();
    }
    void Update()
    {

        if (player.health == 0.5)
        {
            if (checkHPDecrease)
                breakMask();
            checkHPDecrease = false;
        }
        if (!checkHPDecrease && player.health>0.5)
        {
            healMask();
            checkHPDecrease=true;
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
            Masks.sprite = earthMask;
            scrollMask = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Masks.sprite = waterMask;
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
            Masks.sprite = earthMask;
        else if (scrollMask == 2)
            Masks.sprite = waterMask;
        else if (scrollMask == 3)
            Masks.sprite = airMask;
        if (isBossOnLevel)
            getBossMagic();
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
    private void healMask()
    {
        fireMask = forBossSprite[0];
        airMask = forBossSprite[15];
        waterMask = forBossSprite[10];
        earthMask = forBossSprite[5];
    }
    private void getBossMagic()
    {
        if (player.health > 0.5)
            Masks.sprite = forBossSprite[4 * (int)boss.magicType + scrollMask];
        else
            Masks.sprite = brokenForBossSprite[4 * (int)boss.magicType + scrollMask];
        Debug.Log((int)boss.magicType + scrollMask);
    }
}
