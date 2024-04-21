using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonsterActSate
{
    void Start()
    {
        myState = this.gameObject.transform.GetComponent<MonsterState>();
    }

    public override void SetNextPos()
    {
        pathFinding.Astar(myState.target.transform.position);
        if(pathFinding.path.Count >=2)
        {
            myState.path = pathFinding.path;
            myState.nextPos.x = myState.path[1].x;
            myState.nextPos.y = myState.path[1].y;
            myState.oldPlayerPos = myState.target.transform.position;

        }
        else if (pathFinding.path.Count == 1&& Vector2.Distance(playerObj.transform.position,this.gameObject.transform.position)<1.42f)
        {
            myState.moveState = MoveState.attack;
        }
        else
        {
            myState.myActState = this.gameObject.transform.GetComponent<Rest>();
        }
    }
}
