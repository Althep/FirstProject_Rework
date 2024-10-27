using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButton : MonoBehaviour
{
    Button button;
    GameObject child;
    private void Start()
    {
        child = this.transform.GetChild(0).gameObject;
        button = this.transform.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if (child.activeSelf)
        {
            child.SetActive(false);
        }
        else
        {
            child.SetActive(true);
        }
    }
}
