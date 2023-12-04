using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject playerObj;
    public Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameManager.instance.playerObj;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }
    public void CameraFollow()
    {
        if(playerObj == null)
        {
            playerObj = GameManager.instance.playerObj;
        }
        if (playerObj != null && playerPos!=(Vector2)playerObj.transform.position)
        {
            Debug.Log(1);

            this.gameObject.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z - 10) ;
            playerPos = playerObj.transform.position;
        }
        
    }
}
