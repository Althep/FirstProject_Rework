using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : MonsterActSate
{

    // Start is called before the first frame update
    public override void CollideStart()
    {
        myState.myActState = this.transform.GetComponent<Chase>();
    }


    public override void SetNextPos()
    {
        //playerOldPos = GameManager.instance.playerObj.transform.position;
        pathFinding.Astar(myState.oldPlayerPos);
        if (pathFinding.path.Count>2)
        {
            myState.path = pathFinding.path;
            myState.nextPos.x = myState.path[1].x;
            myState.nextPos.y = myState.path[1].y;
        }
        else
        {
            Debug.Log("searching Path Fild");
            myState.nextPos = this.transform.position;
        }
        
    }

}
