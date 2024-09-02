using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataManager
{

    //Dictionary<string, List<object>> _monsterDatas;
    public Dictionary<string, List<object>> monsterData; //{ get { return _monsterDatas; } }

    Dictionary<string, List<object>> _ConsumItemData;
    public Dictionary<string, List<object>> consumItemData { get { return _ConsumItemData; } }

    Dictionary<string, List<object>> _EquipMentsData;
    public Dictionary<string, List<object>> equipMentsData { get { return _EquipMentsData; } }

    Dictionary<string, List<object>> _magicDatas;
    public Dictionary<string, List<object>> magicDatas { get { return _magicDatas; } }

    Dictionary<string, Dictionary<string, List<object>>> allItems = new Dictionary<string, Dictionary<string, List<object>>>();
    /*
    public DataManager()
    {
        csvReader = GameManager.instance.csvReader;
        ReadMonsterData();
        ReadAllItems();
    }
    */
    public void ReadMonsterData()
    {
        string path = "MonsterData";
        //_monsterDatas = CSVReader.CSVReade(path);
        monsterData = GameManager.instance.csvReader.CSVReade(path);
        Debug.Log(monsterData);
    }

    void ReadAllItems()
    {
        Array value = Enum.GetValues(typeof(EquipType));

        
        foreach (EquipType type in value)
        {
            string name = type.ToString();
            string path = "";
            path += name;

            Dictionary<string, List<object>> itemDatas = GameManager.instance.csvReader.CSVReade(path);

            allItems.Add(name, itemDatas);
        }

        foreach(ConsumType type in value)
        {
            string name = type.ToString();
            string path = "";
            path += name;

            Dictionary<string, List<object>> consumDatas = GameManager.instance.csvReader.CSVReade(path);

            allItems.Add(name, consumDatas);
        }

    }

    public void SetMonsterData(int index, MonsterState monsterState)
    {
        monsterState.name = monsterData["name"][index].ToString();
        Debug.Log($"name : { monsterData["name"][index]}");
        monsterState.index = Convert.ToInt32(monsterData["index"][index]);
        Debug.Log($"index : {monsterData["index"][index]}");
        monsterState.exp = Convert.ToInt32(monsterData["exp"][index]);
        Debug.Log($"exp : {monsterData["exp"][index]}");
        monsterState.myState.maxHp = Convert.ToInt32(monsterData["maxhp"][index]);
        Debug.Log($"maxphp : {monsterData["maxhp"][index]}");
        monsterState.myState.currntHp = monsterState.myState.maxHp;
        monsterState.myState.damage = Convert.ToInt32(monsterData["damage"][index]);
        Debug.Log($"damage : {monsterData["damage"][index]}");
        monsterState.myState.def = Convert.ToInt32(monsterData["def"][index]);
        Debug.Log($"def : {monsterData["def"][index]}");
        monsterState.myState.base_MoveSpeed = Convert.ToInt32(monsterData["moveSpeed"][index]);
        Debug.Log($"ms : {monsterData["moveSpeed"][index]}");
        monsterState.myState.base_AttackSpeed = Convert.ToInt32(monsterData["attackSpeed"][index]);
        Debug.Log($"as : {monsterData["attackSpeed"][index]}");
    }

}
