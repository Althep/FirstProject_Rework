using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : MonsterActSate
{

    // Start is called before the first frame update
    public override void CollideStart()
    {
        myState.monsterActState = this.transform.GetComponent<Chase>();
    }
}
