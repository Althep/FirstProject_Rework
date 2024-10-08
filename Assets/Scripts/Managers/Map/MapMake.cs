using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum TileType
{
    tile,
    wall,
    door,
    stair,
    monster,
    player,
    item
}
public class MapMake : MonoBehaviour
{
    // Start is called before the first frame update
    //public TileType[,] map;
    public int xSize;
    public int ySize;
    //int minMapSize=30;
    //int maxMapSize=60;
    int dividingMin = 80;
    int dividingMax = 120;
    int maxCount = 5;

    MonsterManager monsterManager;
    public MiniMapPanel miniMap;

    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject upStairPrefab;
    public GameObject downStairPrefab;

    public Dictionary<Vector2, TileType> TileMap = new Dictionary<Vector2, TileType>();
    public Dictionary<Vector2, TileType> LivingEntityMap = new Dictionary<Vector2, TileType>();

    public List<Vector2> tilePosList = new List<Vector2>();
    public List<Vector2> upStaires = new List<Vector2>();
    public List<Vector2> downStaires = new List<Vector2>();

    private void Awake()
    {
        monsterManager = this.transform.GetComponent<MonsterManager>();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void MapGenerate()
    {
        MapInitiate();
        MapDivide(0, 0, xSize, ySize, 0);
        TwistingDungeon();
        MakeStairPos();
        ObjectInstantiate();
        MiniMapMaping();
    }
    void ObjectInstantiate()
    {
        foreach (Vector2 pos in TileMap.Keys)
        {
            Instantiate(tilePrefab, pos, Quaternion.identity);

            switch (TileMap[pos])
            {
                case TileType.tile:
                    break;
                case TileType.wall:
                    Instantiate(wallPrefab, pos, Quaternion.identity);
                    break;
                case TileType.door:
                    Instantiate(doorPrefab, pos, Quaternion.identity);
                    break;
                case TileType.stair:
                    break;
                case TileType.monster:
                    break;
                case TileType.player:
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < upStaires.Count; i++)
        {
            GameObject go = Instantiate(upStairPrefab, upStaires[i], Quaternion.identity);
            go.transform.GetComponent<Stair>().stairNumber = i;
            go = Instantiate(downStairPrefab, downStaires[i], Quaternion.identity);
            go.transform.GetComponent<Stair>().stairNumber = i;
        }
        /*
        for(int i = -1; i <=map.GetLength(0); i++)
        {
            Instantiate(wallPrefab, new Vector2(-1,i), Quaternion.identity);
            Instantiate(wallPrefab, new Vector2( map.GetLength(1), i), Quaternion.identity);
        }
        for(int i = -1; i <= map.GetLength(1); i++)
        {
            Instantiate(wallPrefab, new Vector2(i,-1), Quaternion.identity);
            Instantiate(wallPrefab, new Vector2( i, map.GetLength(0)), Quaternion.identity);
        }
        */
    }
    public void MapInitiate()
    {
        //RandomSize(minMapSize, maxMapSize);
        xSize = 32;
        ySize = 32;
        TileMap = new Dictionary<Vector2, TileType>();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector2 newPos = new Vector2(x, y);
                TileMap.Add(newPos, TileType.tile);
            }
        }
        //맵크기 초기화
        //miniMap = GameObject.Find("Canvas").transform.GetChild(2).transform.GetComponent<MiniMapPanel>();
        miniMap = GameObject.Find("MiniMap").transform.GetComponent<MiniMapPanel>();
    }
    void RandomSize(int min, int max)
    {
        ySize = UnityEngine.Random.Range(min, max + 1);
        xSize = UnityEngine.Random.Range(min, max + 1);
    }

    void MiniMapMaping()
    {
        miniMap.mapData = TileMap;
        miniMap.mapCells = new Dictionary<Vector2, MapCell>();
        miniMap.SetCellPosition();
    }

