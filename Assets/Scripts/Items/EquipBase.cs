using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBase : ItemBase
{
    protected virtual void Equip()
    {




    } 

    public void CallEquip()
    {
        Equip();
    }
    protected virtual void UnEquip()
    {


    }
    public void CallUnEquip()
    {
        UnEquip();
    }

}
