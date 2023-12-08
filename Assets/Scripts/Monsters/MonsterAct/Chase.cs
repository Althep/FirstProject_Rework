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
        Debug.Log("chase NextPosFunction");
        pathFinding.Astar(myState.target.transform.position);
        if(pathFinding.path.Count != 0)
        {
            myState.path = pathFinding.path;
            nextPos.x = myState.path[0].x;
            nextPos.y = myState.path[0].y;
        }
        else
        {
            myState.myActState = this.gameObject.transform.GetComponent<Rest>();
        }



    }





}
