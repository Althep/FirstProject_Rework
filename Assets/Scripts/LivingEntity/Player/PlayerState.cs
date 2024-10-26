using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class PlayerState : LivingEntity
{
    public GameObject player;
    public InputManager inputManager;

    public MagicInventory myMagic = new MagicInventory();

    
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
        myState.moveSpeed = myState.base_MoveSpeed;
        myState.attackSpeed = myState.base_AttackSpeed;
        myState.maxHp = 20;
        myState.currntHp = 20;
    }
    private void FixedUpdate()
    {
        
    }
    public void PlayerInstantiate()
    {
        if(GameManager.instance.playerObj == null)
        {
            GameManager.instance.playerObj = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(inputManager == null)
        {
            inputManager = GameManager.instance.transform.GetComponent<InputManager>();
        }
    }
    public override void Attack(LivingEntity target)
    {
        target.Damaged(myState.damage);
    }



}
