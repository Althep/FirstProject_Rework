using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonsterActSate
{
    public override void CollideStart()
    {
        myState.myActState = this.transform.GetComponent<Chase>();
    }


    public override void SetNextPos()
    {
        GetRandomAroundPos();
    }
   

}
