using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapCell : MonoBehaviour
{
    [SerializeField]Vector2 myPos;
    public int tileLayer;
    public TileType tileType;
    Image mapCell;
    Image upperOBJ;
    // Start is called before the first frame update
    void Start()
    {
        SetMapCell();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMapCell()
    {
        mapCell = transform.GetComponent<Image>();
    }

    public void SetPosition(Vector2 pos)
    {
        myPos = pos;
    }
    public void SetTilePosition(TileType tile)
    {
        tileType = tile;
    }
    public void ChangeToLayer()
    {
        switch (tileLayer)
        {
            case 6:
                {
                    //UnSeenTile();
                    break;
                }
            case 7:
                {
                    //SeenTile();
                    //Seen
                    break;
                }
            case 8:
                {
                    tileType = GameManager.instance.mapScript.TileMap[myPos];
                    ChangeToType();
                    break;
                }
            default:
                break;
        }
    }
    void UnSeenTile()
    {
        mapCell.color = Color.black;
    }
    
    void SeenTile()
    {
        if(tileType == TileType.monster)
        {
            tileType = GameManager.instance.mapScript.TileMap[myPos];
            ChangeToLayer();
        }
    }
    void ChangeToType()
    {
        switch (tileType)
        {
            case TileType.tile:
                mapCell.color = Color.white;
                break;
            case TileType.wall:
                mapCell.color = Color.gray;
                break;
            case TileType.door:
                mapCell.color = Color.cyan;
                break;
            case TileType.upstair:
                mapCell.color = Color.yellow;
                break;
            case TileType.downstair:
                mapCell.color = Color.yellow;
                break;
            case TileType.monster:
                mapCell.color = Color.red;
                Debug.Log("monsterColor");
                //UpperOBJColorChange();
                break;
            case TileType.player:
                mapCell.color = Color.green;
                break;
            case TileType.item:
                mapCell.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
