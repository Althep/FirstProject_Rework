using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    int monsterSpawnPoint;
    public GameObject monsterPrefab;
    MapMake mapScript;
    List<GameObject> monsterList = new List<GameObject>();
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
        Debug.Log("AddCount");
        GameManager.instance.OnMapGenerate.AddListener(MonsterCountInitiate);
        Debug.Log("Added");
        Debug.Log("AddInitiate");
        GameManager.instance.OnMapGenerate.AddListener(MonsterInitiate);
        Debug.Log("Added");
    }

    void MonsterCountInitiate()
    {
        Debug.Log(4);
        monsterSpawnPoint = 5;
    }
    public void MonsterInitiate()
    {
        Debug.Log(3);
        for (int i = 0; i < monsterSpawnPoint; i++)
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
        mapScript.TileInfoChange(mapScript.tilePosList[randomIndex], TileType.monster, tileList, mapScript.monsterPosList);
        monsterList.Add(go);
    }
    void SetMonsterData()
    {

    }
}
