using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StateUI : UIBase
{
    string playerName;
    int floor;
    int turn;
    int level;
    int maxExp;
    int exp;
    int str;
    int dex;
    int intel;
    int maxHp;
    int currentHp;
    int maxMp;
    int currentMp;
    TextMeshProUGUI playerNameText;
    TextMeshProUGUI turnText;
    TextMeshProUGUI floorText;
    TextMeshProUGUI levelText;
    TextMeshProUGUI expText;
    TextMeshProUGUI strText;
    TextMeshProUGUI dexText;
    TextMeshProUGUI intText;
    TextMeshProUGUI hpText;
    TextMeshProUGUI mpText;

    Slider hpSlider;
    Slider mpSlider;

    PlayerState player;
    protected override void Init()
    {
        base.Init();

        playerNameText = GameObject.Find("nameText").transform.GetComponent<TextMeshProUGUI>();
        turnText = GameObject.Find("turnText").transform.GetComponent<TextMeshProUGUI>();
        floorText = GameObject.Find("floorText").transform.GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("levelText").transform.GetComponent<TextMeshProUGUI>();
        expText = GameObject.Find("expText").transform.GetComponent<TextMeshProUGUI>();
        strText = GameObject.Find("strText").transform.GetComponent<TextMeshProUGUI>();
        dexText = GameObject.Find("dexText").transform.GetComponent<TextMeshProUGUI>();
        intText = GameObject.Find("intText").transform.GetComponent<TextMeshProUGUI>();

        hpSlider = GameObject.Find("HPBar").transform.GetComponent<Slider>();
        mpSlider = GameObject.Find("MPBar").transform.GetComponent<Slider>();

        hpText = GameObject.Find("HpBarText").transform.GetComponent<TextMeshProUGUI>();
        mpText = GameObject.Find("MpBarText").transform.GetComponent<TextMeshProUGUI>();
        player = GameManager.instance.playerState;
        SetPlayerData();
        UpDateTurn();
        UpDateHp();
        UpdateMp();
        UpdateStats();
        EventManager.Instance.OnPlayerBattle.AddListener(OnBattle);
        EventManager.Instance.OnPlayerMove.AddListener(UpDateTurn);
        EventManager.Instance.OnLevelUp.AddListener(UpdateStats);
        EventManager.Instance.OnMonsterDead.AddListener(UpdateExp);
    }

    protected override void SetPosition()
    {
        RectTransform myrect = this.transform.GetComponent<RectTransform>();
        Vector2 myPos = new Vector2(Screen.width - (myrect.rect.width / 2), 0);
        myrect.anchoredPosition = myPos;
    }

    void SetPlayerData()
    {
        playerName = player.name;
        turn = GameManager.instance.turnManager.turn;
        floor = GameManager.instance.floor;
        level = player.level;
        maxExp = player.maxExp;
        exp = player.exp;

        str = player.myState.str;
        dex = player.myState.dex;
        intel = player.myState.intel;

        maxHp = player.myState.maxHp;
        currentHp = player.myState.currntHp;
        maxMp = player.myState.maxMp;
        currentMp = player.myState.currentMp;

    }


    void UpdateStats()
    {
        str = player.myState.str;
        dex = player.myState.dex;
        intel = player.myState.intel;
        maxHp = player.myState.maxHp;
        currentHp = player.myState.currntHp;
        hpText.text = currentHp + " / " + maxHp;
        strText.text = "str : " + str;
        dexText.text = "dex : " + dex;
        intText.text = "int : " + intel;
        exp = player.exp;
        maxExp = player.maxExp;
        expText.text = exp + " / " + maxExp;
        level = player.level;
        levelText.text = "Level : "+level;
        hpSlider.maxValue = maxHp;
        mpSlider.maxValue = maxMp;
    }
    public void UpDateTurn()
    {
        turn = TurnManager.instance.turn;
        turnText.text = "turn : " + turn.ToString();
    }
    void UpdateExp()
    {
        exp = player.exp;
        expText.text = exp + " / " + maxExp;
    }
    void UpDateHp()
    {
        maxHp = player.myState.maxHp;
        currentHp = player.myState.currntHp;
        hpText.text = currentHp + " / " + maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
    }
    void UpdateMp()
    {
        maxMp = player.myState.maxMp;
        currentMp = player.myState.currentMp;
        mpText.text = currentMp + " / " + maxMp;
        mpSlider.maxValue = maxMp;
        mpSlider.value = currentMp;
    }
    public void UpdateCurrentHp()
    {
        currentHp = player.myState.currntHp;
        hpSlider.value = currentHp;
        hpText.text = currentHp + " / " + maxHp;
    }

    public void UpDateCurrentMp()
    {
        currentMp = player.myState.currentMp;
        mpSlider.value = currentMp;
        mpText.text = currentMp + " / " + maxMp;
    }
    void OnBattle()
    {
        UpdateCurrentHp();
        UpDateCurrentMp();
    }
}
