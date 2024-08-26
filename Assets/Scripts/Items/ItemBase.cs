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
    Weapon,
    Helmet,
    Armor,
    Glove,
    Shoose
}
public class ItemBase
{
    public int id;
    public string name;
    public int weight;
    public ItemKind itemKind;
    public List<string> functions;

    public virtual void Use()
    {
        switch (itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Consumable:
                break;
            case ItemKind.Equipment:

                break;
            default:
                break;
        }
    }
}

