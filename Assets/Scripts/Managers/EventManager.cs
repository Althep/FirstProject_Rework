using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    MapMake mapMakeScript;
    GameManager gameManager;
    MonsterManager monsterManager;
    private void Start()
    {
        mapMakeScript = this.gameObject.transform.GetComponent<MapMake>();
        gameManager = GameManager.instance;
        monsterManager = this.gameObject.transform.GetComponent<MonsterManager>();
        AddMapGenerateFunction();
    }
    void AddMapGenerateFunction()
    {
        if (GameManager.instance != null)
        {
            gameManager.OnMapGenerate.AddListener(mapMakeScript.MapGenerate);
            gameManager.OnMapGenerate.AddListener(monsterManager.AddMonsterSpawnFunction);
            if (monsterManager == null)
            {
                Debug.Log("Null1");
            }
            gameManager.InstantiatePlayerObj();
            gameManager.OnMapGenerate.AddListener(gameManager.SetPlayerNextFloor);
            GameManager.instance.OnMapGenerate.Invoke();
        }
        else
        {
            Debug.Log("NullError");
        }
    }
}
