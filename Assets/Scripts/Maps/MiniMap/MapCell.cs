using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapCell : MonoBehaviour
{
    [SerializeField]int thisx;
    [SerializeField]int thisy;
    public int tileLayer;
    TileType tileType;
    Image mapCell;
    Image upperOBJ;
    // Start is called before the first frame update
    void Start()
    {
        SetMapCell();
        SetUpperOBJ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMapCell()
    {
        mapCell = transform.GetComponent<Image>();
        Debug.Log(mapCell);
    }
    public void SetUpperOBJ()
    {
        upperOBJ = gameObject.transform.GetChild(0).transform.GetComponent<Image>();
    }
    public void SetPosition(int x, int y)
    {
        thisx = x;
        thisy = y;
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
                    UnSeenTile();
                    //UnSeen
                    break;
                }
            case 7:
                {
                    SeenTile();
                    //Seen
                    break;
                }
            case 8:
                {
                    //Insight
                    ChangeToType();
                    break;
                }
            default:
                break;
        }
    }
    void UpperOBJColorChange()
    {
        switch (tileType)
        {
            case TileType.monster:
                upperOBJ.color = Color.red;
                break;
            case TileType.player:
                upperOBJ.color = Color.green;
                break;
            case TileType.item:
                upperOBJ.color = Color.green;
                break;
            default:
                break;
        }
    }
    void UpperOBJColorRemove()
    {
        Color color = new Color(0,0,0,0);
        upperOBJ.color = color;
    }
    void UnSeenTile()
    {
        mapCell.color = Color.black;
    }
    
    void SeenTile()
    {
        //갱신하지않음
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
            case TileType.stair:
                mapCell.color = Color.yellow;
                break;
            case TileType.monster:
                //UpperOBJColorChange();
                break;
            case TileType.player:
                //UpperOBJColorChange();
                break;
            case TileType.item:
                //UpperOBJColorChange();
                break;
            default:
                break;
        }
    }
}
