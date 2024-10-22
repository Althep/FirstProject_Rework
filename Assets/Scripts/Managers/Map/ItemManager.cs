using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
/*
[System.Serializable]
public class ItemWrapper
{
    public int posx;
    public int posy;
    public ItemBase itemBase;
    public string itemType;
    public ItemWrapper(Vector2 positon, ItemBase item)
    {
        posx =(int) positon.x;
        posy = (int)positon.y;
        itemBase = item;
        itemType = item.GetType().AssemblyQualifiedName;
    }
}*/
[System.Serializable]
public class ConsumWrapper//래핑함수에 생성자가 있을경우 역직렬화에 문제가 생길 수 있음
{
    public int posx;
    public int posy;
    public ConsumItem itemBase;
    public string itemType;
    

}
[System.Serializable]
public class EquipWrapper
{
    public int posx;
    public int posy;
    public EquipItem itemBase;
    public string itemType;
    
}
[System.Serializable]
public class ItemDataWrapper
{
    public List<ConsumWrapper> consumSaved = new List<ConsumWrapper>();
    public List<EquipWrapper> equipSaved = new List<EquipWrapper>();
}

[System.Serializable]
public class ItemManager
{
    [JsonProperty]
    public DataManager dataManager;
    public Dictionary<string, EquipItem> EquipScripts = new Dictionary<string, EquipItem>();
    public Dictionary<string, ConsumItem> ConsumScripts = new Dictionary<string, ConsumItem>();
    public Dictionary<Vector2, Item> ItemPosList = new Dictionary<Vector2, Item>();
    public Dictionary<Vector2, ItemBase> itemBaseList = new Dictionary<Vector2, ItemBase>();
    //public List<ItemWrapper> ItemSaveData = new List<ItemWrapper>();
    public ItemDataWrapper wrappedData = new ItemDataWrapper();
    //public Dictionary<Vector2, ItemBase> itemSave = new Dictionary<Vector2, ItemBase>();
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
                index = GameManager.instance.dataManager.consymIndexBytier[tier][index];
                Debug.Log(dataManager.consumData[index]);
                itemType = (dataManager.consumData[index]["type"]).ToString();
                var consumscript = ConsumScripts[itemType];
                ConsumType consumType;

                if(Enum.TryParse(itemType, out consumType))
                {
                    GameObject go = new GameObject();
                    Item consum = go.AddComponent<Item>();
                    consum.myInfo = consumscript.Clone(consumType);
                    dataManager.SetItempData(index,consum.myInfo);
                    go.name = consum.myInfo.name;
                    AddItemComponents(go);
                    go.transform.position = new Vector3(randomPos.x,randomPos.y,-1);
                    ItemPosList[randomPos] = consum;
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
                index = GameManager.instance.dataManager.equipIndexBytier[tier][index];
                itemType = (dataManager.equipMentsData[index]["type"]).ToString();
                var equipscript = EquipScripts[itemType];
                EquipType equipType;

                if (Enum.TryParse(itemType, out equipType))
                {
                    GameObject go = new GameObject();
                    Item equip = go.AddComponent<Item>();
                    equip.myInfo = equipscript.Clone(equipType);
                    dataManager.SetItempData(index, equip.myInfo);
                    go.name = equip.myInfo.name;
                    AddItemComponents(go);
                    go.transform.position = new Vector3(randomPos.x, randomPos.y, -1);
                    if (!ItemPosList.ContainsKey(randomPos))
                    {
                        ItemPosList.Add(randomPos, equip);
                    }
                    else
                    {
                        Debug.Log("itemPosition Same");
                    }
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
        collider.size = new Vector2(0.5f, 0.5f);
        go.transform.tag = "Item";
        go.AddComponent<FogOfWar>();
        go.gameObject.layer = 6;
    }


    public int GetTierByRate(int rate,List<int> tiers)//Todo
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
        
        foreach(Vector2 pos in itemBaseList.Keys)
        {
            GameObject go = new GameObject();
            go.transform.position = pos;
            go.transform.position = new Vector3() {x=go.transform.position.x,y=go.transform.position.y,z=-1 };
            Item item = go.AddComponent<Item>();
            AddItemComponents(go);
            item.myInfo = itemBaseList[pos];
            go.name = item.myInfo.name;
            ItemPosList[pos] = item;
            SetItemImage(go, go.name);
        }
    }
}
