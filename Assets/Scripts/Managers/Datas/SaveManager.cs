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




    public void JsonSave<T>(T saveClass,string fileName)
    {
        if(saveDirectory == null)
        {
            saveDirectory = Application.persistentDataPath;
        }
        string jsonString = JsonConvert.SerializeObject(saveClass);
        string path = Path.Combine(saveDirectory, fileName);
        Debug.Log(jsonString);
        MakeSaveData(path,jsonString);
    }

    void MakeSaveData(string filePath,string JsonData)
    {
        File.WriteAllText(filePath,JsonData);
        Debug.Log("SaveData write");
    }

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
        MapSave();
        ItemSave();
        MonsterSave();
    }
    public void LoadData()
    {
        MapLoad();
        ItemLoad();
        MonsterLoad();

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
        string fileName = GameManager.instance.floor + "_Floor_MapData";
        JsonSave<Dictionary<Vector2, TileType>>(GameManager.instance.mapScript.TileMap, fileName);
        Debug.Log("map save");
        LayerSave();
    }

    public void LayerSave()
    {
        string fileName = GameManager.instance.floor + "_Floor_LayerData";
        for (int i = 0; i < GameManager.instance.mapScript.mapObjects.Count; i++)
        {
            Vector2 pos = GameManager.instance.mapScript.mapObjects[i].transform.position;
            int layer = GameManager.instance.mapScript.mapObjects[i].gameObject.layer;
            GameManager.instance.mapScript.objLayers.Add(pos, layer);
        }
        JsonSave<Dictionary<Vector2, int>>(GameManager.instance.mapScript.objLayers, fileName);
    }
    public void MapLoad()
    {
        string fileName = GameManager.instance.floor + "_Floor_MapData";
        mapScript.TileMap = LoadFile<Dictionary<Vector2, TileType>>(fileName);
        LayerLoad();
    }
    public void LayerLoad()
    {
        string fileName = GameManager.instance.floor + "_Floor_LayerData";
        mapScript.objLayers = LoadFile<Dictionary<Vector2, int>>(fileName);
    }

    public void ItemSave()
    {
        string fileName = GameManager.instance.floor + "_Floor_ItemData";
        foreach (Vector2 key in itemManager.ItemPosList.Keys)
        {
            itemManager.itemSave.Add(key, itemManager.ItemPosList[key].myInfo);
        }
        JsonSave<Dictionary<Vector2, ItemBase>>(itemManager.itemSave, fileName);
        Debug.Log("ItemSave");
    }
    public void ItemLoad()
    {
        string fileName = GameManager.instance.floor + "_Floor_ItemData";
        itemManager.itemSave = LoadFile<Dictionary<Vector2, ItemBase>>(fileName);
    }


    public void MonsterSave()
    {
        string fileName = GameManager.instance.floor + "_Floor_MonsterData";
        for (int i = 0; i < monsterManager.monsterList.Count; i++)
        {
            MonsterState state = monsterManager.monsterList[i].transform.GetComponent<MonsterState>();
            monsterManager.monsterSaveData.Add(monsterManager.monsterList[i].gameObject.transform.position, state.myState);
        }
        JsonSave<Dictionary<Vector2, EntityState>>(monsterManager.monsterSaveData, fileName);
        Debug.Log("MonsterDataSave");
    }
    public void MonsterLoad()
    {
        string fileName = GameManager.instance.floor + "_Floor_MonsterData";
        monsterManager.monsterSaveData = LoadFile<Dictionary<Vector2, EntityState>>(fileName);
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
