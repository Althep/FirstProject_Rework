using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    int monsterSpawnPoint;
    GameObject monsterPrefab;
    MapMake mapScript;
    List<GameObject> monsterList = new List<GameObject>();
    List<Vector2> tileList;
    // Start is called before the first frame update
    void Start()
    {
        mapScript = this.gameObject.transform.GetComponent<MapMake>();
        tileList = mapScript.tilePosList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MonsterCountInitiate()
    {
        monsterSpawnPoint = 5;
    }
    void MonsterInitiate()
    {

    }
    void SetRandomIndex()
    {
        GameObject go;
        int randomIndex;
        randomIndex = Random.RandomRange(0, mapScript.tilePosList.Count);
        go = Instantiate(monsterPrefab, mapScript.tilePosList[randomIndex], Quaternion.identity);
        mapScript.TileInfoChange(mapScript.tilePosList[randomIndex],TileType.monster,tileList,mapScript.mosnterPosList);
        monsterList.Add(go);
    }
    void SetMonsterData()
    {

    }
}
