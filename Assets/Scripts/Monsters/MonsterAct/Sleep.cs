using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonsterActSate
{

    protected override void SetNextPos(Vector2 next)
    {
        base.SetNextPos(this.gameObject.transform.position);
    }

    public override void CollideStart()
    {
        int temp = Random.Range(0, 100);
        if (temp <= myState.awakingRate)
        {
            myState.monsterActState = this.gameObject.transform.GetComponent<Chase>();
        }
    }
}
