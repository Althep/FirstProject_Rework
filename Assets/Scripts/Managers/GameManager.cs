using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public int floor;
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
    void InstantiatePlayerObj()
    {
        if (playerObj == null)
        {
            playerObj = Instantiate(playerPrefab, mapScript.upStaires[0], Quaternion.identity);
            DontDestroyOnLoad(playerObj);
        }
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
