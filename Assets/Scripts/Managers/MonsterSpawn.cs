using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    int monsterSpawnPoint;
    public GameObject monsterPrefab;
    MapMake mapScript;
    List<GameObject> monsterList = new List<GameObject>();
    List<Vector2> tileList;
    // Start is called before the first frame update
    void Start()
    {
        mapScript = this.gameObject.transform.GetComponent<MapMake>();
        tileList = mapScript.tilePosList;
        MonsterCountInitiate(GameManager.instance.floor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MonsterCountInitiate(int floor)
    {
        monsterSpawnPoint = 5;
    }
    void MonsterInitiate()
    {
        for(int i = 0; i > monsterSpawnPoint; i++)
        {
            SetRandomIndex();
        }
    }
    void SetRandomIndex()
    {
        GameObject go;
        int randomIndex;
        randomIndex = Random.Range(0, mapScript.tilePosList.Count);
        go = Instantiate(monsterPrefab, mapScript.tilePosList[randomIndex], Quaternion.identity);
        mapScript.TileInfoChange(mapScript.tilePosList[randomIndex],TileType.monster,tileList,mapScript.monsterPosList);
        monsterList.Add(go);
    }
    void SetMonsterData()
    {

    }
}
