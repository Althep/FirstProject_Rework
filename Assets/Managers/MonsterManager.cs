using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    int monsterSpawnPoint;
    public GameObject monsterPrefab;
    MapMake mapScript;
    public List<GameObject> monsterList = new List<GameObject>();
    List<Vector2> tileList;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    void Start()
    {
        mapScript = this.gameObject.transform.GetComponent<MapMake>();
        tileList = mapScript.tilePosList;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddMonsterSpawnFunction()
    {
        MonsterCountInitiate();
        MonsterInitiate();
    }

    void MonsterCountInitiate()
    {
        monsterSpawnPoint = 1;
    }
    public void MonsterInitiate()
    {
        for (int i = 0; i < monsterSpawnPoint; i++)
        {
            GameObject go;
            int randomIndex;
            randomIndex = Random.Range(0, mapScript.tilePosList.Count);
            go = Instantiate(monsterPrefab, mapScript.tilePosList[randomIndex], Quaternion.identity);
            mapScript.TileInfoChange(mapScript.tilePosList[randomIndex], TileType.monster, tileList, mapScript.monsterPosList);
            monsterList.Add(go);
            go.name = i.ToString();
            //SetRandomIndex();
        }
        //TurnManager.instance.monsterStateList.Add()
    }
    void SetRandomIndex()
    {
        GameObject go;
        int randomIndex;
        randomIndex = Random.Range(0, mapScript.tilePosList.Count);
        go = Instantiate(monsterPrefab, mapScript.tilePosList[randomIndex], Quaternion.identity);
        mapScript.TileInfoChange(mapScript.tilePosList[randomIndex], TileType.monster, tileList, mapScript.monsterPosList);
        monsterList.Add(go);
    }
    void SetMonsterData()
    {

    }
    
}
