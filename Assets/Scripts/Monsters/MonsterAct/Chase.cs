using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonsterActSate
{
    PathFinding astarScript;
    Vector2 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        astarScript = this.transform.GetComponent<PathFinding>();
        myState = this.gameObject.transform.GetComponent<MonsterState>();
    }

    void Update()
    {
        
    }
    public void GetNext()
    {
        Vector2 temp = new Vector2();
        temp.x = astarScript.path[0].x;
        temp.y = astarScript.path[0].y;

        SetNextPos(temp);
    }
    void IsAttack()
    {
        if (myState.turn >= 1)
        {
            float distance;
            distance = Vector2.Distance(this.gameObject.transform.position, myState.target.transform.position);
            if (distance <= myState.attackRange)
            {
                myState.Attack();
                myState.turn--;
            }
        }
        
    }
    
}
