using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected bool canGet = false;
    public ItemBase myInfo;


    protected  virtual void Update()
    {
        if(canGet && Input.GetKeyDown(KeyCode.G))//추후 키세팅 변경 할 수 있게되면 바꿔야함
        {
            GetNewItem();
            RemoveItem();
        }
    }
    protected void RemoveItem()
    {
        GameManager.instance.item.ItemPosList.Remove(this.gameObject.transform.position);
        GameManager.instance.mapScript.TileMap[this.gameObject.transform.position] = TileType.tile;
        Destroy(this.gameObject);

    }
    protected virtual void GetNewItem()
    {
        if(myInfo is EquipItem)
        {
            Item item = this;
            GameManager.instance.playerState.myInventory.GetEquipItem(item);
            RemoveItem();
        }
        else if(myInfo is ConsumItem)
        {
            Item item = this;
            GameManager.instance.playerState.myInventory.GetConsumItem(item);
            RemoveItem();
        }
        else
        {
            Debug.Log("myinfo Type Error");
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            canGet = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canGet = false;
        }
    }

}
