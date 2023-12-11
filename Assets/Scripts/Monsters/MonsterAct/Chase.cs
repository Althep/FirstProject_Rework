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
        if(pathFinding.path.Count >2)
        {
            myState.path = pathFinding.path;
            nextPos.x = myState.path[1].x;
            nextPos.y = myState.path[1].y;
            myState.oldPlayerPos = myState.target.transform.position;

        }
        else
        {
            Debug.Log("getPathFaild");
            myState.myActState = this.gameObject.transform.GetComponent<Rest>();
        }
    }
}
