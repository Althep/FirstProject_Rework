using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlot : ButtonUI
{
    public int slotNumber;
    public ItemBase slotItem;
    Image slotImage;
    Button inventoryButton;
    TextMeshProUGUI tmp;
    private void Start()
    {
        GetMyComp();
    }

    public void GetMyComp()
    {
        slotImage = transform.GetChild(1).transform.GetComponent<Image>();
        tmp = transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>();
    }

    public void SetItemName(int index)
    {
        Item item = GameManager.instance.playerState.myInventory.Inventory[index];
        if(slotImage == null)
        {
            slotImage = transform.GetChild(1).transform.GetComponent<Image>();
        }
        if(tmp == null)
        {
            tmp = transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>();
        }
        if (item.myInfo is ConsumItem consum)
        {
            Debug.Log(item.myInfo.name);
            tmp.text = consum.name + $" : {consum.itemCount}";
        }
        else
        {
            Debug.Log(item.myInfo.name);
            tmp.text = item.myInfo.name;
        }
    }

    public void SetItemImage()
    {

    }
    



}
