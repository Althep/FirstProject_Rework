using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StateUI : MonoBehaviour
{
    int floor;
    TextMeshProUGUI tmp;
    void Start()
    {
        tmp = this.gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(floor != GameManager.instance.floor)
        {
            
            floor = GameManager.instance.floor;
            tmp.text = "Floor : " + floor+"  "+SceneManager.GetActiveScene().name;
        }
    }
}
