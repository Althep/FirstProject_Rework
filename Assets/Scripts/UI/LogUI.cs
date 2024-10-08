using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ActType
{
    attack,
    openUI,
    usingItem
}
public class LogUI : UIBase
{
    GameObject Content;
    
    GameObject logObjPreFab;
    TextMesh text;



    void AddLog(GameObject actObj,GameObject targetOBJ,ActType actType)
    {
        if(Content == null)
        {
            Content = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        }
        if(Content.name != "Content")
        {
            Content = GameObject.Find("Content");
        }
        switch (actType)
        {
            case ActType.attack:
                text.text = $"{actObj.name} attack {targetOBJ.name}";
                break;
            case ActType.openUI:
                text.text = $"{actObj.name} opend {targetOBJ}";
                break;
            case ActType.usingItem:
                text.text = $"{actObj} use {targetOBJ.name}";
                break;
            default:
                break;
        }
        logObjPreFab.transform.GetComponent<TextMesh>().text = text.text;
        GameObject go = GameObject.Instantiate(logObjPreFab);
        go.transform.SetParent(Content.transform);
    }

}
