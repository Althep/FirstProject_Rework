using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class SaveManager
{
    public string saveDirectory;
    
    MapMake mapScript;
    MonsterManager monsterManager;
    ItemManager itemManager;
    List<string> Filenames = new List<string>();

    public void JsonSave<T>(T saveClass,string fileName)
    {
        if (saveDirectory == null)
        {
            saveDirectory = Application.persistentDataPath;
        }
        string jsonString = JsonUtility.ToJson(saveClass);
        string path = Path.Combine(saveDirectory, fileName);
        MakeSaveData(path,jsonString);
    }
    /*
    public void JsonSave<T>(T saveClass,string fileName)
    {
        if(saveDirectory == null)
        {
            saveDirectory = Application.persistentDataPath;
        }
        string jsonString = JsonConvert.SerializeObject(saveClass);
        string path = Path.Combine(saveDirectory, fileName);
        MakeSaveData(path,jsonString);
    }
    */
    void MakeSaveData(string filePath,string JsonData)
    {
        File.WriteAllText(filePath,JsonData);
        Filenames.Add(filePath);
        Debug.Log(filePath);
    }
    public T LoadFile<T>(string fileName)
    {
        if (saveDirectory == null)
        {
            saveDirectory = Application.persistentDataPath;
        }
        string filePath = Path.Combine(saveDirectory, fileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            T loadedData = JsonUtility.FromJson<T>(jsonString);
            return loadedData;
        }
        else
        {
            Debug.Log("LoadFile Failed");
            return default;
        }
    }
    /*
    public T LoadFile<T>(string fileName)
    {
        if(saveDirectory == null)
        {
            saveDirectory =  Application.persistentDataPath;
        }
        string filePath = Path.Combine(saveDirectory, fileName);
        if (File.Exists(filePath))
        {
            string JsonString = File.ReadAllText(filePath);
            T LoadedData = JsonConvert.DeserializeObject<T>(JsonString);
            return LoadedData;
        }
        else
        {
            Debug.Log("Load failed");
            return default;
        }
    }
    */
    public void Initalize()
    {
        if (mapScript == null)
        {
            mapScript = GameManager.instance.mapScript;
        }
        if (monsterManager == null)
        {
            monsterManager = GameManager.instance.monsterManager;
        }
        if (itemManager == null)
        {
            itemManager = GameManager.instance.item;
        }
    }
    public void SaveMap()
    {
        Initalize();
        MapSave();
        StairSave();
        ItemSave();
        MonsterSave();
    }
    public void LoadData()
    {
        Initalize();
        MapLoad();
        StairLoad();
        //ItemLoad();
        //MonsterLoad();
        
    }
    public void LoadMap()
    {
        LoadData();
        GameManager.instance.mapScript.LoadMapAtData();
        GameManager.instance.monsterManager.LoadMonsterData();
        GameManager.instance.item.LoadItemData();
        
    }
    public void MapSave()
    {
        mapScript.mapSaveData.Clear();
        string fileName = GameManager.instance.floor + "_Floor_MapData";
        
        foreach (Vector2 keys in mapScript.TileMap.Keys)
        {
            MapWrapper wrapped = new MapWrapper();
            wrapped.pos = keys;
            wrapped.tileType = mapScript.OriginMap[keys];
            if (!mapScript.mapSaveData.Contains(wrapped))
            {
                mapScript.mapSaveData.Add(wrapped);
            }
            
        }
        mapScript.mapData.saveData = mapScript.mapSaveData;
        Debug.Log($" Map Save Data Amount : {mapScript.mapSaveData.Count}!!!!");
        JsonSave<MapSaveDataWrapper>(GameManager.instance.mapScript.mapData, fileName);
        Debug.Log("map save");
        LayerSave();
    }

    public void LayerSave()
    {
        GameManager.instance.mapScript.objLayers.Clear();
        string fileName = GameManager.instance.floor + "_Floor_LayerData";
        for (int i = 0; i < GameManager.instance.mapScript.mapObjects.Count; i++)
        {
            int layer = GameManager.instance.mapScript.mapObjects[i].gameObject.layer;
            Vector2 vector = GameManager.instance.mapScript.mapObjects[i].transform.position;
            LayerWrapper wrapped = new LayerWrapper() { layer = layer, pos = vector};
            if (!mapScript.LayerSaveData.Contains(wrapped))
            {
                mapScript.LayerSaveData.Add(wrapped);
            }
        
        }
        GameManager.instance.mapScript.layerData.saveData = mapScript.LayerSaveData;
        JsonSave<LayerDataWrapper>(GameManager.instance.mapScript.layerData, fileName);
    }

    public void StairSave()
    {
        mapScript.stairSaveData.saveData.Clear();
        string fileName = GameManager.instance.floor + "_Floor_StairData";

        foreach(Vector2 key in mapScript.downStaires.Keys)
        {
            StairWrapper stairWrapped = new StairWrapper() { pos = key, stairType = StairType.downStair, stairNumber = mapScript.downStaires[key] };
            mapScript.stairList.Add(stairWrapped);
        }
        foreach(Vector2 key in mapScript.upStaires.Keys)
        {
            StairWrapper stairWrapped = new StairWrapper() { pos = key, stairType = StairType.upStair, stairNumber = mapScript.upStaires[key] };
            mapScript.stairList.Add(stairWrapped);
        }
        mapScript.stairSaveData.saveData = mapScript.stairList;
        Debug.Log($"Stair List Count : {mapScript.stairList.Count}");
        JsonSave<StairDataWrapper>(GameManager.instance.mapScript.stairSaveData, fileName);
    }

    public void StairLoad()
    {
        mapScript.stairSaveData.saveData.Clear();
        mapScript.upStaires.Clear();
        mapScript.downStaires.Clear();
        string fileName = GameManager.instance.floor + "_Floor_StairData";
        mapScript.stairSaveData = LoadFile<StairDataWrapper>(fileName);
        mapScript.stairList = mapScript.stairSaveData.saveData;

        for(int i = 0; i < mapScript.stairList.Count; i++)
        {
            Vector2 pos = mapScript.stairList[i].pos;
            int stairNumber = mapScript.stairList[i].stairNumber;
            StairType stairType = mapScript.stairList[i].stairType;
            switch (stairType)
            {
                case StairType.upStair:
                    mapScript.upStaires[pos] = stairNumber;
                    break;
                case StairType.downStair:
                    mapScript.downStaires[pos] = stairNumber;
                    break;
                default:
                    break;
            }
        }
    }
    public void MapLoad()
    {
        mapScript.mapData.saveData.Clear();
        mapScript.TileMap.Clear();
        mapScript.OriginMap.Clear();
        string fileName = GameManager.instance.floor + "_Floor_MapData";
        mapScript.mapData = LoadFile<MapSaveDataWrapper>(fileName);
        mapScript.mapSaveData = mapScript.mapData.saveData;
        for(int i = 0; i<mapScript.mapSaveData.Count; i++)
        {
            mapScript.TileMap[mapScript.mapSaveData[i].pos] = mapScript.mapSaveData[i].tileType;
            mapScript.OriginMap[mapScript.mapSaveData[i].pos] = mapScript.mapSaveData[i].tileType;
        }
        LayerLoad();
    }
    public void LayerLoad()
    {
        mapScript.layerData.saveData.Clear();
        //mapScript.objLayers.Clear();
        string fileName = GameManager.instance.floor + "_Floor_LayerData";
        mapScript.layerData = LoadFile<LayerDataWrapper>(fileName);
        mapScript.LayerSaveData = mapScript.layerData.saveData;
    }

    public void ItemSave()
    {
        Initalize();
        itemManager.ItemSaveData.Clear();

        string fileName = GameManager.instance.floor + "_Floor_ItemData";

        foreach(Vector2 key in itemManager.ItemPosList.Keys)
        {
            ItemWrapper itemWrapper = new ItemWrapper() { pos = key,itemBase = itemManager.ItemPosList[key].myInfo };
            itemManager.ItemSaveData.Add(itemWrapper);
        }
        itemManager.wrappedData.saveData = itemManager.ItemSaveData;
        Debug.Log($"Item Wrapped Data Save Data Count : {itemManager.wrappedData.saveData.Count}");
        JsonSave<ItemDataWrapper>(itemManager.wrappedData, fileName);
        Debug.Log("ItemSave");
    }
    public void ItemLoad()
    {
        itemManager.wrappedData = null;
        itemManager.ItemPosList.Clear();
        string fileName = GameManager.instance.floor + "_Floor_ItemData";
        itemManager.wrappedData = LoadFile<ItemDataWrapper>(fileName);
        itemManager.ItemSaveData = itemManager.wrappedData.saveData;
        for(int i = 0; i < itemManager.ItemSaveData.Count; i++)
        {
            itemManager.ItemPosList[itemManager.ItemSaveData[i].pos].myInfo = itemManager.ItemSaveData[i].itemBase;
        }
    }


    public void MonsterSave()
    {
        monsterManager.monsterSaveData.Clear();
        string fileName = GameManager.instance.floor + "_Floor_MonsterData";
        for (int i = 0; i < monsterManager.monsterList.Count; i++)
        {
            MonsterState state = monsterManager.monsterList[i].transform.GetComponent<MonsterState>();
            Vector2 vector = monsterManager.monsterList[i].gameObject.transform.position;
            MonsterWrapper monsterWrapper = new MonsterWrapper() {myState = state.myState,pos = vector };
            if (!monsterManager.monsterSaveData.Contains(monsterWrapper))
            {
                monsterManager.monsterSaveData.Add(monsterWrapper);
            }
        }
        monsterManager.wrappedData.saveData = monsterManager.monsterSaveData;
        Debug.Log($"monster Wrapped Data save Data Count :  {monsterManager.wrappedData.saveData.Count}");
        JsonSave<MonsterDataWrapper>(monsterManager.wrappedData, fileName);
        Debug.Log("MonsterDataSave");
    }
    public void MonsterLoad()
    {
        string fileName = GameManager.instance.floor + "_Floor_MonsterData";
        monsterManager.wrappedData = null;
        monsterManager.monsterList.Clear();

        monsterManager.wrappedData = LoadFile<MonsterDataWrapper>(fileName);
        monsterManager.monsterSaveData = monsterManager.wrappedData.saveData;
        for (int i =0; i < monsterManager.monsterSaveData.Count; i++)
        {

        }
    }


    public void InventorySave()
    {
        string fileName = GameManager.instance.playerState.name + "_Inventory";
        ItemInventory playerInven = GameManager.instance.playerState.myInventory;
        for(int i = 0; i<playerInven.Inventory.Count; i++)
        {
            playerInven.invenSave.Add(playerInven.Inventory[i].myInfo);
        }
        JsonSave<List<ItemBase>>(playerInven.invenSave,fileName);
        Debug.Log("PlayerInventory Saved");
    }
    public void InventoryLoad()
    {
        string fileName = GameManager.instance.playerState.name + "_Inventory";
        ItemInventory playerInven = GameManager.instance.playerState.myInventory;
        playerInven.invenSave = LoadFile<List<ItemBase>>(fileName);
    }

    

    
}
