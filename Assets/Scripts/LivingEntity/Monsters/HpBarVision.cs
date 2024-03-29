using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBarVision : MonoBehaviour
{
    GameObject hpSliderObj;
    Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        SetHpSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHpSlider()
    {
        hpSliderObj = transform.GetChild(0).gameObject;
        hpSlider = hpSliderObj.transform.GetComponent<Slider>();
    }
    
    void ActiveFalseHpSlider()
    {
        hpSliderObj.SetActive(false);
    }
    void ActiveTrueHpSlider()
    {
        hpSliderObj.SetActive(true);
    }

}
