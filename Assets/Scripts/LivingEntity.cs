using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    int maxHp;
    int currntHp;
    bool isDead;
    int turnPont;
    public int turn;
    protected virtual void Damaged(int damage)
    {
        currntHp -= damage;
        IsDead();
    }
    public virtual void Attack()
    {

    }
    protected void IsDead()
    {
        if (currntHp <= 0)
        {
            isDead = true;
        }
    }
}
