using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public int floor;
    public int stairNumber=0;
    public static GameManager instance;
    [SerializeField]GameManager asdf;
    public UnityEvent OnMapGenerate;
    public GameObject playerObj;
    public GameObject playerPrefab;
    List<int> visitedFloor = new List<int>();
    MapMake mapScript;
    private void Awake()
    {
        SetInstance();
        InstantiatePlayerObj();
    }

    void Start()
    {
        floor = 0;
        //OnMapGenerate.Invoke();
        
    }

    // Update is called once per frame
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
        Debug.Log("genPos"+genPos);
    }
    void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            asdf = instance;
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
}
