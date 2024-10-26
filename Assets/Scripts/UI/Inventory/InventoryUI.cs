using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public class InventoryUI : UIBase
{
    // Start is called before the first frame update

    GameObject playerOBJ;
    ItemInventory playerInven;
    public GameObject SlotPrefab;
    List<GameObject> slotList = new List<GameObject>();
    public List<ItemSlot> slotScripts = new List<ItemSlot>();
    protected override void Start()
    {
        setKey = KeyCode.I;
        Debug.Log(setKey);
        base.Start();
    }

    private void Awake()
    {
        InstantSlot();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        OpenInventory(0);
    }
    private void OnDisable()
    {
        CloseInventory();
    }
    void InstantSlot()
    {
        if (playerInven == null)
        {
            playerInven = GameManager.instance.playerState.myInventory;
        }

        for (int i = slotList.Count; i < 40; i++)
        {
            GameObject go = Instantiate(SlotPrefab);
            slotList.Add(go);
            go.transform.SetParent(this.transform);
            ItemSlot slotScrip = go.transform.GetComponent<ItemSlot>();
            slotScrip.slotNumber = i;
            slotScripts.Add(slotScrip);
        }


    }

    void SetPlayerOBJ()
    {
        playerOBJ = GameManager.instance.playerObj;
        playerInven = playerOBJ.transform.GetComponent<ItemInventory>();
    }


    public void OpenInventory(int start)
    {
        for (int i = start; i < playerInven.Inventory.Count; i++)
        {
            slotScripts[i].slotItem = playerInven.Inventory[i];
            if (playerInven.Inventory[i] == null)
            {
                slotScripts[i].slotItem = null;
                slotScripts[i].itemImage.sprite = null;
                slotScripts[i].tmp.text = null;
            }
            //slotScripts[i].inventoryButton.onClick.RemoveAllListeners();
            slotScripts[i].SetSlot(i);
            slotScripts[i].invenUI = this;
        }
    }
    public void CloseInventory()
    {
        for (int i = 0; i < playerInven.Inventory.Count; i++)
        {
            Button button = slotScripts[i].gameObject.transform.GetComponent<Button>();
            Image image = slotScripts[i].gameObject.transform.GetComponent<Image>();
            slotScripts[i].itemImage = null;
            if (button.onClick != null)
            {
                button.onClick.RemoveAllListeners();
            }
            image.sprite = null;
        }
    }
    public void RemoveLastIndex()
    {
        int index = playerInven.Inventory.Count;
        slotScripts[index].itemImage.sprite = null;
        slotScripts[index].slotItem = null;
        slotScripts[index].tmp.text = null;
        slotScripts[index].slotImage.color = Color.white;
    }

}
