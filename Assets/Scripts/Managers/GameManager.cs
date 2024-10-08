using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public int floor;
    public int stairNumber=0;
    
    public static GameManager instance;
    
    public UnityEvent OnMapGenerate;
    
    public GameObject playerObj;
    public GameObject playerPrefab;

    List<int> visitedFloor = new List<int>();
    
    public MapMake mapScript;
    public InputManager inputManager;
    public CSVReader csvReader = new CSVReader();
    public UIManager UIManager = new UIManager();
    public DataManager dataManager = new DataManager();
    public ItemManager item = new ItemManager();
    
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
        item.ItemFactiry();

        
        
    }

    void Update()
    {
        
    }
    public void InstantiatePlayerObj()
    {
        if (playerObj == null)
        {
            Instantiate(playerPrefab);
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
        mapScript = this.gameObject.transform.GetComponent<MapMake>();
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
