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

    protected void Register()
    {
        if (!GameManager.instance.item.EquipScripts.ContainsKey(_equipType))
        {
            GameManager.instance.item.EquipScripts.Add(_equipType, this);
        }
    }

    public virtual EquipItem Clone()
    {
        switch (_equipType)
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
        throw new ArgumentException("Type Exception", nameof(_equipType));
    }

}

public class Weapon : EquipItem
{
    public Weapon()
    {
        _equipType = EquipType.Weapon;
        Register();
    }


}

public class Shield : EquipItem
{
    public Shield()
    {
        _equipType = EquipType.Shield;
        Register();
    }

}

public class Helm : EquipItem
{
    public Helm()
    {
        _equipType = EquipType.Helm;
        Register();

    }
}
public class Armor : EquipItem
{
    public Armor()
    {
        _equipType = EquipType.Armor;
        Register();

    }
}
public class Amulet : EquipItem
{
    public Amulet()
    {
        _equipType = EquipType.Amulet;
        Register();

    }
}
public class Glove : EquipItem
{
    public Glove()
    {
        _equipType = EquipType.Glove;
        Register();

    }
}
public class Shoose : EquipItem
{
    public Shoose()
    {
        _equipType = EquipType.Shoose;
        Register();
    }
}
public class Ring : EquipItem
{
    public Ring()
    {
        _equipType = EquipType.Ring;
        Register();
    }
}
