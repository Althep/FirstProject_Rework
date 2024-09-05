using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
public class ItemManager
{
    public DataManager dataManager;
    public Dictionary<string, EquipItem> EquipScripts = new Dictionary<string, EquipItem>();
    public Dictionary<string, ConsumItem> ConsumScripts = new Dictionary<string, ConsumItem>();

    T GetRandomType<T>() where T : Enum
    {
        Array value = Enum.GetValues(typeof(T));
        return (T)value.GetValue(UnityEngine.Random.Range(0, value.Length));
    }


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
    /*
    public void AddKeysScripts()
    {
        var types = Enum.GetValues(typeof(EquipType));
        foreach(EquipType type in types)
        {
            EquipScripts.Add(type, null);
            Debug.Log(type.ToString());
        }
        
    }
    private void ResisterEquipItems()
    {
        var equipScripts = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(EquipItem)) && !t.IsAbstract);

        foreach (var scripts in equipScripts) 
        { 
            
        
        
        
        }


    }
    */
}
