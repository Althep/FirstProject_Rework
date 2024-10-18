using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;

[System.Serializable]
public class ItemManager
{
    [JsonProperty]
    public DataManager dataManager;
    public Dictionary<string, EquipItem> EquipScripts = new Dictionary<string, EquipItem>();
    public Dictionary<string, ConsumItem> ConsumScripts = new Dictionary<string, ConsumItem>();
    public Dictionary<Vector2, Item> ItemPosList = new Dictionary<Vector2, Item>();
    public Dictionary<Vector2, ItemBase> itemSave = new Dictionary<Vector2, ItemBase>();
    //public List<Vector2> ItemPosList = new List<Vector2>();
    public int itemSpawnCount;
    T GetRandomType<T>() where T : Enum
    {
        Array value = Enum.GetValues(typeof(T));
        return (T)value.GetValue(UnityEngine.Random.Range(0, value.Length));
    }

    public void MakeItems()
    {
        SetItemSpawnCount();
        for(int i = 0; i<itemSpawnCount; i++)
        {
            ItemFactiry();
        }

    }

    public void SetItemSpawnCount()
    {
        itemSpawnCount = 10;
    }

    public void ItemFactiry()
    {
        var type = GetRandomType<ItemType>();
        int index = 0;
        string itemType;
        int maxRate;
        int rate;
        int tier;
        int randomPosIndex;
        Vector2 randomPos;
        randomPosIndex = UnityEngine.Random.Range(0, GameManager.instance.mapScript.tilePosList.Count);
        if (randomPosIndex > 0)
        {
            randomPos = GameManager.instance.mapScript.tilePosList[randomPosIndex];
            Debug.Log($"RandomPos Index : {randomPosIndex}, RandomPos {randomPos.x}, {randomPos.y}");
            
        }
        else
        {
            Debug.Log("itemFactory Index Error");
            return;
        }
        if(dataManager== null)
        {
            dataManager = GameManager.instance.dataManager;
        }
        if(dataManager.consumData == null)
        {
            dataManager.consumData = GameManager.instance.csvReader.Read("ConsumItemData");
        }
        if(dataManager.equipMentsData == null)
        {
            dataManager.equipMentsData = GameManager.instance.csvReader.Read("EquipItemData");
        }

        switch (type)
        {
            case ItemType.Consumable:
                
                List<int>rates = GameManager.instance.dataManager.tierRates[type.ToString()];
                maxRate = rates[rates.Count-1];//마지막 티어까지의 확률중 마지막값(마지막 티어의 값)을 가져옴
                rate = UnityEngine.Random.Range(0,maxRate);
                tier = GetTierByRate(rate,rates);
                index = UnityEngine.Random.Range(0,GameManager.instance.dataManager.consymIndexBytier[tier].Count-1);
                Debug.Log($"Before index : {index}");
                index = GameManager.instance.dataManager.consymIndexBytier[tier][index];
                Debug.Log($"After index {index}");
                Debug.Log(dataManager.consumData[index]);
                itemType = (dataManager.consumData[index]["type"]).ToString();
                Debug.Log($"itemType : {itemType}");
                var consumscript = ConsumScripts[itemType];
                ConsumType consumType;

                if(Enum.TryParse(itemType, out consumType))
                {
                    GameObject go = new GameObject();
                    Consumable consum = go.AddComponent<Consumable>();
                    consum.myInfo = consumscript.Clone(consumType);
                    dataManager.SetItempData(index,consum.myInfo);
                    go.name = consum.myInfo.name;
                    AddItemComponents(go);
                    Debug.Log($"Consum ItemMake Succese item name : {go.name}");
                    go.transform.position = new Vector3(randomPos.x,randomPos.y,-1);
                    ItemPosList.Add(randomPos, consum);
                    SetItemImage(go, go.name);
                }
                else
                {
                    Debug.Log("Type Parse Error");
                }

                break;
            case ItemType.Equipment:
                rates = GameManager.instance.dataManager.tierRates[type.ToString()];
                maxRate = rates[rates.Count-1];
                rate = UnityEngine.Random.Range(0, maxRate);
                tier = GetTierByRate(rate, rates);
                index = UnityEngine.Random.Range(0, GameManager.instance.dataManager.equipIndexBytier[tier].Count-1);
                Debug.Log($"Before index : {index}");
                index = GameManager.instance.dataManager.equipIndexBytier[tier][index];
                Debug.Log($"After index {index}");
                itemType = (dataManager.equipMentsData[index]["type"]).ToString();
                Debug.Log($"itemType : {itemType}");
                var equipscript = EquipScripts[itemType];
                EquipType equipType;

                if (Enum.TryParse(itemType, out equipType))
                {
                    GameObject go = new GameObject();
                    EquipMents equip = go.AddComponent<EquipMents>();
                    equip.myInfo = equipscript.Clone(equipType);
                    dataManager.SetItempData(index, equip.myInfo);
                    go.name = equip.myInfo.name;
                    AddItemComponents(go);
                    Debug.Log($"equip ItemMake Succese{go.name}");
                    go.transform.position = new Vector3(randomPos.x, randomPos.y, -1);
                    ItemPosList.Add(randomPos, equip);
                    SetItemImage(go, go.name);
                }
                else
                {
                    Debug.Log("Type Parse Error");
                }
                
                break;
            default:
                break;
        }
        

    }

