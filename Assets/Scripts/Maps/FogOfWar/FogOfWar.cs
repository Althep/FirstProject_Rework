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
        player = GameObject.FindWithTag("Player");
        oldLayer = this.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColorToLayer()
    {
        switch (this.gameObject.layer)
        {
            case 6: // UnSeen
                myRenderer.color = Color.black;
                break;
            case 7:// visited
                if (this.transform.tag != "Monster")
                {
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
        oldLayer = this.gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShotLayCast();
        if (oldLayer != this.gameObject.layer)
        {
            ChangeColorToLayer();
            oldLayer = this.gameObject.layer;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Plaer"&&this.gameObject.layer!=6)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Visited");
            oldLayer = this.gameObject.layer;
            ChangeColorToLayer();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(this.transform.tag == "Monster")
        {
            if(collision.transform.tag == "Player" && (myPos != (Vector2)(this.transform.position)))
            {
                ShotLayCast();
                ChangeColorToLayer();
            }
        }
        else
        {
            if (collision.transform.tag == "Player" && (playerPos != (Vector2)(player.transform.position)))
            {
                ShotLayCast();
                ChangeColorToLayer();
            }
        }
        
    }
}
