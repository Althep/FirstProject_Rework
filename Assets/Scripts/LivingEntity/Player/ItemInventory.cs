using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json;
public class ItemInventory//<T> where T : ItemBase
{
    [JsonProperty]
    public List<Item> Inventory = new List<Item>();
    public List<ItemBase> invenSave = new List<ItemBase>();

    public void GetEquipItem(Item item)
    {
        Inventory.Add(item);
        Debug.Log(item.name);
        Debug.Log($"ItemCount = {Inventory.Count}");
    }

    public void GetConsumItem(Item item)
    {
        if (IsSame(item))
        {
            AddSameItem(item);
            Debug.Log(item.name);
        }
        else
        {
            Inventory.Add(item);
            if(item.myInfo is ConsumItem consum)
            {
                consum.itemCount++;
            }
        }
    }
    
    bool IsSame(Item item)//소모품 이름 확인
    {
        return Inventory.Any(i => i.myInfo.name == item.myInfo.name);
    }

    void AddSameItem(Item item)//소모품 갯수++
    {
        foreach (var value in Inventory)
        {
            if(value.myInfo is ConsumItem consumable && value.myInfo.name == item.myInfo.name)
            {
                consumable.itemCount++;
                break;
            }
        }
    }

    /*
    internal void GetEquipItem<T>(Item<T> item) where T : ItemBase
    {
        throw new NotImplementedException();
    }
    */
}

