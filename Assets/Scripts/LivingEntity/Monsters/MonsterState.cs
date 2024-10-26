using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
enum MosnsterType
{
    anumal,
    humanoid
}
public class MonsterState : LivingEntity
{

    
    [JsonIgnore]
    public Vector2 oldPlayerPos;
    [JsonIgnore]
    public Vector2 nextPos;
    [JsonIgnore]
    public GameObject target;
    [JsonIgnore]
    public List<Node> path;
    [JsonIgnore]
    public MonsterActSate myActState;
    [JsonIgnore]
    PlayerState playerState;
    

    private void Start()
    {
        myActState = this.transform.GetComponent<Rest>();
        target = GameManager.instance.playerObj;
        myState.base_MoveSpeed = 10;
        myState.base_AttackSpeed = 10;
        myState.maxHp = 10;
        myState.currntHp = myState.maxHp;
        myState.attackRange = 1.42f;
        SetSpeed();
        playerState = GameManager.instance.playerObj.transform.GetComponent<PlayerState>();
        EventManager.Instance.OnPlayerMove.AddListener(OnPlayerMove);
    }

    private void Update()
    {
        //ShowState();
        myState.distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position).ToString();
        CanvasSetActive();
        if (myState.leftTurnPoint != 0)
        {
            Debug.Log(myState.leftTurnPoint);
        }
        
    }

    void OnPlayerMove()
    {
        TurnPointAdd();
        SetMoveState();
        TurnCalculate();
        GetNextPos();
        TurnAct();
    }
    void SetMoveState()
    {
        if (myActState is Chase && Vector2.Distance(this.gameObject.transform.position, target.transform.position) <= myState.attackRange)
        {
            moveState = MoveState.attack;
        }
        else if (myActState is Sleep)
        {
            moveState = MoveState.idle;
        }
        else
        {
            moveState = MoveState.move;
        }
    }
    void GetNextPos()
    {
        myActState.SetNextPos();
    }
    void TurnAct()
    {
        while (myState.turn > 0)
        {
            myActState.TurnAct();
            Debug.Log(myState.turn);
            myState.turn--;
        }
            
        
    }
    public void TurnPointAdd()
    {
        switch (playerState.moveState)
        {
            case MoveState.idle:
                Debug.Log("PlayerMoveStateError");
                break;
            case MoveState.move:
                myState.leftTurnPoint += playerState.myState.moveSpeed;
                break;
            case MoveState.attack:
                myState.leftTurnPoint += playerState.myState.attackSpeed;
                break;
            default:
                break;
        }
    }

    public void TurnCalculate()
    {
        switch (moveState)
        {
            case MoveState.idle:
                Debug.Log("Idle!");
                break;
            case MoveState.move:
                myState.turn = myState.leftTurnPoint / myState.moveSpeed;
                myState.leftTurnPoint %= myState.moveSpeed;
                break;
            case MoveState.attack:
                myState.turn = myState.leftTurnPoint / myState.attackSpeed;
                myState.leftTurnPoint %= myState.attackSpeed;
                break;
            default:
                break;
        }
    }

    
    protected override void IsDead()
    {
        if (myState.currntHp <= 0)
        {
            myState.isDead = true;
            EventManager.Instance.OnPlayerMove.RemoveListener(this.OnPlayerMove);
        }
        if (myState.isDead)
        {
            GameManager.instance.monsterManager.monsterList.Remove(this.gameObject);
            GameManager.instance.mapScript.TileMap[this.gameObject.transform.position] = GameManager.instance.mapScript.OriginMap[this.gameObject.transform.position];
            Destroy(this.gameObject);
        }
    }


    void ShowState()
    {
        switch (myActState)
        {

            case Chase:
                myState.State = "Chase";
                break;
            case Sleep:
                myState.State = "Sleep";
                break;
            case Searching:
                myState.State = "Searching";
                break;
            case Rest:
                myState.State = "Rest";
                break;
            default:
                break;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && myActState is Sleep)
        {
            myActState.CollideStart();
        }
        else if (collision.transform.tag == "Player")
        {
            myActState = this.transform.GetComponent<Chase>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && myActState is Chase)
        {
            myActState = this.transform.GetComponent<Searching>();
            oldPlayerPos = GameManager.instance.playerObj.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.transform.tag == "Player" && myActState is Rest) && !(myActState is Chase))
        {
            myActState = this.transform.GetComponent<Chase>();
        }
    }

}
