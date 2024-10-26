using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ConsumType
{
    Potion,
    Book,
    Scroll,
    Evoke,
    Food

}

public class ConsumItem : ItemBase
{
    public ConsumType _consumType;
    public int itemCount;
    public string function;
    public int maintain;
    public ConsumItem()
    {
        Register();
    }
    protected void Register()
    {
        string name = this.GetType().Name;
        //Debug.Log(name);
        if (name == "ConsumItem")
            return;
        if (!GameManager.instance.item.ConsumScripts.ContainsKey(name))
        {
            GameManager.instance.item.ConsumScripts.Add(name, this);
            //Debug.Log($"regist  : {name}");
        }
    }
    public virtual ConsumItem Clone(ConsumType type)
    {
        if (_consumType != type)
        {
            _consumType = type;
        }
        switch (type)
        {
            case ConsumType.Potion:
                return new Potion();
            case ConsumType.Book:
                return new Book();
            case ConsumType.Scroll:
                return new Scroll();
            case ConsumType.Evoke:
                return new Evoke();
            case ConsumType.Food:
                return new Food();
        }
        throw new ArgumentException("Type Exception", nameof(type));
    }
    public override void Use(LivingEntity entity)
    {
        Debug.Log("ItemUse");
        if (GameManager.instance.dataManager.consumFunctions.TryGetValue(function, out Action<LivingEntity> action))
        {
            action.Invoke(entity);
            itemCount--;
            if (itemCount <= 0)
            {
                entity.myInventory.Inventory.Remove(this);
            }
        }
        else
        {
            Debug.Log("Function Cant find");
        }
    }
}

public class Potion : ConsumItem
{

}

public class Book: ConsumItem
{
    
}

public class Scroll : ConsumItem
{

}
public class Evoke : ConsumItem
{

}

public class Food : ConsumItem
{

}
/*
public class ConsumFunction
{



}
*/
