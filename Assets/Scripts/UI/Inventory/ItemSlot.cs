using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class ItemSlot : ButtonUI
{
    public int slotNumber;
    public ItemBase slotItem;
    public Image itemImage;
    public Image slotImage;
    public Button inventoryButton;
    public TextMeshProUGUI tmp;
    PlayerState player;
    public InventoryUI invenUI;
    private void Start()
    {
        GetMyComp();
    }

    public void GetMyComp()
    {
        if (itemImage == null)
        {
            itemImage = transform.GetChild(1).transform.GetComponent<Image>();
            itemImage.color = new Color(255, 255, 255, 255);
            //slotImage.gameObject.transform.SetAsLastSibling();
        }
        if (tmp == null)
        {
            tmp = transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>();
        }
        if (inventoryButton == null)
        {
            inventoryButton = transform.GetComponent<Button>();
        }
        if (slotImage == null)
        {
            slotImage = this.gameObject.transform.GetComponent<Image>();
        }
        if (player == null)
        {
            player = GameManager.instance.playerState;
        }

    }

    public void SetSlot(int index)
    {
        GetMyComp();
        ItemBase item = GameManager.instance.playerState.myInventory.Inventory[index];


        if (item is ConsumeItem consum)
        {
            Debug.Log(item.name);
            tmp.text = consum.name + $" : {consum.itemCount}";
        }
        else
        {
            Debug.Log(item.name);
            tmp.text = item.name;
        }
        SetSlotImage(item.name, itemImage);
        ButtonFunction();
        if (slotItem is EquipItem equip)
        {
            ChangeColor(equip);
        }
    }

    void SetSlotImage(string name, Image image)
    {
        Debug.Log("ImageSet");

        Sprite sprite = GameManager.instance.dataManager.GetItemImage(name);
        if (sprite == null)
        {
            image.sprite = null;
            return;
        }
        image.sprite = sprite;

    }

    protected override void ButtonFunction()
    {
        UnityAction action;
        if (slotItem is EquipItem equip)
        {
            action = IsEquiped(equip);
            inventoryButton.onClick.AddListener(action);

        }
        else if (slotItem is ConsumeItem consum)
        {
            action = IsConsum(consum);
            inventoryButton.onClick.AddListener(action);
        }

    }
    public UnityAction IsEquiped(EquipItem equip)
    {
        //UnityAction action;
        return () =>
        {
            if (player.equips.ContainsKey(equip._equipType))
            {
                if (player.equips[equip._equipType] == slotItem)
                {
                    equip.UnEquip(player);//action = () => equip.UnEquip(player);
                    ChangeColor(equip);

                }
                else
                {
                    equip.Use(player);//action = () => equip.Use(player);
                    ChangeColor(equip);
                    EquipUpdate(equip);
                }
            }
            else
            {
                equip.Use(player);//action = () => equip.Use(player);
                ChangeColor(equip);
            }
            Debug.Log(player.myInventory.Inventory[slotNumber]);
        };
        //inventoryButton.onClick.AddListener(action);
    }
    public void ChangeColor(EquipItem equip)
    {
        if (player.equips.ContainsValue(equip))
        {
            slotImage.color = Color.green;
        }
        else
        {
            slotImage.color = Color.white;
        }
    }
    public UnityAction IsConsum(ConsumeItem consum)
    {
        return () =>
        {
            consum.Use(player);//UnityAction action = () => consum.Use(player);
            ResetSlotData(consum);
            //action += UpDataInventory(consum);
            
                              //inventoryButton.onClick.AddListener(action);
        };


    }
    public void EquipUpdate(EquipItem equip)
    {
        for(int i = 0; i < invenUI.slotScripts.Count; i++)
        {
            if(invenUI.slotScripts[i].slotItem is EquipItem value)
            {
                if(equip._equipType == value._equipType && i != slotNumber)
                {
                    invenUI.slotScripts[i].ChangeColor(value);
                }
            }
        }
    }
    public void ResetSlotData(ConsumeItem consum)
    {
        if(consum.itemCount <= 0)
        {
            slotItem = null;
            itemImage.sprite = null;
            tmp.text = null;
            UpDataNextIndexs(slotNumber);
            invenUI.RemoveLastIndex();
        }
        else
        {
            UpDataNextIndexs(slotNumber);
        }
    }
    
    public void UpDataNextIndexs(int start)
    {
        inventoryButton.onClick.RemoveAllListeners();
        invenUI.OpenInventory(slotNumber);
        Debug.Log($"slot Number : {slotNumber}");
    }
}
