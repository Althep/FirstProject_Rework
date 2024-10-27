using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ActType
{
    attack,
    openUI,
    usingItem
}
public class LogUI : UIBase
{
    public GameObject Content;
    
    public GameObject logObjPreFab;
    TextMeshProUGUI text;
    List<GameObject> logList = new List<GameObject>();
    Scrollbar scrollbar;

    protected override void SetPosition()
    {
        base.SetPosition();
        RectTransform rect = this.transform.GetComponent<RectTransform>();
        Vector2 myPos = new Vector2(rect.rect.width/2, 0);
        this.transform.position = myPos;
    }

    public void SetContent()
    {
        if(Content == null)
        {
            Content = GameObject.Find("Content");
        }
        if (text == null)
        {
            text = logObjPreFab.transform.GetComponent<TextMeshProUGUI>();
        }
        if(scrollbar == null)
        {
            scrollbar = this.gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Scrollbar>();
        }
        
    }

    public void LogPreFabFactory()
    {
        text = logObjPreFab.transform.GetComponent<TextMeshProUGUI>();
        GameObject go = GameObject.Instantiate(logObjPreFab);
        text.fontSize = 20f;

        go.transform.SetParent(Content.transform);
        text.color = Color.white;
        logList.Add(go);
        scrollbar.value = 0f;
    }
    public void Damaged(GameObject go, float hpbarValue)
    {
        SetContent();
        
        if(text == null)
        {
            Debug.Log("text is null! ");
            return;
        }
        if (hpbarValue > 0.8f)
        {
            text.color = Color.white;
            text.text = $"{go.name} Damaged";
            Debug.Log(0.8f);
        }
        else if (hpbarValue >= 0.5f && hpbarValue<=0.8)
        {
            text.color = Color.yellow;
            text.text = $"{go.name} looks like hurt";
            Debug.Log(0.5f);
        }
        else if (hpbarValue > 0.3f && hpbarValue<0.5f)
        {
            text.color = Color.red;
            text.text = $"{go.name} looks really hurt";
            Debug.Log(0.3f);
        }
        else if(hpbarValue<=0.3f)
        {
            text.color = Color.red;
            text.text = $"{go.name} almost die";
        }
        else
        {
            Debug.Log("LogUI Damaged Fuction Error");
        }
        LogPreFabFactory();
    }

    public void ItemUse(GameObject go, ItemBase item)
    {
        text.text = $"{go.name} Use {item.name}";
        LogPreFabFactory();
    }
    
    public void OpenUI(GameObject targetUI)
    {
        text.text = $"You opend {targetUI}";
        LogPreFabFactory();
    }
    
    public void AttackLog(GameObject actObj, GameObject targetObj)
    {
        text.text = $"{actObj.name} attack {targetObj.name}";
        LogPreFabFactory();
    }

    public void DeadLog(GameObject go)
    {
        text.color = Color.red;
        text.text = $"{go.name} dead";
        LogPreFabFactory();
    }
}
