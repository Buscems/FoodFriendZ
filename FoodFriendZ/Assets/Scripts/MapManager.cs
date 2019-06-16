using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public Vector2[] roomPosition;
    [HideInInspector]
    public string[] roomName;
    [HideInInspector]
    public GameObject[] roomManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int arraySize)
    {
        roomPosition = new Vector2[arraySize];
        roomName = new string[arraySize];
        roomManager = new GameObject[arraySize];
    }

    public void UpdateVariables(int arrayPos, GameObject room)
    {
        roomManager[arrayPos] = room;
        roomName[arrayPos] = roomManager[arrayPos].name;
        print(roomName[arrayPos] = roomManager[arrayPos].name);
        roomPosition[arrayPos] = roomManager[arrayPos].transform.position;
    }
}
