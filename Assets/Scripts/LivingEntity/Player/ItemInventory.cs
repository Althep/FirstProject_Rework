using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemInventory//<T> where T : ItemBase
{
    public List<Item> Inventory = new List<Item>();


    public void GetEquipItem(Item item)
    {
        Inventory.Add(item);
    }

    public void GetConsumItem(Item item)
    {
        if (IsSame(item))
        {
            AddSameItem(item);
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

