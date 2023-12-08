using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActSate : MonoBehaviour
{
    MapMake mapScript;
    [SerializeField] protected Vector2 nextPos;
    protected Vector2 playerOldPos;
    protected Vector2 myOldPos;
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

    protected IEnumerator Move()
    {
        Vector2 now = this.transform.position;
        Vector2 MoveTo = nextPos - now;
        mapScript.TileInfoSwap(now, nextPos, mapScript.monsterPosList, mapScript.tilePosList, TileType.monster);

        while (Vector2.Distance(now,nextPos)>0.2f)
        {
            this.transform.Translate(MoveTo*Time.deltaTime*4f);
            yield return null;
        }
        myState.turn--;
        this.transform.position = nextPos;
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
        myState.turn--;
    }

    public void TurnAct()
    {
        switch (myState.moveState)
        {
            case MoveState.idle:
                break;
            case MoveState.move:
                StartCoroutine("Move");
                break;
            case MoveState.attack:
                Attack();
                break;
            default:
                break;
        }
    }

    protected void GetRandomAroundPos()
    {
        Vector2 nowPos = this.transform.position;
        int[] dx = new int[]{1, 1, 1, 0, 0, 0, -1, -1, -1};
        int[] dy = new int[]{ 1, 0, -1, 1, 0, -1, 1, 0, -1 };
        List<Vector2> tiles = new List<Vector2>();
        for(int i =0; i < dx.Length; i++)
        {
            if(mapScript.map[(int)nowPos.x+dx[i], (int)nowPos.y+dy[i]] == TileType.tile)
            {
                tiles.Add(new Vector2(nowPos.x + dx[i], nowPos.y + dy[i]));
            }
        }
        nextPos = tiles[Random.Range(0, tiles.Count)];
    }
    
}
