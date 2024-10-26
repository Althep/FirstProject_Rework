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
[System.Serializable]
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
        //Debug.Log(name);
        if (name == "EquipItem")
            return;
        if (!GameManager.instance.item.EquipScripts.ContainsKey(name))
        {
            GameManager.instance.item.EquipScripts.Add(name, this);
        }
    }
    public override void Use(LivingEntity entity)
    {
        Debug.Log("ItemUse");
        if (entity.equips.ContainsKey(_equipType))
        {
            UnEquip(entity);
            AddValue(entity);
        }
        else
        {
            AddValue(entity);
        }
    }
    public virtual void AddValue(LivingEntity entity)
    {

    }
    public virtual void UnEquip(LivingEntity entity)
    {
        if (entity.equips.ContainsKey(_equipType))
        {
            entity.equips.Remove(_equipType);
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


    public override void AddValue(LivingEntity entity)
    {
        entity.myState.damage += damage;
        entity.equips.Add(_equipType, this);
    }

    public override void UnEquip(LivingEntity entity)
    {
        if (entity.equips.ContainsKey(_equipType))
        {
            entity.equips.Remove(_equipType);
            entity.myState.damage -= damage;
        }
    }
    /*
    public Weapon()
    {
        _equipType = EquipType.Weapon;
        Register();
    }
    */

}

public class Defensive : EquipItem
{
    public int defense;

    public override void AddValue(LivingEntity entity)
    {
        entity.myState.def += defense;
        entity.equips.Add(_equipType, this);
    }
    public override void UnEquip(LivingEntity entity)
    {
        if (entity.equips.ContainsKey(_equipType))
        {
            entity.equips.Remove(_equipType);
            entity.myState.def -= defense;
        }
    }
}

public class Shield : Defensive
{

    public int blockRate;

    public override void AddValue(LivingEntity entity)
    {
        entity.myState.def += defense;
        entity.myState.blockRate += blockRate;
        entity.equips.Add(_equipType, this);
    }
    public override void UnEquip(LivingEntity entity)
    {
        if (entity.equips.ContainsKey(_equipType))
        {
            entity.equips.Remove(_equipType);
            entity.myState.def -= defense;
            entity.myState.blockRate -= blockRate;
        }
    }
}

public class Helm : Defensive
{
    
}
public class Armor : Defensive
{
    /*
    public Armor()
    {
        _equipType = EquipType.Armor;
        Register();

    }
    */
}
public class Amulet : Defensive
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
public class Glove : Defensive
{
    /*
    public Glove()
    {
        _equipType = EquipType.Glove;
        Register();

    }
    */
}
public class Shoose : Defensive
{
    /*
    public Shoose()
    {
        _equipType = EquipType.Shoose;
        Register();
    }
    */
}
public class Ring : Defensive
{
    /*
    public Ring()
    {
        _equipType = EquipType.Ring;
        Register();
    }
    */
}
