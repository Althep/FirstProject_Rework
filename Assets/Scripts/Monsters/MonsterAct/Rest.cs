using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonsterActSate
{
    public override void CollideStart()
    {
        myState.monsterActState = this.transform.GetComponent<Chase>();
    }
}
