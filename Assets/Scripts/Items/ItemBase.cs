using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
public enum ItemType
{
    Consumable,
    Equipment
}


[System.Serializable]
public class ItemBase
{
    [JsonProperty]
    //public int _index;
    public int index; //{ get { return _index; } }

    //public string _name;
    public string name;// { get { return _name; } }

    //protected int _weight;
    public int weight;//{ get { return _weight; } }

    public int tier;
    protected void GetMyData()
    {



    }
}



