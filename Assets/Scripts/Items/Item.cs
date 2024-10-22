using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
[System.Serializable]
public class Item : MonoBehaviour
{
    [JsonProperty]
    [SerializeField]protected bool canGet = false;
    public ItemBase myInfo;


    protected  virtual void Update()
    {
        if(canGet && Input.GetKeyDown(KeyCode.G))//���� Ű���� ���� �� �� �ְԵǸ� �ٲ����
        {
            GetNewItem();
            //RemoveItem();
        }
    }
    protected void RemoveItem()
    {
        GameManager.instance.item.ItemPosList.Remove(this.gameObject.transform.position);
        if(GameManager.instance.mapScript.TileMap[this.gameObject.transform.position] == TileType.item)
        {
            GameManager.instance.mapScript.TileMap[this.gameObject.transform.position] = TileType.tile;
        }
        
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && collision is BoxCollider2D) // �÷��̾��� �þ߸� �����ϴ� CircleCollder ������ �þ��� �������� ��� ����Ǵ� ���� �߻� �ݶ��̴� ���� �ʿ��� ����
        {
            canGet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canGet = false;
        }
    }

}
