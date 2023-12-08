using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterState : LivingEntity
{
    public GameObject target;
    public List<Node> path;
    public int damage;
    public int leftTurnPoint;
    public MonsterActSate myActState;
    public float attackRange=1.5f;
    public int awakingRate;
    public string State;
    PlayerState playerState;
    public string distance;
    private void Start()
    {
        myActState = this.transform.GetComponent<Rest>();
        target = GameManager.instance.playerObj;
        base_MoveSpeed = 10;
        base_AttackSpeed = 10;
        SetSpeed();
        playerState = GameManager.instance.playerObj.transform.GetComponent<PlayerState>(); 
        EventManager.Instance.OnPlayerMove.AddListener(OnPlayerMove);
    }

    private void Update()
    {
        ShowState();
        distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position).ToString();
    }

    void OnPlayerMove()
    {
        TurnPointAdd();
        SetMoveState();
        TurnCalculate();
        MonsterAct();
        GetNextPos();
        TurnAct();
    }
    void SetMoveState()
    {
        if(myActState is Chase && Vector2.Distance(this.gameObject.transform.position,target.transform.position)<=attackRange)
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
        this.myActState.SetNextPos();
    }
    void TurnAct()
    {
        this.myActState.TurnAct();
    }
    public void TurnPointAdd()
    {
        switch (playerState.moveState)
        {
            case MoveState.idle:
                Debug.Log("PlayerMoveStateError");
                break;
            case MoveState.move:
                leftTurnPoint += playerState.moveSpeed;
                break;
            case MoveState.attack:
                leftTurnPoint += playerState.attackSpeed;
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
                turn = leftTurnPoint / moveSpeed;
                leftTurnPoint %= moveSpeed;
                break;
            case MoveState.attack:
                turn = leftTurnPoint / attackSpeed;
                leftTurnPoint %= attackSpeed;
                break;
            default:
                break;
        }
    }

    public void MonsterAct()
    {
        while (turn>0)
        {
            switch (moveState)
            {
                case MoveState.idle:
                    Debug.Log("DoNothing");
                    break;
                case MoveState.move:
                    Debug.Log("DoMove");
                    break;
                case MoveState.attack:
                    Debug.Log("DoAttack");
                    break;
                default:
                    break;
            }
            turn--;
        }
        //moveState = MoveState.idle;
    }
    protected override void IsDead()
    {
        if (currntHp <= 0)
        {
            isDead = true;
            EventManager.Instance.OnPlayerMove.RemoveListener(this.OnPlayerMove);
        }
    }


    void ShowState()
    {
        switch (myActState)
        {

            case Chase:
                State = "Chase";
                break;
            case Sleep:
                State = "Sleep";
                break;
            case Searching:
                State = "Searching";
                break;
            case Rest:
                State = "Rest";
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
