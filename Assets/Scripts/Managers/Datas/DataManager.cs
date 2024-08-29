using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    CSVReader CSVReader = new CSVReader();

    Dictionary<string, List<object>> _monsterDatas;
    public Dictionary<string,List<object>> monsterData { get { return _monsterDatas; } }

    Dictionary<string, List<object>> _ConsumItemData;
    public Dictionary<string, List<object>> consumItemData { get { return _ConsumItemData; } }

    Dictionary<string, List<object>> _EquipMentsData;
    public Dictionary<string, List<object>> equipMentsData { get { return _EquipMentsData; } }

    Dictionary<string, List<object>> _magicDatas;
    public Dictionary<string, List<object>> magicDatas { get { return _magicDatas; } }

    
    void ReadMonsterData()
    {
        string path = "\\MonsterData";
        _monsterDatas = CSVReader.CSVReade(path);
    }

    void ReadItemData()
    {
        string path = "\\ConsumItemData";
        _ConsumItemData = CSVReader.CSVReade(path);

        path = "\\EquipItemData"; ;
        _EquipMentsData = CSVReader.CSVReade(path);

    }

    void ReadMagicData()
    {
        string path = "\\MagicData";
        _magicDatas = CSVReader.CSVReade(path);
    }
}
