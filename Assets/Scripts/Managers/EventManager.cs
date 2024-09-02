using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    MapMake mapMakeScript;
    GameManager gameManager;
    MonsterManager monsterManager;
    public static EventManager Instance;

    public UnityEvent OnPlayerMove;
    public UnityEvent OnMapGenerate;


    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        mapMakeScript = this.gameObject.transform.GetComponent<MapMake>();
        gameManager = GameManager.instance;
        monsterManager = this.gameObject.transform.GetComponent<MonsterManager>();
        gameManager.InstantiatePlayerObj();
        AddMapGenerateFunction();
        GameManager.instance.OnMapGenerate.Invoke();
        TurnManager.instance.turn++;
    }

    void AddMapGenerateFunction()
    {
        if (GameManager.instance != null)
        {
            gameManager.OnMapGenerate.AddListener(mapMakeScript.MapGenerate);
            gameManager.dataManager.ReadMonsterData();
            gameManager.OnMapGenerate.AddListener(monsterManager.AddMonsterSpawnFunction);
            gameManager.OnMapGenerate.AddListener(gameManager.SetPlayerNextFloor);
        }
        else
        {
            Debug.Log("GameManager NullError");
        }
    }
    void SetInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
