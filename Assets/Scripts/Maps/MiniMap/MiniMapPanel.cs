using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPanel : MonoBehaviour
{

    public MapCell[,] mapCells;
    public TileType[,] mapData;
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
        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for(int x = 0; x < mapData.GetLength(1); x++)
            {
                MapCell mapCell = transform.GetChild(count).transform.GetComponent<MapCell>();
                mapCell.SetPosition(x,y);
                mapCells[y, x] = mapCell;
                mapCells[y, x].SetTilePosition(mapData[y, x]);
                count++;
            }
        }
    }

    
    public void CellColorChange(int y, int x,int layer)
    {
        if(y>=0 && y<mapData.GetLength(0) && x>=0 && x< mapData.GetLength(1))
        {
            mapCells[y, x].tileLayer = layer;
            mapCells[y, x].ChangeToLayer();

        }

    }
}
