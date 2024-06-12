using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject playerOBJ;
    Inventory playerInven;
    public GameObject inventorySlot;
    GameObject inventoryPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetPlayerOBJ()
    {
        playerOBJ = GameManager.instance.playerObj;
        playerInven = playerOBJ.transform.GetComponent<Inventory>();
    }
    void SetInventoryPanel()
    {
        inventoryPanel = this.gameObject.transform.GetChild(0).gameObject;
    }

    public void OpenInventory()
    {

    }

    void MakeInventorySlot()
    {
        for(int i = 0; i < playerInven.inventoryItems.Count; i++)
        {
            

        }

    }

    void InstantiateItemSlot()
    {
        GameObject go = Instantiate(inventorySlot);
        go.transform.SetParent(inventoryPanel.transform);
    }

}
