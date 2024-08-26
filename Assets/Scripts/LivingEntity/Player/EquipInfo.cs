using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EquipInfo : MonoBehaviour
{
    public Dictionary<EquipType, EquipBase> equipMents;

    //슬롯의 string을 가져와서 EquipBase 키값이 있는지 비교


    public void UseEquip(EquipBase equip)
    {
        if (equipMents.ContainsKey(equip.equipType))
        {
            if (equip == equipMents[equip.equipType])
            {
                equipMents[equip.equipType] = null;
            }
            else
            {
                equipMents[equip.equipType] = equip;
            }

            //EquipChange
        }
        else
        {
            equipMents.Add(equip.equipType,equip);
        }
    }



    
}
