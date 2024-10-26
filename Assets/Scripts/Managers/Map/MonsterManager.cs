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
    //public Dictionary<Vector2, GameObject> monsterPos = new Dictionary<Vector2, GameObject>();

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
            int maxRate;
            int rate;
            int tier;
            int index;
            if (mapScript.TileMap[randomPos] == TileType.tile)
            {
                GameObject go = Instantiate(monsterPrefab,new Vector3( mapScript.tilePosList[randomIndex].x, mapScript.tilePosList[randomIndex].y,-1), Quaternion.identity);
                mapScript.TileTypeChange(randomPos, TileType.monster);
                monsterList.Add(go);
                go.name = i.ToString();
                mapScript.tilePosList.Remove(randomPos);
                MonsterState monsterState = go.transform.GetComponent<MonsterState>();
                List<int> rates = GameManager.instance.dataManager.tierRates[(monsterState.GetType()).ToString()];
                maxRate = rates[rates.Count - 1];
                rate = UnityEngine.Random.Range(0, maxRate);
                tier = GetTierByRate(rate, rates);
                index = UnityEngine.Random.Range(0, GameManager.instance.dataManager.monsterIndexBytier[tier].Count - 1);
                index = GameManager.instance.dataManager.monsterIndexBytier[tier][index];
                Debug.Log(GameManager.instance.dataManager.monsterData);
                GameManager.instance.dataManager.SetMonsterData(index, monsterState);
                go.name = go.transform.GetComponent<MonsterState>().myState.name;
                SetMonsterImage(go);
                
                //monsterPos.Add(randomPos, go);
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
            go.name = go.transform.GetComponent<MonsterState>().myState.name;
            SetMonsterImage(go);
            //monsterPos.Add(monsterSaveData[i].pos, go);
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
    public int GetTierByRate(int rate, List<int> tiers)//Todo
    {
        int tier = 1;
        for (int i = 1; i < tiers.Count; i++)
        {
            if (rate >= tiers[i] && i > tier)
            {
                tier = i;
            }
            else
            {
                break;
            }
        }
        if (tier == 0)
        {
            Debug.Log("tier is 0 tier Error!!");
        }
        return tier;
    }
    public void SetMonsterImage(GameObject go)
    {
        MonsterState monsterState = go.transform.GetComponent<MonsterState>();
        Texture2D texture = GameManager.instance.dataManager.GetMonsterImage(monsterState.myState.name);
        if (texture != null)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            go.transform.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

}
