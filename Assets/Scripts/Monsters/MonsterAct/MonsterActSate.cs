using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActSate : MonoBehaviour
{
    protected Vector2 nextPos;
    protected MonsterState myState;
    private void Start()
    {
        myState = this.gameObject.transform.GetComponent<MonsterState>();
    }
    protected virtual void SetNextPos(Vector2 next)
    {
        nextPos.x = next.x;
        nextPos.y = next.y;
    }

    public virtual void CollideStart()
    {

    }
}
