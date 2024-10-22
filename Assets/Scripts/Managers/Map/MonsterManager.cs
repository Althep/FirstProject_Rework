using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonsterWrapper
{
    public Vector2 pos;
    public EntityState myState;
}
[System.Serializable]
public class MonsterDataWrapper
{
    public List<MonsterWrapper> saveData = new List<MonsterWrapper>();
}
public class MonsterManager : MonoBehaviour
{
    int monsterSpawnPoint;
    public GameObject monsterPrefab;
    MapMake mapScript;
    public List<GameObject> monsterList = new List<GameObject>();
    List<Vector2> tileList;
    //public Dictionary<Vector2, EntityState> monsterSaveData = new Dictionary<Vector2, EntityState>();
    public List<MonsterWrapper> monsterSaveData = new List<MonsterWrapper>();
    public MonsterDataWrapper wrappedData = new MonsterDataWrapper();
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
        monsterSpawnPoint = 10;
    }
    public void MonsterInitiate()
    {
        monsterList.Clear();
        for (int i = 0; i < monsterSpawnPoint; i++)
        {
            int randomIndex;
            randomIndex = Random.Range(0, mapScript.tilePosList.Count);
            Vector2 randomPos = mapScript.tilePosList[randomIndex];
            if (mapScript.TileMap[randomPos] == TileType.tile)
            {
                GameObject go = Instantiate(monsterPrefab, mapScript.tilePosList[randomIndex], Quaternion.identity);
                mapScript.TileTypeChange(randomPos, TileType.monster);
                //mapScript.TileMap[mapScript.tilePosList[randomIndex]] = TileType.monster;
                //mapScript.miniMap.mapData[randomPos] = TileType.monster;
                monsterList.Add(go);
                go.name = i.ToString();
                mapScript.tilePosList.Remove(randomPos);
                //Debug.Log($"monster name : {go.name}");
            }
            else
            {
                mapScript.tilePosList.Remove(randomPos);
                Debug.Log("Monster Spqawn Tiletype Error");
            }
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
        mapScript.TileTypeChange(mapScript.tilePosList[randomIndex], TileType.monster);
        monsterList.Add(go);
    }
    public void LoadMonsterData()
    {
        monsterList.Clear();

        for(int i = 0; i < monsterSaveData.Count; i++)
        {
            GameObject go = Instantiate(monsterPrefab, monsterSaveData[i].pos, Quaternion.identity);
            go.transform.GetComponent<MonsterState>().myState = monsterSaveData[i].myState;
            monsterList.Add(go);
            go.name = go.transform.GetComponent<MonsterState>().name;
        }
        /*
        foreach (Vector2 keys in monsterSaveData.Keys)
        {
            GameObject go = Instantiate(monsterPrefab, keys, Quaternion.identity);
            go.transform.GetComponent<MonsterState>().myState = monsterSaveData[keys];
            monsterList.Add(go);
            go.name = go.transform.GetComponent<MonsterState>().name;
        }
        */
    }
    


}
