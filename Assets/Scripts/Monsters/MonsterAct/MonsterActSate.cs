using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActSate : MonoBehaviour
{
    MapMake mapScript;
    protected Vector2 nextPos;
    protected MonsterState myState;
    private void OnEnable()
    {
        myState = this.gameObject.transform.GetComponent<MonsterState>();
        mapScript = GameManager.instance.gameObject.transform.GetComponent<MapMake>();
    }
    
    protected virtual void SetNextPos(Vector2 next)
    {
        nextPos.x = next.x;
        nextPos.y = next.y;
    }

    public virtual void CollideStart()
    {

    }
    public virtual void MoveTo(Vector2 next)
    {
        Vector2 now = this.transform.position;
        Vector2 MoveTo = next - now;
        mapScript.TileInfoSwap(now,next,mapScript.monsterPosList,mapScript.tilePosList,TileType.monster);
        //Todo : monster Actually Move
        this.gameObject.transform.Translate(MoveTo);
    }
}
