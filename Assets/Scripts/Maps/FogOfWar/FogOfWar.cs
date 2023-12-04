using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    SpriteRenderer myRenderer;
    GameObject player;
    Vector2 myPos;
    Vector2 playerPos;
    int oldLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = this.transform.GetComponent<SpriteRenderer>();
        player = GameManager.instance.playerObj;
        oldLayer = this.gameObject.layer;
        //ShotLayCast();
        ChangeColorToLayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColorToLayer()
    {
        if (myRenderer == null)
        {
            myRenderer = this.transform.GetComponent<SpriteRenderer>();
        }
        switch (this.gameObject.layer)
        {
            case 6: // UnSeen
                if (this.transform.tag != "Monster")
                {
                    myRenderer.color = Color.black;
                }
                else
                {
                    Color newColor = new Color();
                    newColor = Color.white;
                    newColor.a = 0;
                    myRenderer.color = newColor;
                }
                break;
            case 7:// Seen
                if (this.transform.tag != "Monster")
                {
                    Debug.Log(11);
                    myRenderer.color = Color.gray;
                }
                else
                {
                    Color newColor = new Color();
                    newColor = Color.white;
                    newColor.a = 0;
                    myRenderer.color = newColor;
                }
                break;
            case 8:// insight
                myRenderer.color = Color.white;
                break;
            default:
                break;
        }
    }

    void ShotLayCast()
    {
        Debug.Log("LayCasted");
        bool isBlock = false;
        int originLayer = this.gameObject.layer;
        myPos = this.transform.position;
        playerPos = player.transform.position;
        this.gameObject.layer = 2;
        RaycastHit2D[] hit = Physics2D.RaycastAll(myPos,(playerPos-myPos),Vector2.Distance(myPos,playerPos));
        this.gameObject.layer = originLayer;
        for(int i = 0; i < hit.Length; i++)
        {
            if(hit[i].transform.tag == "Wall")
            {
                isBlock = true;
            }

        }
        if (!isBlock)
        {
            this.gameObject.layer = LayerMask.NameToLayer("InSight");
        }
        if(isBlock && this.gameObject.layer == LayerMask.NameToLayer("InSight"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Seen");
        }
        oldLayer = this.gameObject.layer;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            ShotLayCast();
        }
        if (oldLayer != this.gameObject.layer)
        {
            ChangeColorToLayer();
            oldLayer = this.gameObject.layer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player && this.gameObject.layer != 6)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Seen");
            oldLayer = this.gameObject.layer;
            ChangeColorToLayer();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.transform.tag == "Monster")
        {
            if (collision.gameObject == player && (myPos != (Vector2)(this.transform.position) || playerPos != (Vector2)(player.transform.position)))
            {
                ShotLayCast();
                ChangeColorToLayer();
            }
        }
        else
        {
            if (collision.gameObject == player && (playerPos != (Vector2)(player.transform.position)))
            {
                ShotLayCast();
                ChangeColorToLayer();
            }
        }
    }
}
