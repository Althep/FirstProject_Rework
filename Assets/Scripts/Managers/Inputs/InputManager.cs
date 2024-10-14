using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum MoveState
{
    idle,
    move,
    attack,
    cast,
    threw,
    uiOpen
}

public class InputManager : MonoBehaviour
{
    //인풋 설정에서 Horizental Virtical 옵션을 바꿔 넘버패드로 키입력을 받게 바꿈
    // Start is called before the first frame update
    float HoriInput;
    float VirtyInput;
    float moveSpeed = 4f;

    //public MoveState playerMoveState = MoveState.idle;
    bool CanInput;

    GameObject playerObj;
    GameObject aimObj;
    GameObject[] targets;

    PlayerState playerState;
    MapMake mapScript;
    UIManager uiManager;


    Vector3 moveDirection;
    Vector3 playerPos;

    KeyCode[] UIKeyCodes;

    public Dictionary<string, KeyCode> UInameToKeyCode = new Dictionary<string, KeyCode>();
    public Dictionary<KeyCode, GameObject> UIKeyCodeToObj = new Dictionary<KeyCode, GameObject>();

    KeyCode inputKey;




    void Start()
    {
        playerObj = GameManager.instance.playerObj;
        mapScript = GameManager.instance.gameObject.transform.GetComponent<MapMake>();
        uiManager = GameManager.instance.UIManager;
        playerState = playerObj.transform.GetComponent<PlayerState>();
        aimObj = playerObj.transform.GetChild(1).gameObject;
        
    }

    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            playerState.moveState = MoveState.cast;
        }

        MoveStateOnKey();

        foreach(KeyCode key in UIKeyCodeToObj.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log(key);
                UIObjectOnOff(key);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.RemoveUIListUpdate();
        }

    }
    #region MoveControll
    void MoveStateOnKey()
    {
        switch (playerState.moveState)
        {
            case MoveState.idle:
                OnkeyPlayerMove();
                break;
            case MoveState.move:
                break;
            case MoveState.attack:
                break;
            case MoveState.cast:
                OnKeyPlayerAim();
                break;
            case MoveState.threw:
                OnKeyPlayerAim();
                break;
            case MoveState.uiOpen:
                //UI 오브젝트가 열렸을경우 방향키 할당, 이동함수
                break;

            default:
                break;
        }
    }
   
    public void OnkeyPlayerMove()
    {
        AxisInput();
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        Vector2 next = new Vector2(nextx, nexty);
        
        if (IsInSize())
        {
            Debug.Log($"Next Pos Data : {mapScript.TileMap[next]}");
            switch (mapScript.TileMap[next])
            {
                case TileType.tile:
                    InputMoveKey();
                    //Debug.Log("MovableTile");
                    break;
                case TileType.wall:
                    //Debug.Log("Wall");
                    break;
                case TileType.door:
                    InputMoveKey();
                    break;
                case TileType.stair:
                    //Debug.Log("stair");
                    break;
                case TileType.monster:
                    MakeCollider(next);
                    //MoveState Attack으로 바꾼 후 Attack함수에서 실행 후 idle로 바꿀 필요 있음
                    break;
                case TileType.player:
                    break;
                default:
                    break;
            }
        }
    }
    public void OnKeyPlayerAim()
    {
        aimObj.SetActive(true);
        AxisInput();
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        Vector2 next = new Vector2(nextx, nexty);
        if (IsInSize())
        {
            aimObj.transform.position = next;
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
            playerState.moveState = MoveState.move;
            StartCoroutine("Moving");
        }
    }
    public IEnumerator Moving()
    {
        playerPos = playerObj.transform.position;
        float maxDistance = SetMoveDistance(moveDirection);
        mapScript.EntityMove(playerPos, playerPos + moveDirection,TileType.player);
        playerState.moveState = MoveState.move;
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
        if (EventManager.Instance.OnPlayerMove != null)
        {
            EventManager.Instance.OnPlayerMove.Invoke();
        }
        StartCoroutine("WaitInputTime");

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
                collider.gameObject.TryGetComponent<LivingEntity>(out entity);
                PlayerAttack(entity, playerState.myState.damage);
            }
        }
    }
    #endregion

    #region UIControll

    void UIObjectOnOff(KeyCode key)
    {
        GameObject uiObj = UIKeyCodeToObj[key];
        Debug.Log(UIKeyCodeToObj[key].name);
        if(uiObj.activeSelf)
        {
            uiObj.SetActive(false);
            playerState.moveState = MoveState.idle;
            uiManager.RemoveUIListUpdate();
        }
        else
        {
            uiObj.SetActive(true);
            playerState.moveState = MoveState.uiOpen;
            uiManager.AddUIListUpDate(uiObj);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    #endregion

}
