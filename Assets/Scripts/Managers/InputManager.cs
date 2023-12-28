using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum MoveState
{
    idle,
    move,
    attack
}

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    float HoriInput;
    float VirtyInput;
    float moveSpeed = 4f;

    public MoveState playerMoveState = MoveState.idle;


    GameObject playerObj;
    PlayerState playerState;
    MapMake mapScript;
    
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
        if (playerState.moveState == MoveState.idle)
        {
            OnkeyPlayerMove();
        }
    }
    public void OnkeyPlayerMove()
    {
        AxisInput();
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        /*
        if ( IsInSize() && (mapScript.map[nexty, nextx] == TileType.tile))
        {
            InputMoveKey();
        }

        else if(IsInSize() && (mapScript.map[nexty, nextx] == TileType.monster))
        {

        }
        */
        if (IsInSize())
        {
            switch (mapScript.map[nexty, nextx])
            {
                case TileType.tile:
                    InputMoveKey();
                    break;
                case TileType.wall:
                    Debug.Log("NExt Tile is Wall");
                    break;
                case TileType.door:
                    InputMoveKey();
                    break;
                case TileType.water:
                    break;
                case TileType.monster:
                    Debug.Log("NExt Tile is Monster");
                    break;
                case TileType.player:
                    Debug.Log("PlayerPosError");
                    break;
                default:
                    break;
            }
        }
    }
    bool IsInSize()
    {
        bool temp=false;
        int nexty = (int)(playerObj.transform.position.y + moveDirection.y);
        int nextx = (int)(playerObj.transform.position.x + moveDirection.x);
        if ((nextx < mapScript.xSize && nexty < mapScript.ySize) && (nextx>-1 && nexty>-1))
        {
            temp = true;
        }
        return temp;
    }
    void AxisInput()
    {
        HoriInput = Input.GetAxisRaw("Horizontal");
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
        playerState.moveState = MoveState.move;
        float maxDistance = SetMoveDistance(moveDirection);
        mapScript.TileInfoSwap(playerPos, playerPos + moveDirection, mapScript.playerPos, mapScript.tilePosList, TileType.player);
        while (Vector2.Distance(playerPos + moveDirection, playerObj.transform.position) >= 0.2f)
        {
            playerObj.transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
            if(Vector2.Distance(playerPos + moveDirection, playerObj.transform.position) >= maxDistance)
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
    public IEnumerator PlayerAttack()
    {
        playerState.moveState = MoveState.attack;
        //playerState.Attack();
        if (EventManager.Instance.OnPlayerMove != null)
        {
            EventManager.Instance.OnPlayerMove.Invoke();
        }
        playerState.moveState = MoveState.idle;
        yield return null;
    }

    float SetMoveDistance(Vector2 moveDirection)
    {
        float maxDixtance = 1.5f;
        if (Mathf.Abs(moveDirection.x)==Mathf.Abs(moveDirection.y))
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
