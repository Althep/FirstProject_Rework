using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum TileType
{
    tile,
    wall,
    door,
    water,
    monster,
    player
}
public class MapMake : MonoBehaviour
{
    // Start is called before the first frame update
    public TileType[,] map;
    public int xSize;
    public int ySize;
    int minMapSize=30;
    int maxMapSize=60;
    int dividingMin = 80;
    int dividingMax = 120;
    int maxCount = 5;
    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject AstarTest;
    public List<Vector2> tilePosList = new List<Vector2>();
    public List<Vector2> mosnterPosList = new List<Vector2>();
    List<Vector2> wallPosList = new List<Vector2>();
    List<Vector2> upStaires = new List<Vector2>();
    List<Vector2> downStaires = new List<Vector2>();
    List<Vector2> doorPosList = new List<Vector2>();
    
    private void Awake()
    {
        MapInitiate();
        
    }
    void Start()
    {
        Debug.Log(map.GetLength(0));
        Debug.Log(map.GetLength(1));
        MapDivide(0, 0, xSize, ySize, 0);
        TwistingDungeon();
        ObjectInstantiate();
        Vector2 testStart = new Vector2();
        testStart.x = xSize - 1;
        testStart.y = ySize - 1;
        Instantiate(AstarTest, testStart, Quaternion.identity);
    }

    void Update()
    {
        
    }
   
    void ObjectInstantiate()
    {
        for(int i = 0; i < tilePosList.Count; i++)
        {
            Instantiate(tilePrefab, tilePosList[i], Quaternion.identity);
        }
        for(int i = 0; i < wallPosList.Count; i++)
        {
            Instantiate(wallPrefab, wallPosList[i], Quaternion.identity);
        }
        for(int i = 0; i< doorPosList.Count; i++)
        {
            Instantiate(doorPrefab, doorPosList[i], Quaternion.identity);
        }
    }

    public void MapInitiate()
    {
        RandomSize(minMapSize, maxMapSize);
        map = new TileType[ySize,xSize];
        for(int y = 0; y < ySize; y++)
        {
            for(int x = 0; x < xSize; x++)
            {
                map[y, x] = TileType.tile;
            }
        }
        //맵크기 초기화
        
    }
    void RandomSize(int min,int max)
    {
        ySize = UnityEngine.Random.Range(min,max+1);
        xSize = UnityEngine.Random.Range(min, max + 1);
    }
    void MapDivide(int startX,int startY,int endX,int endY,int count)
    {
        if (count < maxCount&&endX-startX>5 && endY-startY>5)
        {
            if ((endX - startX) > (endY - startY))
            {
                count++;
                int divided = ((startX + endX) * UnityEngine.Random.RandomRange(dividingMin, dividingMax))/200;
                divided = RerollDivied(startX, endX, divided);
                for (int y = startY; y < endY; y++)
                {
                    map[y, divided] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startY,endY);
                TileInfoChange(new Vector2(divided, door), TileType.door,wallPosList,doorPosList);
                if (divided - startX > 3)
                {
                    MapDivide(startX, startY, divided, endY, count);
                }
                if (endX - divided > 3)
                {
                    MapDivide(divided, startY, endX, endY, count);
                }
            }
            else
            {
                count++;
                int divided = ((startY + endY) * UnityEngine.Random.RandomRange(dividingMin, dividingMax))/200;
                divided = RerollDivied(startY, endY, divided);
                for (int x = startX; x < endX; x++)
                {
                    map[divided, x] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startX, endX);
                TileInfoChange(new Vector2(door,divided), TileType.door,wallPosList,doorPosList);
                if (divided - startY>3)
                {
                    MapDivide(startX, startY, endX, divided, count);
                }
                if (endY - divided > 3)
                {
                    MapDivide(startX, divided, endX, endY, count);
                }
            }
        }
    }
    int RerollDivied(int start, int end, int divide)
    {
        if (start >= divide || end <= divide||start+1==divide||end-1==divide)
        {
            return (start + end) / 2;
        }
        else
        {
            return divide;
        }
    }

    void TwistingDungeon()
    {
        int constant;
        for(int y= 0; y<ySize;y++)
            for(int x = 0; x < xSize; x++)
            {
                switch (map[y,x])
                {
                    case TileType.tile:
                        constant = 10;
                        Vector2 Pos = new Vector2();
                        if (UnityEngine.Random.Range(0, 100) < constant)
                        {
                            map[y, x] = TileType.wall;
                            Pos.x = x;
                            Pos.y = y;
                            TileInfoChange(Pos, TileType.wall, tilePosList, wallPosList);
                        }
                        else
                        {
                            Pos.x = x;
                            Pos.y = y;
                            tilePosList.Add(Pos);
                        }
                        break;
                    case TileType.wall:
                        constant = 30;
                        if (UnityEngine.Random.Range(0, 100) < constant)
                        {
                            map[y, x] = TileType.tile;
                            Pos.x = x;
                            Pos.y = y;
                            TileInfoChange(Pos, TileType.tile, wallPosList, tilePosList);
                        }
                        else
                        {
                            Pos.x = x;
                            Pos.y = y;
                            wallPosList.Add(Pos);
                        }
                        break;
                    case TileType.door:
                        break;
                    case TileType.water:
                        break;
                    case TileType.monster:
                        break;
                    case TileType.player:
                        break;
                    default:
                        break;
                }
            }
    }
    public void TileInfoChange(Vector2 changePos,TileType tileType,List<Vector2> origin,List<Vector2> change)
    {
        map[(int)changePos.y, (int)changePos.x] = tileType;
        
        if (origin.Contains(changePos))
        {
            origin.Remove(changePos);
        }
        if (!change.Contains(changePos))
        {
            change.Add(changePos);
        }

    }

    public void MakeStairPos()
    {
        for(int i =0; i < 3; i++)
        {
            int temp = UnityEngine.Random.RandomRange(0,tilePosList.Count);
            upStaires.Add(tilePosList[temp]);
            tilePosList.Remove(tilePosList[temp]);
            temp = UnityEngine.Random.RandomRange(0, tilePosList.Count);
            downStaires.Add(tilePosList[temp]);
            tilePosList.Remove(tilePosList[temp]);
        }
        for(int i = 0; i < 3; i++)
        {
            tilePosList.Add(upStaires[i]);
            tilePosList.Add(downStaires[i]);
        }

    }

}
