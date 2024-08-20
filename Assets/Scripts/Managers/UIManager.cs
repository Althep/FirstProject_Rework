using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    GameObject inventoryUI;
    InputManager inputManager;
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }
    void Init()
    {
        SetInputManager();
        SetInventoryObj();
    }

    void UIActiveChange(KeyCode keycode)
    {
        switch (keycode)
        {

            case KeyCode.I:
                inputManager.UIObjectOnOff(inventoryUI);
                break;
            case KeyCode.Z:
                break;
            default:
                break;
        }

    }

    void SetInputManager()
    {
        inputManager = GameManager.instance.gameObject.transform.GetComponent<InputManager>();
    }

    void SetInventoryObj()
    {
        inventoryUI = GameObject.Find("Inventory");
        inventoryUI.SetActive(false);
    }
}
