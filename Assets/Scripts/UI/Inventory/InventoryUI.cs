using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    // Start is called before the first frame update

    GameObject playerOBJ;
    ItemInventory playerInven;
    public GameObject SlotPrefab;
    List<GameObject> slotList = new List<GameObject>();
    List<ItemSlot> slotScripts = new List<ItemSlot>();
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

        OpenInventory();
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
            slotScripts.Add(go.transform.GetComponent<ItemSlot>());
        }


    }

    void SetPlayerOBJ()
    {
        playerOBJ = GameManager.instance.playerObj;
        playerInven = playerOBJ.transform.GetComponent<ItemInventory>();
    }


    public void OpenInventory()
    {
        for (int i = 0; i < playerInven.Inventory.Count; i++)
        {

            slotScripts[i].SetItemName(i);

        }
    }

    void MakeInventorySlot()
    {


    }

}
