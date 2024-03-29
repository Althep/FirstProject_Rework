using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : LivingEntity
{
    public GameObject player;
    public InputManager inputManager;
    
    private void Awake()
    {
        inputManager = GameManager.instance.transform.GetComponent<InputManager>();
        PlayerInstantiate();
    }
    private void Start()
    {
        base_AttackSpeed = 10;
        base_MoveSpeed = 10;
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
    }
    public override void Attack(LivingEntity target)
    {
        
    }



}
