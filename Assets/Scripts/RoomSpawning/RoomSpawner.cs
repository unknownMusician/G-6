using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    #region Properties

    public List<GameObject> mapElementsPrefabs;

    public List<GameObject> mapIconsPrefabs;

    public int amountOfRooms;

    private (int, int) finishRoomCoord;

    #region Matrixes

    public PlaceForRoom[,] roomDirectionsDataMatrix;
    public GameObject[,] roomsGameObjectMatrix;
    public GameObject[,] miniMapMatrix;

    #endregion

    #region Amount of rows and columns 

    public int rows;
    public int columns;

    #endregion

    #region Coordinates of current room

    public int CurrentColumn { get; set; }
    public int CurrentRow { get; set; }

    #endregion

    #region Prefabs dictionaries

    private Dictionary<string, GameObject> mapElementsPrefabsDictionary;
    private Dictionary<string, GameObject[]> roomsPrefabsDictionary;
    private Dictionary<string, GameObject> mapIconsPrefabsDictionary;

    #endregion

    #region Rooms prefabs arrays

    public GameObject[] T;
    public GameObject[] R;
    public GameObject[] B;
    public GameObject[] L;
    public GameObject[] TR;
    public GameObject[] TB;
    public GameObject[] TL;
    public GameObject[] RB;
    public GameObject[] RL;
    public GameObject[] BL;
    public GameObject[] notT;
    public GameObject[] notR;
    public GameObject[] notB;
    public GameObject[] notL;
    public GameObject[] Base;
    public GameObject[] Block;

    #endregion

    #endregion

    #region Methods

    #region Awake() and Start() methods

    private void Awake() {
        MainData.RoomSpawnerObject = this.gameObject;
    }

    private void Start() {

        #region Matrixes initialization

        roomDirectionsDataMatrix = new PlaceForRoom[rows, columns];
        roomsGameObjectMatrix = new GameObject[rows, columns];
        miniMapMatrix = new GameObject[rows, columns];
        
        #endregion

        #region Current row and column initialization

        CurrentColumn = columns / 2;
        CurrentRow = rows / 2;
        
        #endregion

        #region Creating base room

        PlaceForRoom baseRoom = new PlaceForRoom(1, 1, 1, 1);
        roomDirectionsDataMatrix[CurrentRow, CurrentColumn] = baseRoom;
        amountOfRooms -= 1;
        
        #endregion

        #region mapElementsPrefabsDictionary initialization and filling

        mapElementsPrefabsDictionary = new Dictionary<string, GameObject>();

        mapElementsPrefabsDictionary.Add("2000", mapElementsPrefabs[0]);     // T
        mapElementsPrefabsDictionary.Add("0200", mapElementsPrefabs[1]);     // R
        mapElementsPrefabsDictionary.Add("0020", mapElementsPrefabs[2]);     // B
        mapElementsPrefabsDictionary.Add("0002", mapElementsPrefabs[3]);     // L
        mapElementsPrefabsDictionary.Add("2200", mapElementsPrefabs[4]);    // TR
        mapElementsPrefabsDictionary.Add("2020", mapElementsPrefabs[5]);    // TB
        mapElementsPrefabsDictionary.Add("2002", mapElementsPrefabs[6]);    // TL
        mapElementsPrefabsDictionary.Add("0220", mapElementsPrefabs[7]);    // RB
        mapElementsPrefabsDictionary.Add("0202", mapElementsPrefabs[8]);    // RL
        mapElementsPrefabsDictionary.Add("0022", mapElementsPrefabs[9]);    // BL
        mapElementsPrefabsDictionary.Add("0222", mapElementsPrefabs[10]); // notT
        mapElementsPrefabsDictionary.Add("2022", mapElementsPrefabs[11]); // notR
        mapElementsPrefabsDictionary.Add("2202", mapElementsPrefabs[12]); // notB
        mapElementsPrefabsDictionary.Add("2220", mapElementsPrefabs[13]); // notL
        mapElementsPrefabsDictionary.Add("2222", mapElementsPrefabs[14]); // Base
        mapElementsPrefabsDictionary.Add("0000", mapElementsPrefabs[15]); // Block
        
        #endregion

        #region roomsPrefabsDictionary initialization and filling

        roomsPrefabsDictionary = new Dictionary<string, GameObject[]>();

        roomsPrefabsDictionary.Add("2000", T);     // T
        roomsPrefabsDictionary.Add("0200", R);     // R
        roomsPrefabsDictionary.Add("0020", B);     // B
        roomsPrefabsDictionary.Add("0002", L);     // L
        roomsPrefabsDictionary.Add("2200", TR);    // TR
        roomsPrefabsDictionary.Add("2020", TB);    // TB
        roomsPrefabsDictionary.Add("2002", TL);    // TL
        roomsPrefabsDictionary.Add("0220", RB);    // RB
        roomsPrefabsDictionary.Add("0202", RL);    // RL
        roomsPrefabsDictionary.Add("0022", BL);    // BL
        roomsPrefabsDictionary.Add("0222", notT); // notT
        roomsPrefabsDictionary.Add("2022", notR); // notR
        roomsPrefabsDictionary.Add("2202", notB); // notB
        roomsPrefabsDictionary.Add("2220", notL); // notL
        roomsPrefabsDictionary.Add("2222", Base); // Base
        roomsPrefabsDictionary.Add("0000", Block); // Block

        #endregion

        #region mapIconsPrefabsDictionary initialization and filling

        mapIconsPrefabsDictionary = new Dictionary<string, GameObject>();

        mapIconsPrefabsDictionary.Add("start", mapIconsPrefabs[0]);
        mapIconsPrefabsDictionary.Add("finish", mapIconsPrefabs[1]);
        mapIconsPrefabsDictionary.Add("boss", mapIconsPrefabs[2]);
        mapIconsPrefabsDictionary.Add("chest", mapIconsPrefabs[3]);
        mapIconsPrefabsDictionary.Add("market", mapIconsPrefabs[4]);
        mapIconsPrefabsDictionary.Add("no enemy", mapIconsPrefabs[5]);

        #endregion

        generateDungeon();

        spawnDungeon();

        spawnDungeonMap();

        renderIconsOnMinimap();

        #region Teleporting player to base room

        MainData.PlayerObject.transform.position = roomsGameObjectMatrix[CurrentRow, CurrentColumn].transform.position;

        #endregion

    }

    #endregion

    #region Generate and spawn methods

    private void generateDungeon() {

        #region Generating dungeon

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                PlaceForRoom point = roomDirectionsDataMatrix[i, j];

                if (amountOfRooms > 0) {
                    
                    #region Creating PlaceForRoom objects for every place for room

                    if ((i - 1 >= 0) && (roomDirectionsDataMatrix[i - 1, j] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setTop(roomDirectionsDataMatrix[i - 1, j].getDoorParams()[2]);
                    } else if (i - 1 < 0) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setTop(-1);
                        }
                    if ((j + 1 < columns) && (roomDirectionsDataMatrix[i, j + 1] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setRight(roomDirectionsDataMatrix[i, j + 1].getDoorParams()[3]);
                    } else if (j + 1 >= columns) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setRight(-1);
                    }
                    if ((i + 1 < rows) && (roomDirectionsDataMatrix[i + 1, j] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setBottom(roomDirectionsDataMatrix[i + 1, j].getDoorParams()[0]);
                    } else if (i + 1 >= rows) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setBottom(-1);
                    }
                    if ((j - 1 >= 0) && (roomDirectionsDataMatrix[i, j - 1] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setLeft(roomDirectionsDataMatrix[i, j - 1].getDoorParams()[1]);
                    } else if (j - 1 < 0) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setLeft(-1);
                    }

                    #endregion

                    #region Random door filling

                    if ((point != null) && (point.anyEqualToOne() == true)) {
                        short[] doors = point.getDoorParams();
                        if (doors[0] == 0) {
                            float rd = Random.value;
                            if (!doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setTop(-1);
                            } else if (doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setTop(1);
                                point.AmountOfDoors += 1;
                            }
                        }
                        if (doors[1] == 0) {
                            float rd = Random.value;
                            if (!doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setRight(-1);
                            } else if (doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setRight(1);
                                point.AmountOfDoors += 1;
                            }
                        }
                        if (doors[2] == 0) {
                            float rd = Random.value;
                            if (!doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setBottom(-1);
                            } else if (doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setBottom(1);
                                point.AmountOfDoors += 1;
                            }
                        }
                        if (doors[3] == 0) {
                            float rd = Random.value;
                            if (!doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setLeft(-1);
                            } else if (doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {
                                point.setLeft(1);
                                point.AmountOfDoors += 1;
                            }
                        }
                        amountOfRooms -= 1;
                    }

                    #endregion

                } else {
                    break;
                }
            }
        }

        #endregion

        #region Adding rooms to fit amount of rooms

        while (amountOfRooms >= 0) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {

                    float rd = Random.value;
                    PlaceForRoom point = roomDirectionsDataMatrix[i, j];
                    
                    if (point != null) {
                        
                        if (doWeNeedAnotherDoor(point.AmountOfDoors, rd)) {

                            if ((point.anyEqualToOne() == true)) {

                                if ((i - 1 >= 0) && (amountOfRooms >= 0)) {
                                    if (roomDirectionsDataMatrix[i - 1, j] != null) {
                                        if (roomDirectionsDataMatrix[i - 1, j].anyEqualToOne() == false) {
                                            point.setTop(1);
                                            roomDirectionsDataMatrix[i - 1, j].setTop(-1);
                                            roomDirectionsDataMatrix[i - 1, j].setRight(-1);
                                            roomDirectionsDataMatrix[i - 1, j].setBottom(1);
                                            roomDirectionsDataMatrix[i - 1, j].setLeft(-1);
                                            if (amountOfRooms == 0) {
                                                finishRoomCoord = (i - 1, j);
                                            }
                                            amountOfRooms -= 1;
                                        }
                                    }
                                }

                                if ((j + 1 < columns) && (amountOfRooms >= 0)) {
                                    if (roomDirectionsDataMatrix[i, j + 1] != null) {
                                        if (roomDirectionsDataMatrix[i, j + 1].anyEqualToOne() == false) {
                                            point.setRight(1);
                                            roomDirectionsDataMatrix[i, j + 1].setTop(-1);
                                            roomDirectionsDataMatrix[i, j + 1].setRight(-1);
                                            roomDirectionsDataMatrix[i, j + 1].setBottom(-1);
                                            roomDirectionsDataMatrix[i, j + 1].setLeft(1);
                                            if (amountOfRooms == 0) {
                                                finishRoomCoord = (i, j + 1);
                                            }
                                            amountOfRooms -= 1;
                                        }
                                    }
                                }

                                if ((i + 1 < rows) && (amountOfRooms >= 0)) {
                                    if (roomDirectionsDataMatrix[i + 1, j] != null) {
                                        if (roomDirectionsDataMatrix[i + 1, j].anyEqualToOne() == false) {
                                            point.setBottom(1);
                                            roomDirectionsDataMatrix[i + 1, j].setTop(1);
                                            roomDirectionsDataMatrix[i + 1, j].setRight(-1);
                                            roomDirectionsDataMatrix[i + 1, j].setBottom(-1);
                                            roomDirectionsDataMatrix[i + 1, j].setLeft(-1);
                                            if (amountOfRooms == 0) {
                                                finishRoomCoord = (i + 1, j);
                                            }
                                            amountOfRooms -= 1;
                                        }
                                    }
                                }

                                if ((j - 1 >= 0) && (amountOfRooms >= 0)) {
                                    if (roomDirectionsDataMatrix[i, j - 1] != null) {
                                        if (roomDirectionsDataMatrix[i, j - 1].anyEqualToOne() == false) {
                                            point.setLeft(1);
                                            roomDirectionsDataMatrix[i, j - 1].setTop(-1);
                                            roomDirectionsDataMatrix[i, j - 1].setRight(1);
                                            roomDirectionsDataMatrix[i, j - 1].setBottom(-1);
                                            roomDirectionsDataMatrix[i, j - 1].setLeft(-1);
                                            if (amountOfRooms == 0) {
                                                finishRoomCoord = (i, j - 1);
                                            }
                                            amountOfRooms -= 1;
                                        }
                                    }
                                } 
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Closing doors to nowhere, etc.

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                if (amountOfRooms < 0) {
                    PlaceForRoom point = roomDirectionsDataMatrix[i, j];

                    if (point != null) {

                        if ((point.anyEqualToOne() == true)) {

                            if (i - 1 >= 0) {
                                if (roomDirectionsDataMatrix[i - 1, j] != null) {
                                    if (roomDirectionsDataMatrix[i - 1, j].anyEqualToOne() == false) {
                                        point.setTop(-1);
                                    } else if (roomDirectionsDataMatrix[i - 1, j].getDoorParams()[2] == -1) {
                                        point.setTop(-1);
                                    }
                                } else {
                                    point.setTop(-1);
                                }
                            }

                            if (j + 1 < columns) {
                                if (roomDirectionsDataMatrix[i, j + 1] != null) {
                                    if (roomDirectionsDataMatrix[i, j + 1].anyEqualToOne() == false) {
                                        point.setRight(-1);
                                    } else if (roomDirectionsDataMatrix[i, j + 1].getDoorParams()[3] == -1) {
                                        point.setRight(-1);
                                    }
                                } else {
                                    point.setRight(-1);
                                }
                            }

                            if (i + 1 < rows) {
                                if (roomDirectionsDataMatrix[i + 1, j] != null) {
                                    if (roomDirectionsDataMatrix[i + 1, j].anyEqualToOne() == false) {
                                        point.setBottom(-1);
                                    } else if (roomDirectionsDataMatrix[i + 1, j].getDoorParams()[0] == -1) {
                                        point.setBottom(-1);
                                    }
                                } else {
                                    point.setBottom(-1);
                                }
                            }

                            if (j - 1 >= 0) {
                                if (roomDirectionsDataMatrix[i, j - 1] != null) {
                                    if (roomDirectionsDataMatrix[i, j - 1].anyEqualToOne() == false) {
                                        point.setLeft(-1);
                                    } else if (roomDirectionsDataMatrix[i, j - 1].getDoorParams()[1] == -1) {
                                        point.setLeft(-1);
                                    }
                                } else {
                                    point.setLeft(-1);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

    }

    private void spawnDungeonMap() {

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                if (roomDirectionsDataMatrix[i, j] != null) {

                    short[] array = roomDirectionsDataMatrix[i, j].getDoorParams();
                    for (int k = 0; k < array.Length; k++) {
                        array[k] += 1;
                    }

                    string key = string.Concat<short>(array);

                    if (mapElementsPrefabsDictionary.ContainsKey(key) == false) {
                        continue;
                    } else {
                        miniMapMatrix[i, j] = Instantiate(mapElementsPrefabsDictionary[key],
                            new Vector2(j * 10 - 300, -i * 10 + 300),
                            Quaternion.identity,
                            this.gameObject.transform.GetChild(1).GetChild(1)
                            );
                    }
                }
            }
        }
        // Coloring base room mini-map element to red, because it is the first active room
        miniMapMatrix[CurrentRow, CurrentColumn].GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void spawnDungeon() {

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                if (roomDirectionsDataMatrix[i, j] != null) {

                    short[] array = roomDirectionsDataMatrix[i, j].getDoorParams();
                    for (int k = 0; k < array.Length; k++) {
                        array[k] += 1;
                    }

                    string key = string.Concat<short>(array);

                    if (mapElementsPrefabsDictionary.ContainsKey(key) == false) {
                        continue;
                    } else {

                        roomsGameObjectMatrix[i, j] = Instantiate(
                            roomsPrefabsDictionary[key][Random.Range(0, roomsPrefabsDictionary[key].Length)],
                            new Vector2(j * 250, -i * 200),
                            Quaternion.identity,
                            this.gameObject.transform.GetChild(0)
                            );

                        roomsGameObjectMatrix[i, j].SetActive(false);

                        if ((i == rows / 2) && (j == columns / 2)) {
                            roomsGameObjectMatrix[i, j].SetActive(true);
                        }
                    }
                }
            }
        }
        // Last added room = finish room, base room = start room
        roomsGameObjectMatrix[finishRoomCoord.Item1, finishRoomCoord.Item2].GetComponent<Room>().RoomType = "finish";
        roomsGameObjectMatrix[finishRoomCoord.Item1, finishRoomCoord.Item2].GetComponent<Room>().RoomStuff = "boss";

        roomsGameObjectMatrix[CurrentRow, CurrentColumn].GetComponent<Room>().RoomType = "start";
        roomsGameObjectMatrix[CurrentRow, CurrentColumn].GetComponent<Room>().RoomStuff = "market";

    }

    #endregion

    #region Get() methods of matrixes

    public GameObject[,] getRoomsMatrix() {
        return roomsGameObjectMatrix;
    }

    public GameObject[,] getMiniMapMatrix() {
        return miniMapMatrix;
    }

    #endregion

    #region Get() methods of active room components

    public Vector2 getCoordinatesOfTheActiveRoom() {
        Vector2 result = new Vector2(CurrentColumn * 250, -CurrentRow * 200);
        return result;
    }

    public GameObject getGameObjectOfTheActiveRoom() {
        return roomsGameObjectMatrix[CurrentRow, CurrentColumn];
    }

    public Room getRoomComponentOfTheActiveRoom() {
        return roomsGameObjectMatrix[CurrentRow, CurrentColumn].GetComponent<Room>();
    }

    public Transform getTransformComponentOfTheActiveRoom() {
        return roomsGameObjectMatrix[CurrentRow, CurrentColumn].GetComponent<Transform>();
    }

    #endregion

    #region Overloads of isThereAnyEnemy()

    public bool isThereAnyEnemy(int row, int column) {
        return roomsGameObjectMatrix[row, column].transform.GetChild(2).transform.childCount != 0;
    }

    public bool isThereAnyEnemy(GameObject room) {
        return room.transform.GetChild(2).transform.childCount != 0;
    }

    #endregion

    #region Methods of going to next room operations

    public void goToNextRoom(Transform playerTransform, int direction) {

        GameObject currentRoomGameObject = roomsGameObjectMatrix[CurrentRow, CurrentColumn];
        GameObject currentRoomMiniMapElement = miniMapMatrix[CurrentRow, CurrentColumn];

        GameObject nextRoomGameObject = getNextRoomGameObject(direction);
        GameObject nextRoomMiniMapElement = getNextRoomMiniMapElement(direction);

        nextRoomGameObject.SetActive(true);
        nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
        nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;

        GameObject spawnpoint = getNextRoomSpawnpoint(direction, nextRoomGameObject);
        playerTransform.position = spawnpoint.transform.position;

        focusCameraOnANewRoom(nextRoomMiniMapElement);

        currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
        currentRoomGameObject.SetActive(false);

        changeCurrentCoordAfterGoingToNextRoom(direction);

    }

    public GameObject getNextRoomGameObject(int direction) {
        if (direction == 0) {
            return roomsGameObjectMatrix[CurrentRow - 1, CurrentColumn];
        } else if (direction == 1) {
            return roomsGameObjectMatrix[CurrentRow, CurrentColumn + 1];
        } else if (direction == 2) {
            return roomsGameObjectMatrix[CurrentRow + 1, CurrentColumn];
        } else if (direction == 3) {
            return roomsGameObjectMatrix[CurrentRow, CurrentColumn - 1];
        }
        return null;
    }

    public GameObject getNextRoomMiniMapElement(int direction) {
        if (direction == 0) {
            return miniMapMatrix[CurrentRow - 1, CurrentColumn];
        } else if (direction == 1) {
            return miniMapMatrix[CurrentRow, CurrentColumn + 1];
        } else if (direction == 2) {
            return miniMapMatrix[CurrentRow + 1, CurrentColumn];
        } else if (direction == 3) {
            return miniMapMatrix[CurrentRow, CurrentColumn - 1];
        }
        return null;
    }

    public GameObject getNextRoomSpawnpoint(int direction, GameObject nextRoom) {
        if (direction == 0) {
            return nextRoom.GetComponent<Room>().BottomSpawnpoint;
        } else if (direction == 1) {
            return nextRoom.GetComponent<Room>().LeftSpawnpoint;
        } else if (direction == 2) {
            return nextRoom.GetComponent<Room>().TopSpawnpoint;
        } else if (direction == 3) {
            return nextRoom.GetComponent<Room>().RightSpawnpoint;
        }
        return null;
    }

    public void focusCameraOnANewRoom(GameObject nextRoomMiniMapElement) {
        float coordX = nextRoomMiniMapElement.transform.position.x;
        float coordY = nextRoomMiniMapElement.transform.position.y;
        float coordZ = this.transform.GetChild(2).position.z;
        this.transform.GetChild(2).position = new Vector3(coordX, coordY, coordZ);
    }

    public void changeCurrentCoordAfterGoingToNextRoom(int direction) {
        if (direction == 0) {
            CurrentRow -= 1;
        } else if (direction == 1) {
            CurrentColumn += 1;
        } else if (direction == 2) {
            CurrentRow += 1;
        } else if (direction == 3) {
            CurrentColumn -= 1;
        }
    }

    #endregion

    public bool doWeNeedAnotherDoor(byte amountOfDoors, float randomNumber) {
        if ((amountOfDoors == 0) & (randomNumber > 0.5f)) {
            return true;
        } else if ((amountOfDoors == 1) & (randomNumber > 0.85f)) {
            return true;
        } else if ((amountOfDoors == 2) & (randomNumber > 0.95f)) {
            return true;
        } else if ((amountOfDoors == 3) & (randomNumber > 0.99f)) {
            return true;
        }
        return false;
    }

    public void renderIconsOnMinimap()  {
        
        for (int i = 0; i < rows; i++) {
            
            for (int j = 0; j < columns; j++) {

                GameObject gameobjectOfTheRoom = roomsGameObjectMatrix[i, j];

                if (gameobjectOfTheRoom != null) {

                    #region Properties initialization

                    GameObject gameobjectOfTheMiniMapElement = miniMapMatrix[i, j];

                    float coordX = gameobjectOfTheMiniMapElement.transform.position.x;
                    float coordY = gameobjectOfTheMiniMapElement.transform.position.y;
                    float coordZ = gameobjectOfTheMiniMapElement.transform.position.z;

                    Room roomComponentOfTheRoom = gameobjectOfTheRoom.GetComponent<Room>();

                    List<string> roomIcons = new List<string>();

                    #endregion

                    #region Adding icons

                    if (roomComponentOfTheRoom.RoomType != "regular") { 
                        roomIcons.Add(roomComponentOfTheRoom.RoomType);
                    }

                    if (roomComponentOfTheRoom.RoomStuff != null) {
                        roomIcons.Add(roomComponentOfTheRoom.RoomStuff);
                    }

                    if (!roomComponentOfTheRoom.isThereAnyEnemy()) {
                        roomIcons.Add("no enemy");
                    }

                    roomIcons.Add("chest");

                    #endregion

                    #region Rendering icons

                    if (roomIcons.Count == 0) {
                        break;
                    } else if (roomIcons.Count == 1) {
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                            new Vector3(coordX, coordY, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                    } else if (roomIcons.Count == 2) {
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                            new Vector3(coordX - 1.75f, coordY, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                            new Vector3(coordX + 1.75f, coordY, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                    } else if (roomIcons.Count == 3) {
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                            new Vector3(coordX - 1.75f, coordY + 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                            new Vector3(coordX + 1.75f, coordY + 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[2]],
                            new Vector3(coordX, coordY - 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                    } else if (roomIcons.Count == 4) {
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                            new Vector3(coordX - 1.75f, coordY + 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                            new Vector3(coordX + 1.75f, coordY + 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[2]],
                            new Vector3(coordX - 1.75f, coordY - 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[3]],
                            new Vector3(coordX + 1.75f, coordY - 1.75f, coordZ),
                            Quaternion.identity,
                            this.transform.GetChild(1).GetChild(0));
                    }

                    #endregion

                }
            }
        }
    }

    #endregion

}
