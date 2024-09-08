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


    public Dictionary<string, List<object>> equipMentsData;

    Dictionary<string, List<object>> _magicDatas;
    public Dictionary<string, List<object>> magicDatas { get { return _magicDatas; } }

    Dictionary<string, Dictionary<string, List<object>>> allItems = new Dictionary<string, Dictionary<string, List<object>>>();

    Dictionary<string, List<object>> EquipData = new Dictionary<string, List<object>>();

    Dictionary<int, List<int>> indexBytier = new Dictionary<int, List<int>>();
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
        monsterData = GameManager.instance.csvReader.CSVReade(path);
        
    }

    public void ReadItemByTiers()
    {
        string path = "EquipItemData";

        equipMentsData = GameManager.instance.csvReader.CSVReade(path);
        
        for(int i =0; i< equipMentsData["tier"].Count; i++)
        {
            int tier = Convert.ToInt32(equipMentsData["tier"][i]);
            int index = Convert.ToInt32(equipMentsData["index"][i]);
            if (indexBytier.ContainsKey(tier))
            {
                indexBytier[tier].Add(index);
            }
            else
            {
                
                indexBytier[tier] = new List<int>();
                indexBytier[tier].Add(index);
            }
        }

        foreach(var key in indexBytier.Keys)
        {
            Debug.Log($"key : {key}  value : {string.Join(",",indexBytier[key])}");
        }
        
        
    }





    public void ReadAllItems()
    {
        Array value = Enum.GetValues(typeof(EquipType));

        
        foreach (EquipType type in value)
        {
            string typeName = type.ToString();
            
            string path = "EquipItemData";
            //path += typeName;

            Dictionary<string, List<object>> itemDatas = GameManager.instance.csvReader.CSVReade(path);

            allItems.Add(typeName, itemDatas);
        }
        value = Enum.GetValues(typeof(ConsumType));
        foreach(ConsumType type in value)
        {
            string typeName = type.ToString();
            string path = "";
            path += typeName;

            Dictionary<string, List<object>> consumDatas = GameManager.instance.csvReader.CSVReade(path);

            allItems.Add(typeName, consumDatas);
        }

    }

    public void SetMonsterData(int index, MonsterState monsterState)
    {
        if (monsterData == null)
        {
            Debug.Log("Monster DataError");
            return;
        }

        monsterState.name = monsterData["name"][index].ToString();
        monsterState.index = Convert.ToInt32(monsterData["index"][index]);
        monsterState.exp = Convert.ToInt32(monsterData["exp"][index]);
        monsterState.myState.maxHp = Convert.ToInt32(monsterData["maxhp"][index]);
        monsterState.myState.currntHp = monsterState.myState.maxHp;
        monsterState.myState.damage = Convert.ToInt32(monsterData["damage"][index]);
        monsterState.myState.def = Convert.ToInt32(monsterData["def"][index]);
        monsterState.myState.base_MoveSpeed = Convert.ToInt32(monsterData["moveSpeed"][index]);
        monsterState.myState.base_AttackSpeed = Convert.ToInt32(monsterData["attackSpeed"][index]);
    }

    public void SetItempData(string itemType,int index,ItemBase itemData) 
    {
        //공통부분
        switch (itemData)
        {
            case  EquipItem equip:
                SetEquipData<EquipItem>(itemType, index, equip);
                break;
            case ConsumItem consum:
                SetConsumData<ConsumItem>(itemType,index,consum);
                break;
            default:
                break;
        }
        
    }
    public void SetEquipData<T>(string itemType, int index, T itemData) where T : EquipItem
    {
        string type = typeof(ItemData).ToString();
        itemData.name = allItems[type]["name"][index].ToString();
        itemData.index = Convert.ToInt32(allItems[type]["index"][index]);
        itemData.weight = Convert.ToInt32(allItems[type]["weight"][index]);
        switch (itemData)
        {
            case Weapon weapon:
                weapon.damage = Convert.ToInt32(allItems[type]["damage"][index]);
                weapon.attackSpeed = Convert.ToInt32(allItems[type]["attackSpeed"][index]);
                break;
            case Shield shield:
                shield.defense = Convert.ToInt32(allItems[type]["def"][index]);
                shield.blockRate = Convert.ToInt32(allItems[type]["blockRate"][index]);
                break;
            case Helm helm:
                helm.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            case Armor armor:
                armor.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            case Amulet amulet:
                amulet.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            case Glove glove:
                glove.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            case Shoose shoose:
                shoose.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            case Ring ring:
                ring.defense = Convert.ToInt32(allItems[type]["def"][index]);
                break;
            default:
                break;
        }

    }
    public void SetConsumData<T>(string itemType, int index, T itemData) where T : ConsumItem
    {
        string gear = typeof(ItemData).ToString();
        itemData.index = index; 
        switch (itemData)
        {
            case Potion potion:
                
                break;
            case Book book:
                break;
            case Scroll scroll:
                break;
            case Evoke evoke:
                break;
            case Food food:
                break;
            default:
                break;
        }

    }

    public void SetBasicItemData(ItemData iteam)
    {


    }
}
