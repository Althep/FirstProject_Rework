using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;

public class PlayerState : LivingEntity
{
    public GameObject player;
    public InputManager inputManager;

    public MagicInventory myMagic = new MagicInventory();

    public int level;
    public int maxExp;
    public int exp;

    private void Awake()
    {
        inputManager = GameManager.instance.transform.GetComponent<InputManager>();
        PlayerInstantiate();
        name = "Player";
        SetCanvas();
        
    }
    private void Start()
    {
        myState.damage = 3;
        myState.base_AttackSpeed = 10;
        myState.base_MoveSpeed = 10;
        myState.str = 10;
        myState.dex = 10;
        myState.intel = 10;
        myState.moveSpeed = myState.base_MoveSpeed;
        myState.attackSpeed = myState.base_AttackSpeed;
        myState.maxHp = 20;
        myState.currntHp = 20;
        maxExp = 10;
        exp = 0;
        EventManager.Instance.OnLevelUp.AddListener(LevelUp);
    }
    private void FixedUpdate()
    {

    }
    public void PlayerInstantiate()
    {
        if (GameManager.instance.playerObj == null)
        {
            GameManager.instance.playerObj = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (inputManager == null)
        {
            inputManager = GameManager.instance.transform.GetComponent<InputManager>();
        }
    }
    public override void Attack(LivingEntity target)
    {
        target.Damaged(myState.damage);
        if (exp >= maxExp)
        {
            EventManager.Instance.OnLevelUp.Invoke();
        }

    }
    public override void Damaged(int damage)
    {
        base.Damaged(damage);
        EventManager.Instance.OnPlayerBattle.Invoke();
    }

    public void LevelUp()
    {
        float hprate = myState.currntHp / myState.maxHp;
        exp -= maxExp;
        maxExp += 10;
        level++;
        myState.maxHp += 10;
        myState.maxMp += 5;
        myState.str++;
        myState.dex++;
        myState.intel++;
        myState.currntHp = (int)(myState.maxHp * hprate);
    }

}
