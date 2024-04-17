using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn;
    public static TurnManager instance;
    PlayerState playerState;
    MonsterManager monsterManager;
    public List<MonsterState> monsterStateList = new List<MonsterState>();
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        Initiate();
        EventManager.Instance.OnPlayerMove.AddListener(PlayerAct);
    }

    void Initiate()
    {
        SetInstance();
        playerState = GameManager.instance.playerObj.transform.GetComponent<PlayerState>();
        monsterManager = GameManager.instance.gameObject.transform.GetComponent<MonsterManager>();
       
    }
    void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void PlayerAct()
    {
        turn++;
    }

}
