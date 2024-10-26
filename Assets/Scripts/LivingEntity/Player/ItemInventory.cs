using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json;
public class ItemInventory//<T> where T : ItemBase
{
    [JsonProperty]
    public List<ItemBase> Inventory = new List<ItemBase>();
    public List<ItemBase> invenSave = new List<ItemBase>();

    public void GetEquipItem(ItemBase item)
    {
        Inventory.Add(item);
        Debug.Log(item.name);
        Debug.Log($"ItemCount = {Inventory.Count}");
    }

    public void GetConsumItem(ItemBase item)
    {
        if (IsSame(item))
        {
            AddSameItem(item);
            Debug.Log(item.name);
        }
        else
        {
            Inventory.Add(item);
            if(item is ConsumItem consum)
            {
                consum.itemCount++;
            }
        }
    }
    
    bool IsSame(ItemBase item)//소모품 이름 확인
    {
        return Inventory.Any(i => i.name == item.name);
    }

    void AddSameItem(ItemBase item)//소모품 갯수++
    {
        foreach (var value in Inventory)
        {
            if(value is ConsumItem consumable && value.name == item.name)
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

