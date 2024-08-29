using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
public class ItemManager
{
    static ItemManager _Instance;
    public static ItemManager Instance { get { Init(); return _Instance; } }
    public Dictionary<EquipType, EquipItem> EquipScripts = new Dictionary<EquipType, EquipItem>();

    static void Init()
    {
        if(_Instance == null)
        {
            _Instance = new ItemManager();
        }
    }


    T GetRandomType<T>() where T : Enum
    {
        Array value = Enum.GetValues(typeof(T));
        return (T)value.GetValue(UnityEngine.Random.Range(0, value.Length));
    }


    public void ItemFactory()
    {
        GameObject go = new GameObject();
        var (itemType,consum, equip) = GetItemKind();

        if(itemType == ItemType.Equipment && consum==null)
        {

        }
        else if (itemType == ItemType.Consumable && equip == null)
        {

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
