using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<ItemBase> inventoryItems;
    LivingEntity myEntity;
    EquipInfo myEquip;
    // Start is called before the first frame update
    void Start()
    {
        SetEntity();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetEntity()
    {
        myEntity = this.gameObject.transform.GetComponent<LivingEntity>();
        myEquip = this.gameObject.transform.GetComponent<EquipInfo>();
    }

    void GetItem(ItemBase getaItem)
    {
        inventoryItems.Add(getaItem);
    }

    void DropItem()
    {

    }

    void ItemUse(ItemBase itembase)
    {
        switch (itembase.itemKind)
        {
            case ItemKind.None:
                Debug.Log("ItemKindError , ItemKindNone");
                break;
            case ItemKind.Consumable:
                break;
            case ItemKind.Equipment:
                break;
            default:
                Debug.Log("ItemKindError , Default");
                break;
        }
    }


    void ItemEquip()
    {


    }

    void Equip()
    {

    }
    void UnEquip()
    {

    }

    void ConsumableUse()
    {

    }

}
