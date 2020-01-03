using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    [Header("Inputs")]
    //[SerializeField] public GameObject self;
    [SerializeField] public ExperienceSystem playerEXP;
    [SerializeField] public float levelnum;
    [SerializeField] public Image tab;
    [SerializeField] public Image border;
    [SerializeField] public Image xp;
    [SerializeField] public TextMeshProUGUI lvlText;
    [SerializeField] public TextMeshProUGUI levelUpText;

    [Header("Bools")]
    [SerializeField] public bool beginXpScroll = false;

    [Header("enums")]
    [SerializeField] public State state;

    [Header("fideleties")]
    [SerializeField] public float tabspeed = 0.02f;
    [SerializeField] public float lvlTextSpeed = 0.02f;
    [SerializeField] public float textRollSpeed = 0.2f;
    [SerializeField] public float fillSpeed = 0.5f;
    [SerializeField] public float rollMagnitude = 0.02f;

    [Header("Inputs")]
    [SerializeField] public TimerVariant levelnumAppear;
    [SerializeField] public TimerVariant dissapearAll;

    [SerializeField] private float progress = 0f;
    [SerializeField] private GameObject particleObject;
    public bool rollLevelbool = false;

    [SerializeField]
    public enum State
    {
        waiting,
        lvlUp,
        levelNum,
        dissapearAll,
        resetAll, //resets all states.
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener<LevelUpEvent>(LevelUpCall);
        particleObject.SetActive(false);
    }

    private void Awake()
    {
        levelnum = playerEXP.CurrentLevel;
        setInvisible();

    }

    void setInvisible()
    {

    }


    public void LevelUpCall(LevelUpEvent ev)
    {
        state = State.resetAll;
        levelnum = playerEXP.CurrentLevel;
    }


    void UiUpdate()
    {
        switch (state)
        {
            //initial state which does nothing.
            case State.waiting:
                break;

            //initially levels up.
            case State.lvlUp:
                levelnum = playerEXP.CurrentLevel;
                fadeInTab();

                //set count!
                lvlText.text = "0";

                //start timers...!
                levelnumAppear.finished = false;
                particleObject.SetActive(true);

                break;
            case State.levelNum:
                particleObject.SetActive(false);
                lvlText.alpha = 255;
                levelCountRise();
                appearLevelUpTxt();
                fadeInBorder();
                rollXP();
                //display particle
                //bool rollLevelbool true
                break;
            //fade out all
            case State.dissapearAll:
                lvlText.alpha = 0;

                rollLevelbool = false;
                hide();
                particleObject.SetActive(false);

                //state = State.waiting;
                break;

            //resets all to 0 state before starting Level up Instance. This stops potential glitches where the player leveling up many times at once causes bizzare events.
            case State.resetAll:
                var tabCol = tab.color;
                tabCol.a = 0;
                tab.color = tabCol;
                border.color = tabCol;
                rollLevelbool = false;
                lvlText.alpha = 0;
                levelUpText.alpha = 0;
                progress = 0f;
                particleObject.SetActive(false);
                xp.fillAmount = 0;
                levelnum = playerEXP.CurrentLevel;
                state = State.lvlUp;
                levelnumAppear.finished = true;
                dissapearAll.finished = true;
                break;
        }

        //Debug remove if finished.
        if (Input.GetKeyDown(KeyCode.U))
        {
            state = State.resetAll;
        }
    }

    public void fadeInTab()
    {
        var tabCol = tab.color;
        if (tabCol.a <= 1)
        {
            tabCol.a += tabspeed;
            tab.color = tabCol;
        }
        else
        {

            state = State.levelNum;
        }
    }

    public void fadeInBorder()
    {
        var tabCol = border.color;
        if (tabCol.a <= 1)
        {
            tabCol.a += tabspeed;
            border.color = tabCol;
        }
    }

    void rollXP()
    {
        if (xp.fillAmount < 1)
        {
            xp.fillAmount += fillSpeed * rollMagnitude;
        }
    }

    public void callNewState()
    {
        state = State.levelNum;
    }

    public void appearLevelTxt()
    {

        if (lvlText.alpha <= 255)
        {
            lvlText.alpha += lvlTextSpeed;
        }
    }
    public void appearLevelUpTxt()
    {
        if (levelUpText.alpha <= 255)
        {
            levelUpText.alpha += lvlTextSpeed;
        }
    }

    public void levelCountRise()
    {
        if (progress < levelnum)
        {
            progress += textRollSpeed;
            lvlText.text = ((int)progress).ToString();
        }
        else
        {
            if (!rollLevelbool)
            {
                dissapearAll.finished = false; //begin counter until fadeout
                rollLevelbool = true;
            }
        }
    }

    public void dissapear() { state = State.dissapearAll; }

    public void hide()
    {
        //levelUpText

        //LevelCount
        if (levelUpText.alpha > 0)
        {
            levelUpText.alpha -= lvlTextSpeed * 2.5f;
        }

        //hidetab
        var tabCol = tab.color;
        if (tabCol.a >= 0)
        {
            tabCol.a -= tabspeed;
            tab.color = tabCol;
        }

        //hidetab
        var bordervar = border.color;
        if (bordervar.a >= 0)
        {
            bordervar.a -= tabspeed;
            border.color = bordervar;
        }

        ////xp
        if (xp.fillAmount >= 0)
        {
            xp.fillAmount -= fillSpeed;
        }
    }

    public void hideLvlNum()
    {

    }
    private void Update() { UiUpdate(); }
}
