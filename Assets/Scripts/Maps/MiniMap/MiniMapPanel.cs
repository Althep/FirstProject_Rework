using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MiniMapPanel : MonoBehaviour
{

    public Dictionary<Vector2,MapCell>  mapCells;
    public Dictionary<Vector2, TileType> mapData;
    public static MiniMapPanel instance;
    void Start()
    {
        SetInstance();
    }

    public void SetInstance()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }
    public void SetCellPosition()
    {
        int count = 0;

        foreach (Vector2 pos in mapData.Keys.ToList())
        {
            MapCell mapCell = transform.GetChild(count).transform.GetComponent<MapCell>();
            Vector2 transePos = new Vector2(pos.y, pos.x);
            mapCell.SetPosition(transePos);
            mapCells[transePos] = mapCell;
            mapCells[transePos].SetTilePosition(mapData[pos]);
            count++;
        }
    }
    public void UpDateMinimap()
    {
        MapMake mapScript = GameManager.instance.mapScript;
        foreach(Vector2 pos in mapScript.objLayers.Keys)
        {
            if(mapCells[pos].tileLayer != mapScript.objLayers[pos])
            {
                mapCells[pos].tileLayer = mapScript.objLayers[pos];
                CellColorChange(pos, mapScript.objLayers[pos]);
            }
            
        }
        Debug.Log("minimap Update");
    }
    public void CellColorChange(Vector2 pos, int layer)
    {
        if(mapData == null)
        {
            mapData = GameManager.instance.mapScript.TileMap;
        }
        if (mapData.ContainsKey(pos))
        {
            if(layer == 7)
            {
                layer = 8;
            }
            mapCells[pos].tileLayer = layer;
            mapCells[pos].ChangeToLayer();

        }
    }
    public void MiniMapReset()
    {
        foreach(Vector2 key in mapCells.Keys)
        {
            mapCells[key].tileLayer = 6;
            CellColorChange(key,6);
        }
    }

}
