using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ItemType
{
    Consumable,
    Equipment
}



public class ItemBase
{
    public int _index;
    public int index { get { return _index; } }

    public string _name;
    public string name { get { return _name; } }

    protected int _weight;
    public int weight { get { return _weight; } }

    protected void GetMyData()
    {



    }
}



