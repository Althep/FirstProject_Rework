using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonsterActSate
{

    public override void SetNextPos()
    {
        myState.nextPos = this.gameObject.transform.position;
    }

    public override void CollideStart()
    {
        int temp = Random.Range(0, 100);
        if (temp <= myState.myState.awakingRate)
        {
            myState.myActState = this.gameObject.transform.GetComponent<Chase>();
        }
    }

}
