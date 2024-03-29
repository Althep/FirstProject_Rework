using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActSate : MonoBehaviour
{
    MapMake mapScript;
    //[SerializeField] protected Vector2 nextPos;
    public Vector2 playerOldPos;
    protected MonsterState myState;
    protected PathFinding astarScript;
    public GameObject playerObj;
    protected PathFinding pathFinding;
    private void OnEnable()
    {
        myState = this.gameObject.transform.GetComponent<MonsterState>();
        mapScript = GameManager.instance.gameObject.transform.GetComponent<MapMake>();
        astarScript = this.transform.GetComponent<PathFinding>();
        playerObj = GameManager.instance.playerObj;
        pathFinding = this.transform.GetComponent<PathFinding>();
    }

    public virtual void SetNextPos()
    {
        Debug.Log("default NextPosFunction");
    }

    public virtual void CollideStart()
    {

    }

    protected void Move()
    {
        
        Vector2 now = this.transform.position;
        Vector2 MoveTo = myState.nextPos - now;
        float maxDistance = SetMoveDistance(MoveTo);
        mapScript.TileInfoSwap(now, myState.nextPos, mapScript.monsterPosList, mapScript.tilePosList, TileType.monster);
        this.transform.position = myState.nextPos;
    }
    IEnumerator SlideMove()
    {
        Vector2 now = this.transform.position;
        Vector2 MoveTo = myState.nextPos - now;
        float maxDistance = SetMoveDistance(MoveTo);
        while (Vector2.Distance(now, myState.nextPos) >0.2f)
        {
            this.transform.Translate(MoveTo*Time.deltaTime*4f);
            if (maxDistance <= Vector2.Distance(this.transform.position, myState.nextPos))
            {
                Debug.Log("MonsterMoveBreak;");
                break;
            }
                
            yield return null;
        }

    }
    protected void Attack()
    {
        Debug.Log("Attack");
        LivingEntity targetEntity;
        if (Vector2.Distance(this.transform.position, myState.target.transform.position) <= myState.attackRange)
        {
            targetEntity = myState.target.transform.GetComponent<LivingEntity>();
            targetEntity.Damaged(myState.damage);
        }
    }

    public void TurnAct()
    {
        switch (myState.moveState)
        {
            case MoveState.idle:
                break;
            case MoveState.move:
                Move();
                break;
            case MoveState.attack:
                Debug.Log("DoAttack");
                //Attack();
                break;
            default:
                break;
        }
    }

    protected void GetRandomAroundPos()
    {
        Vector2 nowPos = this.transform.position;
        int[] dx = new int[]{1, 1, 1, 0, 0, 0, -1, -1, -1};
        int[] dy = new int[]{1, 0, -1, 1, 0, -1, 1, 0, -1};
        List<Vector2> tiles = new List<Vector2>();
        for(int i =0; i < dx.Length; i++)
        {
            if((nowPos.x+dx[i])>=mapScript.xSize || nowPos.y+dy[i]>=mapScript.ySize || nowPos.x+dx[i]<0 || nowPos.y+dy[i]<0)
            {
                continue;
            }
            if(mapScript.map[((int)nowPos.y+dy[i]), ((int)nowPos.x + dx[i])] == TileType.tile)
            {
                tiles.Add(new Vector2(nowPos.x + dx[i], nowPos.y + dy[i]));
            }
        }
        myState.nextPos = tiles[Random.Range(0, tiles.Count)];
    }
    float SetMoveDistance(Vector2 moveDirection)
    {
        float maxDixtance = 1.5f;
        if (Mathf.Abs(moveDirection.x) == Mathf.Abs(moveDirection.y))
        {
            maxDixtance = 1.4f;
        }
        else
        {
            maxDixtance = 1.1f;
        }
        return maxDixtance;
    }

}