    public void AddItemComponents(GameObject go)
    {
        SpriteRenderer sprender;
        BoxCollider2D collider;
        sprender = go.AddComponent<SpriteRenderer>();
        collider = go.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1, 1);
        go.transform.tag = "Item";
        go.AddComponent<FogOfWar>();
        go.gameObject.layer = 6;
    }


    public int GetTierByRate(int rate,List<int> tiers)
    {
        int tier = 1;
        for(int i = 1; i < tiers.Count; i++)
        {
            if (rate >= tiers[i] && i>tier)
            {
                tier = i;
            }
            else
            {
                Debug.Log($"tier : {tier}");
                break;
            }
        }
        if(tier == 0)
        {
            Debug.Log("tier is 0 tier Error!!");
        }
        return tier;
    }

    public void SetItemImage(GameObject go,string name)
    {
        SpriteRenderer sprite = go.transform.GetComponent<SpriteRenderer>();
        sprite = go.transform.GetComponent<SpriteRenderer>();
        sprite.sprite = GameManager.instance.baseSprite;
    }
    /*
    public void ItemFactory()
    {
        GameObject go = new GameObject();
        var (itemType,consum, equip) = GetItemKind();

        

        if(dataManager == null)
        {
            dataManager = GameManager.instance.dataManager;
        }
        if(itemType == ItemType.Equipment && consum==null)
        {
            EquipType type = GetRandomEquipType();
            string key = type.ToString();
            int index = 0;
            EquipMents itemInfo = go.AddComponent<EquipMents>();
            itemInfo.myInfo = EquipScripts[key].Clone(type);
            dataManager.SetItempData(key, index, itemInfo.myInfo);
            Debug.Log(go.transform.GetComponent<EquipMents>().myInfo._equipType);
        }
        else if (itemType == ItemType.Consumable && equip == null)
        {
            ConsumType type = GetRandomConsumType();
            string key = type.ToString();
            int index = 0;
            Consumable itemInfo = go.AddComponent<Consumable>();
            itemInfo.myInfo = ConsumScripts[key].Clone(type);
            dataManager.SetItempData(key, index, itemInfo.myInfo);
            Debug.Log(go.transform.GetComponent<Consumable>().myInfo._consumType);
        }
        else
        {
            Debug.Log("ItemType Error");
        }

    }
    */
    public (ItemType? itemYype, ConsumType? consumableType , EquipType? equipType) GetItemKind()
    {
        ItemType? itemType = GetRandomType<ItemType>();
        ConsumType? consumType = null;
        EquipType? equipType = null;
        switch (itemType)
        {
            case ItemType.Consumable:
                consumType = GetRandomConsumType();
                break;
            case ItemType.Equipment:
                equipType = GetRandomEquipType();
                break;
        }
        return (itemType,consumType, equipType);
    }

    public ConsumType GetRandomConsumType()
    {
        return GetRandomType<ConsumType>();
    }

    public EquipType GetRandomEquipType()
    {
        return GetRandomType<EquipType>(); 
    }

    public void InitiateItem()
    {
        Debug.Log("InitiateItem");
        Weapon weapon = new Weapon();
        Shield shiled = new Shield();
        Helm helm = new Helm();
        Armor armor= new Armor();
        Amulet amulet = new Amulet();
        Glove glove = new Glove();
        Shoose shoose = new Shoose();
        Ring ring = new Ring();
        
        Potion potion = new Potion();
        Book Book = new Book();
        Scroll scroll = new Scroll();
        Evoke evoke = new Evoke();
        Food food = new Food();


    }
    public void LoadItemData()
    {
        ItemPosList.Clear();

        foreach (Vector2 keys in itemSave.Keys)
        {
            GameObject go = new GameObject();
            Item item = go.AddComponent<Item>();
            AddItemComponents(go);
            item.myInfo = itemSave[keys];
            go.transform.position = keys;
            go.layer = GameManager.instance.mapScript.objLayers[keys];
            ItemPosList.Add(keys,item);
            GameManager.instance.mapScript.mapObjects.Add(go);
        }
    }
}
