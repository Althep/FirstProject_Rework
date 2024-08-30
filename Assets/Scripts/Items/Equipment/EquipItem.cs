using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EquipType
{
    Weapon,
    Shield,
    Helm,
    Armor,
    Amulet,
    Glove,
    Shoose,
    Ring
}

public class EquipItem : ItemBase
{
    public EquipType _equipType;
    public EquipOptions options;

    public EquipItem()
    {
        Register();
    }
    protected void Register()
    {
        string name = this.GetType().Name;
        Debug.Log(name);
        if (name == "EquipItem")
            return;
        if (!GameManager.instance.item.EquipScripts.ContainsKey(name))
        {
            GameManager.instance.item.EquipScripts.Add(name, this);
        }
    }

    public virtual EquipItem Clone(EquipType type)
    {
        if (_equipType != type)
        {
            _equipType = type;
        }
        switch (type)
        {
            case EquipType.Weapon:
                return new Weapon();
            case EquipType.Shield:
                return new Shield();
            case EquipType.Helm:
                return new Helm();
            case EquipType.Armor:
                return new Armor();
            case EquipType.Amulet:
                return new Amulet();
            case EquipType.Glove:
                return new Glove();
            case EquipType.Shoose:
                return new Shoose();
            case EquipType.Ring:
                return new Ring();
        }
        throw new ArgumentException("Type Exception", nameof(type));
    }

}

public class Weapon : EquipItem
{
    public int damage;
    public int attackSpeed;
    /*
    public Weapon()
    {
        _equipType = EquipType.Weapon;
        Register();
    }
    */

}

public class Shield : EquipItem
{
    int defense;
    int blockRate;
    /*
    public Shield()
    {
        _equipType = EquipType.Shield;
        Register();
    }
    */

}

public class Helm : EquipItem
{
    int defense;
    /*
    public Helm()
    {
        _equipType = EquipType.Helm;
        Register();

    }
    */
}
public class Armor : EquipItem
{
    int defense;
    /*
    public Armor()
    {
        _equipType = EquipType.Armor;
        Register();

    }
    */
}
public class Amulet : EquipItem
{
    /*
    int defense;
    public Amulet()
    {
        _equipType = EquipType.Amulet;
        Register();

    }
    */
}
public class Glove : EquipItem
{
    int defense;
    /*
    public Glove()
    {
        _equipType = EquipType.Glove;
        Register();

    }
    */
}
public class Shoose : EquipItem
{
    int defense;
    /*
    public Shoose()
    {
        _equipType = EquipType.Shoose;
        Register();
    }
    */
}
public class Ring : EquipItem
{
    int defense;
    /*
    public Ring()
    {
        _equipType = EquipType.Ring;
        Register();
    }
    */
}
