using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StairType
{
    upStair,
    downStair
}
public class Stair : MonoBehaviour
{
    public int stairNumber;
    [SerializeField]bool CanUse;
    public StairType stairType;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L)&&CanUse)
        {
            CanUse = false;
            SceneChange();
        }
    }

    void SceneChange()
    {
        if(SceneManager.GetActiveScene().name == "Scene2")
        {
            GameManager.instance.stairNumber = stairNumber;
            GameManager.instance.stairType = stairType;
            GameManager.instance.nextScene = "Scene1";

        }
        else if(SceneManager.GetActiveScene().name == "Scene1")
        {
            GameManager.instance.stairNumber = stairNumber;
            GameManager.instance.stairType = stairType;
            GameManager.instance.nextScene = "Scene2";
        }
        GameManager.instance.save.SaveMap();
        SceneManager.LoadScene(GameManager.instance.nextScene);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&& collision is BoxCollider2D)
        {
            CanUse = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision is BoxCollider2D)
        {
            CanUse = false;
        }
    }
}
