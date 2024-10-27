using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
[SerializeField]

public class GameManager : MonoBehaviour
{
    public int floor=1;
    public int stairNumber=0;
    public string nextScene;

    public static GameManager instance;
    
    public UnityEvent OnMapGenerate;
    public UnityEvent OnVisitedFloor;

    public GameObject playerObj;
    public GameObject playerPrefab;
    public PlayerState playerState;

    public List<int> visitedFloor = new List<int>();

    public GameObject canvas;

    public MapMake mapScript;
    public InputManager inputManager;
    public MonsterManager monsterManager;
    public CSVReader csvReader = new CSVReader();
    public UIManager UIManager = new UIManager();
    public DataManager dataManager = new DataManager();
    public ItemManager item = new ItemManager();
    public SaveManager save = new SaveManager();
    public LogUI log;
    public TurnManager turnManager;

    public Sprite baseSprite;
    public StairType stairType =StairType.downStair;

    private void Awake()
    {
        floor = 1;
        stairType = StairType.downStair;
        SetInstance();
        InstantiatePlayerObj();
        SetInputManager();
        DataRead();
        
    }

    void Start()
    {
        visitedFloor.Add(floor);
        //item.AddKeysScripts();
        item.InitiateItem();
        EquipItem equipTest = new EquipItem();
        ConsumItem consumTest = new ConsumItem();
        dataManager.ReadDataByTiers();
        dataManager.NormalDist();
        //item.ItemFactiry();
        
        SceneManager.sceneLoaded += StairFunction;
    }

    void Update()
    {
        
    }
    public void MapDataReset()
    {
        item.wrappedData.equipSaved.Clear();
        item.wrappedData.consumSaved.Clear();
        mapScript.mapSaveData.Clear();
        mapScript.OriginMap.Clear();
        mapScript.mapObjects.Clear();
        monsterManager.monsterList.Clear();
    }
    public void StairFunction(Scene scene,LoadSceneMode mode)
    {
        
        switch (stairType)
        {
            case StairType.upStair:
                floor-=1;
                break;
            case StairType.downStair:
                floor+=1;
                break;
            default:
                break;
        }
        //monsterManager.monsterPos.Clear();
        FloorChange();
        SetPlayerNextFloor();
        
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
        Vector3 genPos = new Vector3();
        switch (stairType)
        {
            case StairType.upStair:
                foreach (Vector2 value in mapScript.downStaires.Keys)
                {
                    if (mapScript.downStaires[value] == stairNumber)
                    {
                        genPos = value;
                    }
                }
                break;
            case StairType.downStair:
                foreach (Vector2 value in mapScript.upStaires.Keys)
                {
                    if (mapScript.upStaires[value] == stairNumber)
                    {
                        genPos = value;
                    }
                }
                break;
            default:
                Debug.Log("StairType Error");
                break;
        }
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
            return;
        }
        if(canvas == null)
        {
            canvas = GameObject.Find("Canvas").gameObject;
            DontDestroyOnLoad(canvas);
        }
        else
        {
            Destroy(GameObject.Find("Canvas").gameObject);
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
        dataManager.consumFunction.SetFunctions();

        if(turnManager == null) 
        {
            turnManager = this.gameObject.transform.GetComponent<TurnManager>();
        }
        dataManager.LoadAssetBundle();
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


    public void FloorChange()
    {
        
        switch (stairType)
        {
            case StairType.upStair:
                SetMap();
                break;
            case StairType.downStair:
                SetMap();
                break;
            default:
                break;
        }
    }

    public void SetMap()
    {
        //MapDataReset();
        if (visitedFloor.Contains(floor))
        {
            save.LoadMap();
            mapScript.miniMap.UpDateMinimap();
        }
        else
        {
            visitedFloor.Add(floor);
            OnMapGenerate.Invoke();
            mapScript.miniMap.MiniMapReset();
        }
        
    }

}
