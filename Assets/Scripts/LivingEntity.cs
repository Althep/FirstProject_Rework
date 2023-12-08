using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    protected int maxHp;
    protected int currntHp;
    protected bool isDead;
    protected int base_AttackSpeed;
    protected int base_MoveSpeed;
    public int attackSpeed=10;
    public int moveSpeed=10;
    public int turn;
    public int oldTurn;
    public MoveState moveState;


    private void Start()
    {
        

    }
    public virtual void Damaged(int damage)
    {
        currntHp -= damage;
        IsDead();
    }
    public virtual void Attack(LivingEntity target)
    {

    }
    protected virtual void IsDead()
    {
        
    }
    protected virtual void OnMove()
    {

    }
    protected virtual void SetSpeed()
    {
        attackSpeed = base_AttackSpeed;
        moveSpeed = base_MoveSpeed;
    }
}
