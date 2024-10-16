using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
public class GameManager : MonoBehaviour
{
    public int floor;
    public int stairNumber=0;
    
    public static GameManager instance;
    
    public UnityEvent OnMapGenerate;
    
    public GameObject playerObj;
    public GameObject playerPrefab;
    public PlayerState playerState;

    List<int> visitedFloor = new List<int>();
    
    public MapMake mapScript;
    public InputManager inputManager;
    public MonsterManager monsterManager;
    public CSVReader csvReader = new CSVReader();
    public UIManager UIManager = new UIManager();
    public DataManager dataManager = new DataManager();
    public ItemManager item = new ItemManager();
    public SaveManager save = new SaveManager();
    public LogUI log;

    public Sprite baseSprite;
    private void Awake()
    {
        SetInstance();
        InstantiatePlayerObj();
        SetInputManager();
        DataRead();
    }

    void Start()
    {
        floor = 1;
        //item.AddKeysScripts();
        item.InitiateItem();
        EquipItem equipTest = new EquipItem();
        ConsumItem consumTest = new ConsumItem();
        dataManager.ReadDataByTiers();
        dataManager.NormalDist(equipTest);
        dataManager.NormalDist(consumTest);
        //item.ItemFactiry();
        save.MapSave();
        
        
    }

    void Update()
    {
        
    }
    public void InstantiatePlayerObj()
    {
        if (playerObj == null)
        {
            playerObj = Instantiate(playerPrefab);
            playerState = playerObj.transform.GetComponent<PlayerState>();
        }
    }
    public void SetPlayerNextFloor()
    {
        Vector3 genPos = mapScript.upStaires[this.stairNumber];
        genPos.z--;
        playerObj.transform.position = genPos;
    }
    void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(csvReader == null)
        {
            csvReader = new CSVReader();
        }
        if(dataManager == null)
        {
            dataManager = new DataManager();
        }
        if(mapScript == null)
        {
            mapScript = this.gameObject.transform.GetComponent<MapMake>();
        }
        
        if(log == null)
        {
            log = GameObject.Find("LogUI").gameObject.transform.GetComponent<LogUI>() ;
        }

        if(save.saveDirectory == null)
        {
            save.saveDirectory = Application.persistentDataPath;
        }
        if(monsterManager == null)
        {
            monsterManager = this.gameObject.transform.GetComponent<MonsterManager>();
        }
    }
    void AddFloor()
    {
        floor++;
        if (!visitedFloor.Contains(floor))
        {
            visitedFloor.Add(floor);
        }
    }
    void SetInputManager()
    {
        inputManager = transform.GetComponent<InputManager>();
    }

    void DataRead()
    {
        dataManager.equipMentsData = csvReader.Read("EquipItemData");
        dataManager.consumData = csvReader.Read("ConsumItemData");

    }
    

}
