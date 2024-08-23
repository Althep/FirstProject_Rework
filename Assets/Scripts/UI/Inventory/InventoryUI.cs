using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    // Start is called before the first frame update

    GameObject playerOBJ;
    Inventory playerInven;
    public GameObject inventorySlot;
    

    protected override void Start()
    {
        setKey = KeyCode.I;
        Debug.Log(setKey);
        base.Start();
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


    public void OpenInventory()
    {

    }

    void MakeInventorySlot()
    {
        for(int i = 0; i < playerInven.inventoryItems.Count; i++)
        {
            

        }

    }

}
