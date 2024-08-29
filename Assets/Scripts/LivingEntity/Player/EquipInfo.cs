using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EquipInfo : MonoBehaviour
{
    public Dictionary<EquipType, EquipItem> equipMents;

    //슬롯의 string을 가져와서 EquipBase 키값이 있는지 비교


    public void UseEquip(EquipItem equip)
    {
        if (equipMents.ContainsKey(equip._equipType))
        {
            if (equip == equipMents[equip._equipType])
            {
                equipMents[equip._equipType] = null;
            }
            else
            {
                equipMents[equip._equipType] = equip;
            }

            //EquipChange
        }
        else
        {
            equipMents.Add(equip._equipType,equip);
        }
    }



    
}
