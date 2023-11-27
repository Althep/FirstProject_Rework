using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind
{
    None,
    Consumable,
    Equipment
}
public enum EquipType
{

}
public class ItemBase
{
    public int id;
    public string name;
    public int weight;
    public ItemKind itemKind;
    public List<string> functions;
}
public class EquipMent : ItemBase
{
    public int slot;
    EquipType equipType; 
    EquipMent()
    {
        this.itemKind = ItemKind.Equipment;
    }
}
public class Consumable : ItemBase 
{
    public int amunt;
    Consumable()
    {
        this.itemKind = ItemKind.Consumable;
    }
    public int amount;
}

