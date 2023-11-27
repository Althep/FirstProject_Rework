using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj != null)
        {
            CameraFollow(playerObj);
        }
    }
    public void CameraFollow(GameObject go)
    {
        cameraObj.transform.position = go.transform.position;
    }
}
