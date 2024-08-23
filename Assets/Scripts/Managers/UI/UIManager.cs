using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    List<GameObject> openedUI = new List<GameObject>();

    
    public void AddUIListUpDate(GameObject go)
    {
        if (!openedUI.Contains(go))
        {
            openedUI.Add(go);
            Debug.Log(openedUI.Count);
        }
    }
    public void RemoveUIListUpdate()
    {
        if (openedUI.Count >= 1)
        {
            openedUI[openedUI.Count - 1].SetActive(false);
            openedUI.RemoveAt(openedUI.Count - 1);
            Debug.Log(openedUI.Count);
        }
        
    }

    
    
}
