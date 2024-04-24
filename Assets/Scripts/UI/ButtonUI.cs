using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    Sprite onSprite;
    Sprite offSprite;
    Button myButton;

    private void Start()
    {
        SetMyButton();
    }


    private void SetMyButton()
    {
        myButton = this.gameObject.transform.GetComponent<Button>();
        AddOnClickEvent();
    }


    private void AddOnClickEvent()
    {
        myButton.onClick.AddListener(ButtonFunction);

    }

    protected virtual void ButtonFunction()
    {



    }

}
