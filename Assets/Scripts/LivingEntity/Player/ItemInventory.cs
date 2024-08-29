using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory
{
    List<ItemBase> _Inventory = new List<ItemBase>();
    public List<ItemBase> Inventory { get { return _Inventory; } }
}

