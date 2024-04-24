using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInfo : MonoBehaviour
{
    Dictionary<int , EquipBase> equipMents;

    /* SlotNumber
     *  0,1 = Weapon And Sheild
     *  2 = Helm
     *  3 = Armor
     *  4 = Boots
    */


    public void EquipItem(int slotNumber, EquipBase itemInfo)
    {
        if (equipMents[slotNumber] != null)
        {
            equipMents[slotNumber].CallUnEquip();
            equipMents[slotNumber] = itemInfo;
            equipMents[slotNumber].CallEquip();

        }
        else
        {
            equipMents.Add(slotNumber, itemInfo);
            equipMents[slotNumber] = itemInfo;
            equipMents[slotNumber].CallEquip();

        }
    }
    public void UnEquipItem(int slotNumber)
    {
        equipMents[slotNumber].CallUnEquip();
        equipMents[slotNumber] = null;
    }
}
