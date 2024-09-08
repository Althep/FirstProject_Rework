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
    
    MapMake mapScript;
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
    }

    void Start()
    {
        floor = 0;
        //item.AddKeysScripts();
        //item.InitiateItem();
        dataManager.ReadItemByTiers();


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

    /*
    public void NormalDist(string kind)
    {
        float mu;//평균
        float sigma;//표준편차
        int floor;
        floor = GameManager.instance.nowFloor;
        if (kind == "equip")
        {
            mu = Mathf.Log(floor, 3f);
            sigma = Mathf.Log(floor + 1, 5) + 1f;
            rateValuePrint(mu, sigma, kind);
        }
        else if (kind == "monster")
        {
            mu = Mathf.Log(floor, 2f);//평균
            sigma = Mathf.Log(floor + 1, 10);//표준편차
            rateValuePrint(mu, sigma, kind);
        }
        else if (kind == "consum")
        {
            mu = Mathf.Log(floor, 4f);
            sigma = Mathf.Log(floor + 1, 4) + 1.07f;
            rateValuePrint(mu, sigma, kind);
        }
    }
    void rateValuePrint(float mu, float sigma, string kind)
    {
        float rateValue = 0;
        float temp;
        float temp1;
        float temp2;
        float sumrate=0;
        List<float> tempList = new List<float>();
        for (int j = 0; j < tierList.Count; j++)
        {
            rateValue = 1;
            temp = 1 / (sigma * math.pow((math.PI * 2), 1 / 2));
            temp2 = -math.pow((tierList[j] - mu), 2) / (2 * math.pow(sigma, 2));
            temp1 = math.pow(math.E, temp2);
            rateValue = temp1 / (temp * sigma);
            tempList.Add(rateValue);
            sumrate += rateValue;
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            if (kind == "equip")
                equipDatas.Add((tempList[i] / sumrate) * 10000);
            else if (kind == "monster")
                monsterDatas.Add((tempList[i] / sumrate) * 1000);
            else if( kind == "consum")
                consumDatas.Add((tempList[i] / sumrate) * 1000);
        }
    }
    */
}
