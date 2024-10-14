using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBase : MonoBehaviour
{
    protected KeyCode setKey;
    string uiName;
    GameObject myObj;
    InputManager inputManager;

    RectTransform myTransform;


    protected virtual void Start()
    {
        myTransform = transform.GetComponent<RectTransform>();
        inputManager = GameManager.instance.inputManager;
        Init();
        SetPosition();
    }

    protected virtual void Init()
    {
        myObj = this.gameObject;
        uiName = this.gameObject.name;
        if (setKey == KeyCode.None)
        {
            Debug.Log(myObj.name + "isn't set");
            return;
        }
        else
        {
            inputManager.UIKeyCodeToObj.Add(setKey, this.gameObject);
        }

        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }

    protected virtual void SetPosition()
    {
        Vector2 myPos = new Vector2(Screen.width / 2, Screen.height / 2);

        myTransform.anchorMin = new Vector2(0.5f, 0.5f);
        myTransform.anchorMax = new Vector2(0.5f, 0.5f);
        myTransform.position = myPos;

    }
}
