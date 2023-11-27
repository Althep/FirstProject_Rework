using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : LivingEntity
{
    public GameObject target;
    PathFinding astar;
    List<Node> path;
    public int damage;
    public MonsterActSate monsterActState;
    Vector2 playerPos;
    bool canAttack;
    public float attackRange;
    public int awakingRate;




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(monsterActState is Sleep)
        {
            monsterActState.CollideStart();
        }
        else
        {
            monsterActState = this.transform.GetComponent<Chase>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(monsterActState is Chase)
        {
            monsterActState = this.transform.GetComponent<Searching>();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!monsterActState is Sleep || !monsterActState is Chase)
        {
            monsterActState = this.transform.GetComponent<Chase>();
        }
    }
}
