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
        if (pathFinding.path.Count != 0)
        {
            nextPos.x = pathFinding.path[0].x;
            nextPos.y = pathFinding.path[0].y;
            pathFinding.path.RemoveAt(0);
        }
        else
        {
            myState.myActState = this.gameObject.transform.GetComponent<Rest>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
