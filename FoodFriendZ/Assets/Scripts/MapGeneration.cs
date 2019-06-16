using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    #region Inspector Elements
    [SerializeField]
    [Header("Number of rooms to be generated")]
    private int m_numRooms;

    [SerializeField]
    [Header("Rooms with a Up door")]
    private GameObject[] m_upDoors;

    [SerializeField]
    [Header("Rooms with a Down door")]
    private GameObject[] m_downDoors;

    [SerializeField]
    [Header("Rooms with a Left door")]
    private GameObject[] m_leftDoors;

    [SerializeField]
    [Header("Rooms with a Right door")]
    private GameObject[] m_rightDoors;
    #endregion Inspector Elements

    #region Private Variables
    private MapManager mapManager;

    private int randNum = 0;
    private int randNum2 = 0;
    private int roomTracker = 0;

    private bool mapGenerated = false;
    #endregion Private Variables


    // Start is called before the first frame update
    void Start()
    {
        mapManager = GetComponent<MapManager>();
        mapManager.Initialize(m_numRooms);

        mapManager.UpdateVariables(roomTracker, GetRandomRoom(0));
        Instantiate(mapManager.roomManager[roomTracker], new Vector2(0, 0), Quaternion.identity);
        roomTracker += 1;
    }

    // Update is called once per frame
    void Update()
    {
        while (roomTracker < m_numRooms)
        {
            for (int i = 0; i < roomTracker; i++)
            {
                bool[] tempDoorsUnoccupied = DoorsUnoccupied(i);
                //up
                if (tempDoorsUnoccupied[0] == true)
                {
                    int randNum = (int)Random.Range(0, 2);
                    if (randNum == 0)
                    {
                        if (CheckIfRoomExists(i, 0) == false)
                        {
                            GameObject roomToBeAdded = Instantiate(GetDownRoom(), new Vector2(mapManager.roomPosition[i].x, mapManager.roomPosition[i].y + 10), Quaternion.identity);
                            mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                            roomTracker += 1;
                            if(roomTracker == m_numRooms)
                            {
                                continue;
                            }
                        }
                    }
                }
                //down
                if (tempDoorsUnoccupied[1] == true)
                {
                    int randNum = (int)Random.Range(0, 2);
                    if (randNum == 0)
                    {
                        if (CheckIfRoomExists(i, 1) == false)
                        {
                            GameObject roomToBeAdded = Instantiate(GetUpRoom(), new Vector2(mapManager.roomPosition[i].x, mapManager.roomPosition[i].y - 10), Quaternion.identity);
                            mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                            roomTracker += 1;
                            if (roomTracker == m_numRooms)
                            {
                                continue;
                            }
                        }
                    }
                }
                //left
                if (tempDoorsUnoccupied[2] == true)
                {
                    int randNum = (int)Random.Range(0, 2);
                    if (randNum == 0)
                    {
                        if (CheckIfRoomExists(i, 2) == false)
                        {
                            GameObject roomToBeAdded = Instantiate(GetRightRoom(), new Vector2(mapManager.roomPosition[i].x - 18, mapManager.roomPosition[i].y), Quaternion.identity);
                            mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                            roomTracker += 1;
                            if (roomTracker == m_numRooms)
                            {
                                continue;
                            }
                        }
                    }
                }
                //right
                if (tempDoorsUnoccupied[3] == true)
                {
                    int randNum = (int)Random.Range(0, 2);
                    if (randNum == 0)
                    {
                        if (CheckIfRoomExists(i, 3) == false)
                        {
                            GameObject roomToBeAdded = Instantiate(GetLeftRoom(), new Vector2(mapManager.roomPosition[i].x + 18, mapManager.roomPosition[i].y), Quaternion.identity);
                            mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                            roomTracker += 1;
                            if (roomTracker == m_numRooms)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

    }


    #region Get Room Functions
    //0 = room has an up door and it is not occupied
    //1 = room has an down door and it is not occupied
    //2 = room has an left door and it is not occupied
    //3 = room has an right door and it is not occupied
    private bool[] DoorsUnoccupied(int roomNumber)
    {
        bool hasUpDoor = false;
        bool hasDownDoor = false;
        bool hasLeftDoor = false;
        bool hasRightDoor = false;

        bool upDoorCanBeUsed = true;
        bool downDoorCanBeUsed = true;
        bool leftDoorCanBeUsed = true;
        bool rightDoorCanBeUsed = true;

        //which doors does this room have?
        if (mapManager.roomName[roomNumber].Contains("UP"))
        {
            hasUpDoor = true;
        }
        if (mapManager.roomName[roomNumber].Contains("DOWN"))
        {
            hasDownDoor = true;
        }
        if (mapManager.roomName[roomNumber].Contains("LEFT"))
        {
            hasLeftDoor = true;
        }
        if (mapManager.roomName[roomNumber].Contains("RIGHT"))
        {
            hasRightDoor = true;
        }

        //which doors already have connecting rooms
        //rooms are 10 apart vertically 
        //rooms are 18 apart horizontally
        if (hasUpDoor)
        {
            Vector2 checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x, mapManager.roomPosition[roomNumber].y + 10);
            for (int i = 0; i < mapManager.roomPosition.Length; i++)
            {
                if (mapManager.roomPosition[i] == checkForThisPos)
                {
                    upDoorCanBeUsed = false;
                }
            }
        }
        else
        {
            upDoorCanBeUsed = false;
        }

        if (hasDownDoor)
        {
            Vector2 checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x, mapManager.roomPosition[roomNumber].y - 10);
            for (int i = 0; i < mapManager.roomPosition.Length; i++)
            {
                if (mapManager.roomPosition[i] == checkForThisPos)
                {
                    downDoorCanBeUsed = false;
                }
            }
        }
        else
        {
            downDoorCanBeUsed = false;
        }

        if (hasLeftDoor)
        {
            Vector2 checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x - 18, mapManager.roomPosition[roomNumber].y);
            for (int i = 0; i < mapManager.roomPosition.Length; i++)
            {
                if (mapManager.roomPosition[i] == checkForThisPos)
                {
                    leftDoorCanBeUsed = false;
                }
            }
        }
        else
        {
            leftDoorCanBeUsed = false;
        }

        if (hasRightDoor)
        {
            Vector2 checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x + 18, mapManager.roomPosition[roomNumber].y);
            for (int i = 0; i < mapManager.roomPosition.Length; i++)
            {
                if (mapManager.roomPosition[i] == checkForThisPos)
                {
                    rightDoorCanBeUsed = false;
                }
            }
        }
        else
        {
            rightDoorCanBeUsed = false;
        }

        bool[] returnThis = new bool[4];
        returnThis[0] = upDoorCanBeUsed;
        returnThis[1] = downDoorCanBeUsed;
        returnThis[2] = leftDoorCanBeUsed;
        returnThis[3] = rightDoorCanBeUsed;

        return returnThis;
    }

    //0 = up
    //1 = down
    //2 = left
    //3 = right
    //rooms are 10 apart vertically 
    //rooms are 18 apart horizontally
    private bool CheckIfRoomExists(int arrayNum, int direction)
    {
        Vector2 currentPos = mapManager.roomPosition[arrayNum];
        switch (direction)
        {
            //up
            case 0:
                Vector2 checkRoomUp = new Vector2(currentPos.x, currentPos.y + 10);
                for (int i = 0; i < mapManager.roomPosition.Length; i++)
                {
                    try
                    {
                        if (mapManager.roomPosition[i] == checkRoomUp)
                        {
                            return true;
                        }
                    }
                    catch { }
                }

                break;

            //down
            case 1:
                Vector2 checkRoomDown = new Vector2(currentPos.x, currentPos.y - 10);
                for (int i = 0; i < mapManager.roomPosition.Length; i++)
                {
                    try
                    {
                        if (mapManager.roomPosition[i] == checkRoomDown)
                        {
                            return true;
                        }
                    }
                    catch { }
                }
                break;

            //left
            case 2:
                Vector2 checkRoomLeft = new Vector2(currentPos.x - 18, currentPos.y);
                for (int i = 0; i < mapManager.roomPosition.Length; i++)
                {
                    try
                    {
                        if (mapManager.roomPosition[i] == checkRoomLeft)
                        {
                            return true;
                        }
                    }
                    catch { }
                }
                break;

            //right
            case 3:
                Vector2 checkRoomRight = new Vector2(currentPos.x + 18, currentPos.y);
                for (int i = 0; i < mapManager.roomPosition.Length; i++)
                {
                    try
                    {
                        if (mapManager.roomPosition[i] == checkRoomRight)
                        {
                            return true;
                        }
                    }
                    catch { }
                }
                break;
        }
        return false;
    }

    //0 = any room
    //1 = up door
    //2 = down door
    //3 = left door
    //4 = right door
    //5 = null
    private GameObject GetRandomRoom(int doorNum)
    {
        switch (doorNum)
        {
            case 0:
                randNum = (int)Random.Range(0, 3);
                switch (randNum)
                {
                    case 0:
                        return GetUpRoom();

                    case 1:
                        return GetDownRoom();

                    case 2:
                        return GetLeftRoom();

                    case 3:
                        return GetRightRoom();
                }
                break;

            case 1:
                return GetUpRoom();

            case 2:
                return GetDownRoom();

            case 3:
                return GetLeftRoom();

            case 4:
                return GetRightRoom();
        }

        return m_upDoors[0];
    }

    private GameObject GetUpRoom()
    {
        randNum2 = (int)Random.Range(0, m_upDoors.Length);
        return m_upDoors[randNum2];
    }

    private GameObject GetDownRoom()
    {
        randNum2 = (int)Random.Range(0, m_downDoors.Length);
        return m_downDoors[randNum2];
    }

    private GameObject GetLeftRoom()
    {
        randNum2 = (int)Random.Range(0, m_leftDoors.Length);
        return m_leftDoors[randNum2];
    }

    private GameObject GetRightRoom()
    {
        randNum2 = (int)Random.Range(0, m_rightDoors.Length);
        return m_rightDoors[randNum2];
    }
    #endregion Get Room Functions
}
