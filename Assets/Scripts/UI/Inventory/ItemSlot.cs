using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : ButtonUI
{
    public int slotNumber;
    ItemBase slotItem;
    Image slotImage;
    Button inventoryButton;
    private void Start()
    {
        slotImage = transform.GetChild(1).transform.GetComponent<Image>();

    }


    



}
