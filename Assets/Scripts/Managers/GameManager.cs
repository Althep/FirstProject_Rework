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
    public UIManager UIManager = new UIManager();
    private void Awake()
    {
        SetInstance();
        InstantiatePlayerObj();
        SetInputManager();
    }

    void Start()
    {
        floor = 0;
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
}
