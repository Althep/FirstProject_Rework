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
    bool CanUse;
    public StairType stairType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)&&CanUse)
        {
            SceneChange();
        }
    }

    void SceneChange()
    {
        if(SceneManager.GetActiveScene().name == "Scene2")
        {
            SceneManager.LoadScene("Scene1");
            FloorChange();
        }
        else if(SceneManager.GetActiveScene().name == "Scene1")
        {
            SceneManager.LoadScene("Scene2");
            FloorChange();
        }
    }

    void FloorChange()
    {
        switch (stairType)
        {
            case StairType.upStair:
                GameManager.instance.floor++;
                break;
            case StairType.downStair:
                GameManager.instance.floor--;
                break;
            default:
                break;
        }
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
