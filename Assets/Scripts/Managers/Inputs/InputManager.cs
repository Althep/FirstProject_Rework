using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum MoveState
{
    idle,
    move,
    attack,
    cast
}

public class InputManager : MonoBehaviour
{
    //인풋 설정에서 Horizental Virtical 옵션을 바꿔 넘버패드로 키입력을 받게 바꿈
    // Start is called before the first frame update
    float HoriInput;
    float VirtyInput;
    float moveSpeed = 4f;

    public MoveState playerMoveState = MoveState.idle;
    bool CanInput;

    GameObject playerObj;
    GameObject[] targets;

    PlayerState playerState;
    MapMake mapScript;

    KeyCode lastInput;
    KeyCode currentKey;
    Vector3 moveDirection;
    Vector3 playerPos;

    void Start()
    {
        playerObj = GameManager.instance.playerObj;
        mapScript = GameManager.instance.gameObject.transform.GetComponent<MapMake>();
        playerState = playerObj.transform.GetComponent<PlayerState>();

    }
    private void FixedUpdate()
    {
        if(playerMoveState == MoveState.idle&&Input.anyKeyDown)
        {
            OnkeyPlayerMove();
        }
        


    }
    /*
    public bool KeyCodeCompare()
    {
        // 현재 입력된 키와 마지막으로 입력된 키 비교
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode == lastInput && lastInputTime+0.5f<Time.time)
                    {
                        //Debug.Log("현재 입력된 키는 마지막으로 입력된 키와 같습니다.");
                        isSame = true;
                    }
                    else
                    {
                        Debug.Log("현재 입력된 키는 마지막으로 입력된 키와 다릅니다.");
                        isSame = false;
                        lastInputTime = Time.time;
                        lastInput = keyCode;
                    }
                }
            }
        }
        return isSame;
    }
    */
    public void OnkeyPlayerMove()
    {
        AxisInput();
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        Vector2 next = new Vector2(nextx, nexty);
        playerState.moveState = MoveState.move;
        if (IsInSize())
        {
            switch (mapScript.map[nexty, nextx])
            {
                case TileType.tile:
                    InputMoveKey();
                    break;
                case TileType.wall:
                    break;
                case TileType.door:
                    InputMoveKey();
                    break;
                case TileType.stair:
                    break;
                case TileType.monster:
                    MakeCollider(next);
                    //MoveState Attack으로 바꾼 후 Attack함수에서 실행 후 idle로 바꿀 필요 있음
                    break;
                case TileType.player:
                    //Debug.Log("PlayerPosError");
                    break;
                default:
                    break;
            }
        }
    }
    bool IsInSize()
    {
        bool temp = false;
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        if ((nextx < mapScript.xSize && nexty < mapScript.ySize) && (nextx > -1 && nexty > -1))
        {
            temp = true;
        }
        return temp;
    }

    void AxisInput()
    {
        HoriInput = Input.GetAxisRaw("Horizontal"); //<- ->
        VirtyInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(HoriInput, VirtyInput, 0);
    }

    public void InputMoveKey()
    {
        if (HoriInput != 0 || VirtyInput != 0)
        {
            StartCoroutine("Moving");
        }
    }
    public IEnumerator Moving()
    {
        playerPos = playerObj.transform.position;
        float maxDistance = SetMoveDistance(moveDirection);
        mapScript.TileInfoSwap(playerPos, playerPos + moveDirection, mapScript.playerPos, mapScript.tilePosList, TileType.player);

        while (Vector2.Distance(playerPos + moveDirection, playerObj.transform.position) >= 0.2f)
        {
            playerObj.transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
            if (Vector2.Distance(playerPos + moveDirection, playerObj.transform.position) >= maxDistance)
            {
                Debug.Log("Break!");
                break;
            }
            yield return null;
        }
        playerObj.transform.position = new Vector2((playerPos + moveDirection).x, (playerPos + moveDirection).y);
        if (EventManager.Instance.OnPlayerMove != null)
        {
            EventManager.Instance.OnPlayerMove.Invoke();
        }
        playerState.moveState = MoveState.idle;
    }
    public void PlayerAttack(LivingEntity target, int damage)
    {
        playerState.moveState = MoveState.attack;
        playerState.Attack(target);
        Debug.Log("111111");
        if (EventManager.Instance.OnPlayerMove != null)
        {
            EventManager.Instance.OnPlayerMove.Invoke();
        }
        StartCoroutine("WaitInputTime");
        Debug.Log(TurnManager.instance.turn);
        playerState.moveState = MoveState.idle;

    }
    IEnumerator WaitInputTime()
    {
        yield return new WaitForSeconds(1f);
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
    void MakeCollider(Vector2 next)
    {
        Collider2D[] col;
        LivingEntity entity;
        col = Physics2D.OverlapCircleAll(next, 0.1f); // 0.1f크기의 콜라이더 생성 해 다음위치의 오브젝트들 잡아옴
        foreach (Collider2D collider in col)
        {
            if (collider.CompareTag("Monster"))
            {
                Debug.Log("Find Target");
                collider.gameObject.TryGetComponent<LivingEntity>(out entity);
                PlayerAttack(entity, playerState.myState.damage);
            }
        }
    }
}
