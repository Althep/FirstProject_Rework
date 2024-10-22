using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDestroy : MonoBehaviour
{
    void Start()
    {
        if(GameManager.instance.canvas!=null && GameManager.instance.canvas != this.gameObject)
        {
            Destroy(this.gameObject);
        }
            
    }

    void Update()
    {
        
    }
}
