using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EquipInfo : MonoBehaviour
{
    public Dictionary<EquipType, EquipBase> equipMents;

    //������ string�� �����ͼ� EquipBase Ű���� �ִ��� ��


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
