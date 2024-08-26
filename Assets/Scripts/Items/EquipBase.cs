using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBase : ItemBase
{

    EquipInfo playerEqip;
    EquipType _equipType;
    public EquipType equipType { set { equipType = _equipType; } get { return _equipType; } }
    

}