    void MapDivide(int startX, int startY, int endX, int endY, int count)
    {//재귀함수로 맵 나누기
        if (count < maxCount && endX - startX > 5 && endY - startY > 5)
        {
            if ((endX - startX) > (endY - startY))
            {
                count++;
                int divided = ((startX + endX) * UnityEngine.Random.Range(dividingMin, dividingMax)) / 200;
                divided = RerollDivied(startX, endX, divided);
                for (int y = startY; y < endY; y++)
                {
                    Vector2 Pos = new Vector2(divided, y);
                    TileMap[Pos] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startY, endY);
                Vector2 doorPos = new Vector2(divided, door);
                TileMap[doorPos] = TileType.door;
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
                int divided = ((startY + endY) * UnityEngine.Random.Range(dividingMin, dividingMax)) / 200;
                divided = RerollDivied(startY, endY, divided);
                for (int x = startX; x < endX; x++)
                {
                    Vector2 pos = new Vector2(x, divided);
                    TileMap[pos] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startX, endX);
                Vector2 doorPos = new Vector2(door, divided);
                TileMap[doorPos] = TileType.wall;
                if (divided - startY > 3)
                {
                    MapDivide(startX, startY, endX, divided, count);
                }
                if (endY - divided > 3)
                {
                    MapDivide(startX, divided, endX, endY, count);
                }
            }
        }
    }//BSP
    int RerollDivied(int start, int end, int divide)
    {
        if (start >= divide || end <= divide || start + 1 == divide || end - 1 == divide)
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
        foreach (Vector2 pos in TileMap.Keys.ToList())
        {
            switch (TileMap[pos])
            {
                case TileType.tile:
                    constant = 10;
                    if (UnityEngine.Random.Range(0, 100) < constant)
                    {
                        TileMap[pos] = TileType.wall;
                        if (tilePosList.Contains(pos))
                        {
                            tilePosList.Remove(pos);
                        }
                    }
                    else
                    {
                        if (!tilePosList.Contains(pos))
                        {
                            tilePosList.Add(pos);
                        }
                        
                    }
                    break;
                case TileType.wall:
                    constant = 30;
                    if (UnityEngine.Random.Range(0, 100) < constant)
                    {
                        TileMap[pos] = TileType.tile;
                        if (!tilePosList.Contains(pos))
                        {
                            tilePosList.Add(pos);
                        }
                    }
                    break;
                case TileType.door:
                    break;
                case TileType.stair:
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
    /*
    public void TileInfoChange(Vector2 changePos,TileType tileType,List<Vector2> origin,List<Vector2> change)
    {
        //생성때에만 사용
        TileMap[changePos] = tileType;
        
        if (origin.Contains(changePos))
        {
            origin.Remove(changePos);
        }
        if (!change.Contains(changePos))
        {
            change.Add(changePos);
            map[(int)changePos.y, (int)changePos.x] = tileType;
        }
    }
    */
    public void EntityMove(Vector2 originPos, Vector2 nextPos, TileType myType)
    {
        TileMap[originPos] = TileType.tile;
        TileMap[nextPos] = myType;
    }
    public void TileTypeChange(Vector2 pos, TileType myType)
    {
        TileMap[pos] = myType;
        if (tilePosList.Contains(pos))
        {
            tilePosList.Remove(pos);
        }
    }
    public void MakeStairPos()
    {
        for (int i = 0; i < 3; i++)
        {
            int temp = UnityEngine.Random.Range(0, tilePosList.Count);
            upStaires.Add(tilePosList[temp]);
            tilePosList.Remove(tilePosList[temp]);
            temp = UnityEngine.Random.Range(0, tilePosList.Count);
            downStaires.Add(tilePosList[temp]);
            tilePosList.Remove(tilePosList[temp]);
        }
        for (int i = 0; i < 3; i++)
        {
            tilePosList.Add(upStaires[i]);
            tilePosList.Add(downStaires[i]);
        }
    }
    void SetPlayer()
    {
        int temp = Random.Range(0, downStaires.Count);

    }
    bool IsInSize(Vector2 Pos)
    {
        bool temp = true;
        if (Pos.x > xSize || Pos.y > ySize || Pos.x < 0 || Pos.y < 0)
        {
            temp = false;
        }
        return temp;
    }
}
