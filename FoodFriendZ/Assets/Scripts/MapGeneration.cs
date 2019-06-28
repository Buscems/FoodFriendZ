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
    string infLoopMonitor = "Start";
    bool shutDoors = false;
    private GameObject[] allRooms;
    private bool checkForOpenDoors = true;
    #endregion Private Variables


    // Start is called before the first frame update
    void Start()
    {
        allRooms = new GameObject[m_upDoors.Length + m_downDoors.Length + m_leftDoors.Length + m_rightDoors.Length];

        int allRoomsCounter = 0;

        for (int i = 0; i < m_upDoors.Length; i++)
        {
            allRooms[allRoomsCounter] = m_upDoors[i];
            allRoomsCounter++;
        }
        for (int i = 0; i < m_downDoors.Length; i++)
        {
            allRooms[allRoomsCounter] = m_downDoors[i];
            allRoomsCounter++;
        }
        for (int i = 0; i < m_leftDoors.Length; i++)
        {
            allRooms[allRoomsCounter] = m_leftDoors[i];
            allRoomsCounter++;
        }
        for (int i = 0; i < m_rightDoors.Length; i++)
        {
            allRooms[allRoomsCounter] = m_rightDoors[i];
            allRoomsCounter++;
        }

        mapManager = GetComponent<MapManager>();
        mapManager.Initialize(m_numRooms);


        GameObject roomToBeAdded = Instantiate(GetRandomRoom(0), new Vector2(0, 0), Quaternion.identity);
        mapManager.UpdateVariables(roomTracker, roomToBeAdded);
        roomTracker += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (roomTracker < m_numRooms)
        {
            if (infLoopMonitor == "Start")
            {
                infLoopMonitor = "Error";
                for (int i = 0; i < roomTracker; i++)
                {
                    bool[] tempDoorsUnoccupied = DoorsUnoccupied(i);
                    //up
                    if (tempDoorsUnoccupied[0] == true)
                    {
                        int randNum = (int)Random.Range(0, 3);
                        if (randNum == 0)
                        {
                            if (CheckIfRoomExists(i, 0) == false && roomTracker != m_numRooms)
                            {
                                GameObject roomToBeAdded = Instantiate(GetDownRoom(), new Vector2(mapManager.roomPosition[i].x, mapManager.roomPosition[i].y + 10), Quaternion.identity);
                                mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                                roomTracker += 1;
                                infLoopMonitor = "Start";
                                if (roomTracker == m_numRooms)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    //down
                    if (tempDoorsUnoccupied[1] == true)
                    {
                        int randNum = (int)Random.Range(0, 3);
                        if (randNum == 0)
                        {
                            if (CheckIfRoomExists(i, 1) == false && roomTracker != m_numRooms)
                            {
                                GameObject roomToBeAdded = Instantiate(GetUpRoom(), new Vector2(mapManager.roomPosition[i].x, mapManager.roomPosition[i].y - 10), Quaternion.identity);
                                mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                                roomTracker += 1;
                                infLoopMonitor = "Start";
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
                        int randNum = (int)Random.Range(0, 3);
                        if (randNum == 0)
                        {
                            if (CheckIfRoomExists(i, 2) == false && roomTracker != m_numRooms)
                            {
                                GameObject roomToBeAdded = Instantiate(GetRightRoom(), new Vector2(mapManager.roomPosition[i].x - 18, mapManager.roomPosition[i].y), Quaternion.identity);
                                mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                                roomTracker += 1;
                                infLoopMonitor = "Start";
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
                        int randNum = (int)Random.Range(0, 3);
                        if (randNum == 0)
                        {
                            if (CheckIfRoomExists(i, 3) == false && roomTracker != m_numRooms)
                            {
                                GameObject roomToBeAdded = Instantiate(GetLeftRoom(), new Vector2(mapManager.roomPosition[i].x + 18, mapManager.roomPosition[i].y), Quaternion.identity);
                                mapManager.UpdateVariables(roomTracker, roomToBeAdded);
                                roomTracker += 1;
                                infLoopMonitor = "Start";
                                if (roomTracker == m_numRooms)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            else if (infLoopMonitor != "Start")
            {
                ChangeExistingDoor();
                infLoopMonitor = "Start";
            }
        }

        if (roomTracker == m_numRooms)
        {
            for (int i = 0; i < m_numRooms; i++)
            {
                CloseDoors(i);
                FixOneWayDoors(i);
            }
        }



    }


    #region Get Room Functions

    private void FixOneWayDoors(int arrayNum)
    {
        if (mapManager.roomName[arrayNum].Contains("UP"))
        {
            FindIfWall(arrayNum, 0);
        }

        if (mapManager.roomName[arrayNum].Contains("DOWN"))
        {
            FindIfWall(arrayNum, 1);
        }

        if (mapManager.roomName[arrayNum].Contains("LEFT"))
        {
            FindIfWall(arrayNum, 2);
        }

        if (mapManager.roomName[arrayNum].Contains("RIGHT"))
        {
            FindIfWall(arrayNum, 3);
        }
    }

    private int FindIfWall(int arrayNum, int direction)
    {
        Vector2 findRoomHere;
        int randNum;
        string roomName;

        switch (direction)
        {
            case 0:
                findRoomHere = new Vector2(mapManager.roomPosition[arrayNum].x, mapManager.roomPosition[arrayNum].y + 10);
                for (int i = 0; i < m_numRooms; i++)
                {
                    if(mapManager.roomPosition[i] == findRoomHere)
                    {
                        roomName = mapManager.roomName[i];
                        if (roomName.Contains("DOWN") == false)
                        {
                            randNum = Random.Range(0, 2);
                            if(randNum == 0)
                            {
                                roomName = InsertWord("DOWN", i);
                                for(int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[i]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[i], Quaternion.identity);
                                        mapManager.UpdateVariables(i, newRoom);
                                    }
                                }
                            }
                            else
                            {
                                roomName = RemoveWord("UP", arrayNum);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[arrayNum]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[arrayNum], Quaternion.identity);
                                        mapManager.UpdateVariables(arrayNum, newRoom);
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case 1:
                findRoomHere = new Vector2(mapManager.roomPosition[arrayNum].x, mapManager.roomPosition[arrayNum].y - 10);
                for (int i = 0; i < m_numRooms; i++)
                {
                    if (mapManager.roomPosition[i] == findRoomHere)
                    {
                        roomName = mapManager.roomName[i];
                        if (roomName.Contains("UP") == false)
                        {
                            randNum = Random.Range(0, 2);
                            if (randNum == 0)
                            {
                                roomName = InsertWord("UP", i);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[i]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[i], Quaternion.identity);
                                        mapManager.UpdateVariables(i, newRoom);
                                    }
                                }
                            }
                            else
                            {
                                roomName = RemoveWord("DOWN", arrayNum);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[arrayNum]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[arrayNum], Quaternion.identity);
                                        mapManager.UpdateVariables(arrayNum, newRoom);
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case 2:
                findRoomHere = new Vector2(mapManager.roomPosition[arrayNum].x - 18, mapManager.roomPosition[arrayNum].y);
                for (int i = 0; i < m_numRooms; i++)
                {
                    if (mapManager.roomPosition[i] == findRoomHere)
                    {
                        roomName = mapManager.roomName[i];
                        if (roomName.Contains("RIGHT") == false)
                        {
                            randNum = Random.Range(0, 2);
                            if (randNum == 0)
                            {
                                roomName = InsertWord("RIGHT", i);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[i]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[i], Quaternion.identity);
                                        mapManager.UpdateVariables(i, newRoom);
                                    }
                                }
                            }
                            else
                            {
                                roomName = RemoveWord("LEFT", arrayNum);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[arrayNum]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[arrayNum], Quaternion.identity);
                                        mapManager.UpdateVariables(arrayNum, newRoom);
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case 3:
                findRoomHere = new Vector2(mapManager.roomPosition[arrayNum].x + 18, mapManager.roomPosition[arrayNum].y);
                for (int i = 0; i < m_numRooms; i++)
                {
                    if (mapManager.roomPosition[i] == findRoomHere)
                    {
                        roomName = mapManager.roomName[i];
                        if (roomName.Contains("LEFT") == false)
                        {
                            randNum = Random.Range(0, 2);
                            if (randNum == 0)
                            {
                                roomName = InsertWord("LEFT", i);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print("rn"+roomName);
                                        print("al"+allRooms[k].name);
                                        Destroy(mapManager.roomManager[i]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[i], Quaternion.identity);
                                        mapManager.UpdateVariables(i, newRoom);
                                    }
                                }
                            }
                            else
                            {
                                roomName = RemoveWord("RIGHT", arrayNum);
                                for (int k = 0; k < allRooms.Length; k++)
                                {
                                    if (allRooms[k].name == roomName)
                                    {
                                        print(allRooms[k].name);
                                        Destroy(mapManager.roomManager[arrayNum]);
                                        GameObject newRoom = Instantiate(allRooms[k], mapManager.roomPosition[arrayNum], Quaternion.identity);
                                        mapManager.UpdateVariables(arrayNum, newRoom);
                                    }
                                }
                            }
                        }
                    }
                }
                break;
        }


        return 0;
    }

    private string InsertWord(string word, int arrayPos)
    {
        bool hasUp = false;
        bool hasDown = false;
        bool hasLeft = false;
        bool hasRight = false;

        string roomName = mapManager.roomName[arrayPos];

        if (roomName.Contains("UP"))
        {
            hasUp = true;
        }
        if (roomName.Contains("DOWN"))
        {
            hasDown = true;
        }
        if (roomName.Contains("LEFT"))
        {
            hasLeft = true;
        }
        if (roomName.Contains("RIGHT"))
        {
            hasRight = true;
        }


        if (word == "UP") {
            if (!hasUp)
            {
                roomName = "_UP" + roomName;
            }
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "DOWN") {
            int lettersIn1 = 0;
            if (hasUp)
            {
                lettersIn1 += 3;
            }
            if (hasDown)
            {
                roomName = roomName.Insert(lettersIn1, "_DOWN");
            }
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "LEFT") {
            int lettersIn2 = 0;
            if (hasUp)
            {
                lettersIn2 += 3;
            }
            if (hasDown)
            {
                lettersIn2 += 5;
            }
            if (hasLeft)
            {
                roomName = roomName.Insert(lettersIn2, "_LEFT");
            }
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "RIGHT") {
            int lettersIn3 = 0;
            if (hasUp)
            {
                lettersIn3 += 3;
            }
            if (hasDown)
            {
                lettersIn3 += 5;
            }
            if (hasLeft)
            {
                lettersIn3 += 5;
            }
            if (hasLeft)
            {
                roomName = roomName.Insert(lettersIn3, "_RIGHT");
            }
            roomName = roomName.Replace("(Clone)", "");
            return roomName;

        }
        
        return "";
    }

    private string RemoveWord(string word, int arrayPos)
    {

        string roomName = mapManager.roomName[arrayPos];


        if (word == "UP")
        {
            roomName = roomName.Replace("_UP", "");
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "DOWN")
        {
            roomName = roomName.Replace("_DOWN", "");
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "LEFT")
        {
            roomName = roomName.Replace("_LEFT", "");
            roomName = roomName.Replace("(Clone)", "");
            return roomName;
        }

        if (word == "RIGHT")
        {
            roomName = roomName.Replace("_RIGHT", "");
            roomName = roomName.Replace("(Clone)", "");
            return roomName;

        }
        return "";
    }

    private void CloseDoors(int arrayNum)
    {
        bool[] shutDoorsHere = DoorsUnoccupied(arrayNum);
        string roomName = "";

        if (shutDoorsHere[0] != false && shutDoorsHere[1] != false && shutDoorsHere[2] != false && shutDoorsHere[3] != false)
        {

        }
        else
        {
            if (shutDoorsHere[0] == true)
            {
                roomName = mapManager.roomName[arrayNum];
                roomName = roomName.Replace("_UP", "");
            }
            if (shutDoorsHere[1] == true)
            {
                roomName = mapManager.roomName[arrayNum];
                roomName = roomName.Replace("_DOWN", "");
            }
            if (shutDoorsHere[2] == true)
            {
                roomName = mapManager.roomName[arrayNum];
                roomName = roomName.Replace("_LEFT", "");
            }
            if (shutDoorsHere[3] == true)
            {
                roomName = mapManager.roomName[arrayNum];
                roomName = roomName.Replace("_RIGHT", "");
            }

            roomName = roomName.Replace("(Clone)", "");

            for (int i = 0; i < allRooms.Length; i++)
            {
                if (allRooms[i].name == roomName)
                {
                    Destroy(mapManager.roomManager[arrayNum]);
                    GameObject newRoom = Instantiate(allRooms[i], mapManager.roomPosition[arrayNum], Quaternion.identity);
                    mapManager.UpdateVariables(arrayNum, newRoom);
                }
            }
        }
    }


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

    private bool[] RoomSpaceAvailable(int roomNumber)
    {
        bool upDoorCanBeUsed = true;
        bool downDoorCanBeUsed = true;
        bool leftDoorCanBeUsed = true;
        bool rightDoorCanBeUsed = true;

        //which doors already have connecting rooms
        //rooms are 10 apart vertically 
        //rooms are 18 apart horizontally

        Vector2 checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x, mapManager.roomPosition[roomNumber].y + 10);
        for (int i = 0; i < mapManager.roomPosition.Length; i++)
        {
            if (mapManager.roomPosition[i] == checkForThisPos)
            {
                upDoorCanBeUsed = false;
            }
        }

        checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x, mapManager.roomPosition[roomNumber].y - 10);
        for (int i = 0; i < mapManager.roomPosition.Length; i++)
        {
            if (mapManager.roomPosition[i] == checkForThisPos)
            {
                downDoorCanBeUsed = false;
            }
        }

        checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x - 18, mapManager.roomPosition[roomNumber].y);
        for (int i = 0; i < mapManager.roomPosition.Length; i++)
        {
            if (mapManager.roomPosition[i] == checkForThisPos)
            {
                leftDoorCanBeUsed = false;
            }
        }

        checkForThisPos = new Vector2(mapManager.roomPosition[roomNumber].x + 18, mapManager.roomPosition[roomNumber].y);
        for (int i = 0; i < mapManager.roomPosition.Length; i++)
        {
            if (mapManager.roomPosition[i] == checkForThisPos)
            {
                rightDoorCanBeUsed = false;
            }
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
        if (roomTracker < m_numRooms)
        {
            randNum2 = (int)Random.Range(0, m_upDoors.Length);
            return m_upDoors[randNum2];
        }
        else
        {
            return null;
        }
    }

    private GameObject GetDownRoom()
    {
        if (roomTracker < m_numRooms)
        {
            randNum2 = (int)Random.Range(0, m_downDoors.Length);
            return m_downDoors[randNum2];
        }
        else
        {
            return null;
        }
    }

    private GameObject GetLeftRoom()
    {
        if (roomTracker < m_numRooms)
        {
            randNum2 = (int)Random.Range(0, m_leftDoors.Length);
            return m_leftDoors[randNum2];
        }
        else
        {
            return null;
        }
    }

    private GameObject GetRightRoom()
    {
        if (roomTracker < m_numRooms)
        {
            randNum2 = (int)Random.Range(0, m_rightDoors.Length);
            return m_rightDoors[randNum2];
        }
        else
        {
            return null;
        }
    }

    private void ChangeExistingDoor()
    {

        bool continueWhile = true;

        if (roomTracker < m_numRooms)
        {
            while (continueWhile)
            {
                int randRoom = Random.Range(0, roomTracker);

                bool[] roomsAvailable = RoomSpaceAvailable(randRoom);

                int randomNum = Random.Range(0, 1);
                if (randomNum == 0)
                {
                    if (roomsAvailable[0] == true)
                    {
                        InsertNewRoom(randRoom, 0);
                        continueWhile = false;
                        continue;
                    }
                }
                randomNum = Random.Range(0, 1);
                if (randomNum == 0)
                {
                    if (roomsAvailable[1] == true)
                    {
                        InsertNewRoom(randRoom, 1);
                        continueWhile = false;
                        continue;
                    }
                }
                randomNum = Random.Range(0, 1);
                if (randomNum == 0)
                {
                    if (roomsAvailable[2] == true)
                    {
                        InsertNewRoom(randRoom, 2);
                        continueWhile = false;
                        continue;
                    }
                }
                randomNum = Random.Range(0, 1);
                if (randomNum == 0)
                {
                    if (roomsAvailable[3] == true)
                    {
                        InsertNewRoom(randRoom, 3);
                        continueWhile = false;
                        continue;
                    }
                }
            }
        }
    }

    private void InsertNewRoom(int arrayPos, int newDirection)
    {
        bool hasUp = false;
        bool hasDown = false;
        bool hasLeft = false;
        bool hasRight = false;

        string roomName = mapManager.roomName[arrayPos];

        if (roomName.Contains("UP"))
        {
            hasUp = true;
        }
        if (roomName.Contains("DOWN"))
        {
            hasDown = true;
        }
        if (roomName.Contains("LEFT"))
        {
            hasLeft = true;
        }
        if (roomName.Contains("RIGHT"))
        {
            hasRight = true;
        }



        switch (newDirection)
        {
            case 0:
                if (!hasUp)
                {
                    roomName = "_UP" + roomName;
                }
                roomName = roomName.Replace("(Clone)", "");
                for (int i = 0; i < m_downDoors.Length; i++)
                {
                    if (m_downDoors[i].name == roomName)
                    {
                        Destroy(mapManager.roomManager[arrayPos]);
                        GameObject newRoom = Instantiate(m_downDoors[i], mapManager.roomPosition[arrayPos], Quaternion.identity);
                        mapManager.UpdateVariables(arrayPos, newRoom);
                    }
                }
                break;

            case 1:
                int lettersIn1 = 0;
                if (hasUp)
                {
                    lettersIn1 += 3;
                }
                if (hasDown)
                {
                    roomName = roomName.Insert(lettersIn1, "_DOWN");
                }
                roomName = roomName.Replace("(Clone)", "");
                for (int i = 0; i < m_upDoors.Length; i++)
                {
                    if (m_upDoors[i].name == roomName)
                    {
                        Destroy(mapManager.roomManager[arrayPos]);
                        GameObject newRoom = Instantiate(m_upDoors[i], mapManager.roomPosition[arrayPos], Quaternion.identity);
                        mapManager.UpdateVariables(arrayPos, newRoom);
                    }
                }
                break;

            case 2:
                int lettersIn2 = 0;
                if (hasUp)
                {
                    lettersIn2 += 3;
                }
                if (hasDown)
                {
                    lettersIn2 += 5;
                }
                if (hasLeft)
                {
                    roomName = roomName.Insert(lettersIn2, "_LEFT");
                }
                roomName = roomName.Replace("(Clone)", "");
                for (int i = 0; i < m_rightDoors.Length; i++)
                {
                    if (m_rightDoors[i].name == roomName)
                    {
                        Destroy(mapManager.roomManager[arrayPos]);
                        GameObject newRoom = Instantiate(m_rightDoors[i], mapManager.roomPosition[arrayPos], Quaternion.identity);
                        mapManager.UpdateVariables(arrayPos, newRoom);
                    }
                }

                break;

            case 3:
                int lettersIn3 = 0;
                if (hasUp)
                {
                    lettersIn3 += 3;
                }
                if (hasDown)
                {
                    lettersIn3 += 5;
                }
                if (hasLeft)
                {
                    lettersIn3 += 5;
                }
                if (hasLeft)
                {
                    roomName = roomName.Insert(lettersIn3, "_RIGHT");
                }
                roomName = roomName.Replace("(Clone)", "");
                for (int i = 0; i < m_leftDoors.Length; i++)
                {
                    if (m_leftDoors[i].name == roomName)
                    {
                        Destroy(mapManager.roomManager[arrayPos]);
                        GameObject newRoom = Instantiate(m_leftDoors[i], mapManager.roomPosition[arrayPos], Quaternion.identity);
                        mapManager.UpdateVariables(arrayPos, newRoom);
                    }
                }
                break;
        }



    }
    #endregion Get Room Functions
}
