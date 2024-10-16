using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class PlayerState : LivingEntity
{
    public GameObject player;
    public InputManager inputManager;

    public ItemInventory myInventory = new ItemInventory();
    public MagicInventory myMagic = new MagicInventory();

    
    private void Awake()
    {
        inputManager = GameManager.instance.transform.GetComponent<InputManager>();
        PlayerInstantiate();
        name = "Player";
    }
    private void Start()
    {
        myState.base_AttackSpeed = 10;
        myState.base_MoveSpeed = 10;
        myState.moveSpeed = myState.base_MoveSpeed;
        myState.attackSpeed = myState.base_AttackSpeed;
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
