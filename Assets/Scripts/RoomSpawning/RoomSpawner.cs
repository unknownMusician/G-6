using G6.Data;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Other;
using UnityEngine;
using UnityEngine.Rendering;

namespace G6.RoomSpawning
{
    public class RoomSpawner : MonoBehaviour
    {

        #region Properties

        public int amountOfRooms;

        private (int x, int y) finishRoomCoord;

        #region Matrixes

        public PlaceForRoom[,] roomDirectionsDataMatrix;
        public GameObject[,] RoomsMatrix { get; private set; }
        public GameObject[,] MiniMapMatrix { get; private set; }

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

        private Dictionary<string, GameObject> mapElementsPrefabsDictionary = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject[]> roomsPrefabsDictionary = new Dictionary<string, GameObject[]>();

        private Dictionary<string, GameObject> mapIconsPrefabsDictionary { get; set; } =
            new Dictionary<string, GameObject>();

        #endregion

        #endregion

        #region Methods

        #region MonoBehaviour methods

        private void Awake()
        {

            #region Matrixes initialization

            roomDirectionsDataMatrix = new PlaceForRoom[rows, columns];
            RoomsMatrix = new GameObject[rows, columns];
            MiniMapMatrix = new GameObject[rows, columns];

            #endregion

            #region Current row and column initialization

            CurrentColumn = columns / 2;
            CurrentRow = rows / 2;

            #endregion

            #region Creating base room

            PlaceForRoom baseRoom = new PlaceForRoom(true);
            roomDirectionsDataMatrix[CurrentRow, CurrentColumn] = baseRoom;
            amountOfRooms -= 1;

            #endregion

            LoadData();

            MainData.RoomSpawner = this;
        }
        private void OnDestroy() => MainData.RoomSpawner = null;

        private void Start()
        {

            CreateDungeon();

            SpawnDungeon();

            AddSecretRoom(3);
            AddSecretRoom(4);

            SpawnDungeonMap();

            renderIconsOnMinimap();

            #region Teleporting player to base room

            MainData.PlayerBehaviour.transform.localPosition = RoomsMatrix[CurrentRow, CurrentColumn].transform.GetChild(3).GetChild(0).position;

            #endregion

        }

        #endregion

        #region Service methods

        private void LoadData()
        {
            #region mapElementsPrefabsDictionary initialization and filling

            string path = "Prefabs/RoomSpawning/Rooms/Minimap/";

            mapElementsPrefabsDictionary.Add("2000", Resources.Load<GameObject>($"{path}1 exit/T"));
            mapElementsPrefabsDictionary.Add("0200", Resources.Load<GameObject>($"{path}1 exit/R"));
            mapElementsPrefabsDictionary.Add("0020", Resources.Load<GameObject>($"{path}1 exit/B"));
            mapElementsPrefabsDictionary.Add("0002", Resources.Load<GameObject>($"{path}1 exit/L"));
            mapElementsPrefabsDictionary.Add("2200", Resources.Load<GameObject>($"{path}2 exit/TR"));
            mapElementsPrefabsDictionary.Add("2020", Resources.Load<GameObject>($"{path}2 exit/TB"));
            mapElementsPrefabsDictionary.Add("2002", Resources.Load<GameObject>($"{path}2 exit/TL"));
            mapElementsPrefabsDictionary.Add("0220", Resources.Load<GameObject>($"{path}2 exit/RB"));
            mapElementsPrefabsDictionary.Add("0202", Resources.Load<GameObject>($"{path}2 exit/RL"));
            mapElementsPrefabsDictionary.Add("0022", Resources.Load<GameObject>($"{path}2 exit/BL"));
            mapElementsPrefabsDictionary.Add("0222", Resources.Load<GameObject>($"{path}3 exit/Not T"));
            mapElementsPrefabsDictionary.Add("2022", Resources.Load<GameObject>($"{path}3 exit/Not R"));
            mapElementsPrefabsDictionary.Add("2202", Resources.Load<GameObject>($"{path}3 exit/Not B"));
            mapElementsPrefabsDictionary.Add("2220", Resources.Load<GameObject>($"{path}3 exit/Not L"));
            mapElementsPrefabsDictionary.Add("2222", Resources.Load<GameObject>($"{path}Base"));
            mapElementsPrefabsDictionary.Add("0000", Resources.Load<GameObject>($"{path}Block"));

            #endregion

            #region roomsPrefabsDictionary initialization and filling

            path = "Prefabs/RoomSpawning/Rooms/Rooms/";

            roomsPrefabsDictionary.Add("2000", Resources.LoadAll<GameObject>($"{path}1 exit/T"));
            roomsPrefabsDictionary.Add("0200", Resources.LoadAll<GameObject>($"{path}1 exit/R"));
            roomsPrefabsDictionary.Add("0020", Resources.LoadAll<GameObject>($"{path}1 exit/B"));
            roomsPrefabsDictionary.Add("0002", Resources.LoadAll<GameObject>($"{path}1 exit/L"));
            roomsPrefabsDictionary.Add("2200", Resources.LoadAll<GameObject>($"{path}2 exit/TR"));
            roomsPrefabsDictionary.Add("2020", Resources.LoadAll<GameObject>($"{path}2 exit/TB"));
            roomsPrefabsDictionary.Add("2002", Resources.LoadAll<GameObject>($"{path}2 exit/TL"));
            roomsPrefabsDictionary.Add("0220", Resources.LoadAll<GameObject>($"{path}2 exit/RB"));
            roomsPrefabsDictionary.Add("0202", Resources.LoadAll<GameObject>($"{path}2 exit/RL"));
            roomsPrefabsDictionary.Add("0022", Resources.LoadAll<GameObject>($"{path}2 exit/BL"));
            roomsPrefabsDictionary.Add("0222", Resources.LoadAll<GameObject>($"{path}3 exit/Not T"));
            roomsPrefabsDictionary.Add("2022", Resources.LoadAll<GameObject>($"{path}3 exit/Not R"));
            roomsPrefabsDictionary.Add("2202", Resources.LoadAll<GameObject>($"{path}3 exit/Not B"));
            roomsPrefabsDictionary.Add("2220", Resources.LoadAll<GameObject>($"{path}3 exit/Not L"));
            roomsPrefabsDictionary.Add("2222", Resources.LoadAll<GameObject>($"{path}Base"));
            roomsPrefabsDictionary.Add("0000", Resources.LoadAll<GameObject>($"{path}Block"));

            #endregion

            #region mapIconsPrefabsDictionary initialization and filling

            path = "Prefabs/RoomSpawning/Rooms/Minimap/Icons/";

            mapIconsPrefabsDictionary.Add("start", Resources.Load<GameObject>($"{path}start"));
            mapIconsPrefabsDictionary.Add("finish", Resources.Load<GameObject>($"{path}finish"));
            mapIconsPrefabsDictionary.Add("boss", Resources.Load<GameObject>($"{path}boss"));
            mapIconsPrefabsDictionary.Add("chest", Resources.Load<GameObject>($"{path}chest"));
            mapIconsPrefabsDictionary.Add("market", Resources.Load<GameObject>($"{path}market"));
            mapIconsPrefabsDictionary.Add("no enemy", Resources.Load<GameObject>($"{path}no enemy"));

            #endregion
        }

        #endregion

        #region Generate and spawn methods

        private void CreateDungeon()
        {
            GenerateDungeon();
            AddRoomsToFitAmount();
            CloseDoorsToNowhere();
        }

        private void GenerateDungeon()
        {
            for (int i = 0; i < rows && amountOfRooms > 0; i++)
            {
                for (int j = 0; j < columns && amountOfRooms > 0; j++)
                {

                    PlaceForRoom place = roomDirectionsDataMatrix[i, j];

                    // Creating PlaceForRoom objects for every place for room
                    if (place == null)
                    {
                        roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                        place = roomDirectionsDataMatrix[i, j];
                    }

                    PlaceForRoom rddm;
                    place.topDoor = (i - 1 >= 0) && ((rddm = roomDirectionsDataMatrix[i - 1, j]) != null) ? rddm.bottomDoor : false;
                    place.rightDoor = (j + 1 < columns) && ((rddm = roomDirectionsDataMatrix[i, j + 1]) != null) ? rddm.leftDoor : false;
                    place.bottomDoor = (i + 1 < rows) && ((rddm = roomDirectionsDataMatrix[i + 1, j]) != null) ? rddm.topDoor : false;
                    place.leftDoor = (j - 1 >= 0) && ((rddm = roomDirectionsDataMatrix[i, j - 1]) != null) ? rddm.rightDoor : false;

                    // Random door filling
                    if ((place != null) && (place.AtLeastOneDoor))
                    {
                        if (place.topDoor == null) { place.topDoor = DoWeNeedAnotherDoor(place.DoorsAmount, Random.value); }
                        if (place.rightDoor == null) { place.rightDoor = DoWeNeedAnotherDoor(place.DoorsAmount, Random.value); }
                        if (place.bottomDoor == null) { place.bottomDoor = DoWeNeedAnotherDoor(place.DoorsAmount, Random.value); }
                        if (place.leftDoor == null) { place.leftDoor = DoWeNeedAnotherDoor(place.DoorsAmount, Random.value); }
                        amountOfRooms -= 1;
                    }
                }
            }
        }
        private void AddRoomsToFitAmount()
        {
            while (amountOfRooms >= 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {

                        PlaceForRoom place = roomDirectionsDataMatrix[i, j];

                        if (place == null || !DoWeNeedAnotherDoor(place.DoorsAmount, Random.value) || !place.AtLeastOneDoor) { continue; }

                        PlaceForRoom currPlace;

                        if ((i > 0) && (amountOfRooms >= 0) && (currPlace = roomDirectionsDataMatrix[i - 1, j]) != null && !currPlace.AtLeastOneDoor)
                        {
                            place.topDoor = true;
                            currPlace.DoorParams = (false, false, true, false);
                            if (amountOfRooms == 0) { finishRoomCoord = (i - 1, j); }
                            amountOfRooms--;
                        }

                        if ((j < columns - 1) && (amountOfRooms >= 0) && (currPlace = roomDirectionsDataMatrix[i, j + 1]) != null && !currPlace.AtLeastOneDoor)
                        {
                            place.rightDoor = true;
                            currPlace.DoorParams = (false, false, false, true);
                            if (amountOfRooms == 0) { finishRoomCoord = (i, j + 1); }
                            amountOfRooms--;
                        }

                        if ((i < rows - 1) && (amountOfRooms >= 0) && (currPlace = roomDirectionsDataMatrix[i + 1, j]) != null && !currPlace.AtLeastOneDoor)
                        {
                            place.bottomDoor = true;
                            currPlace.DoorParams = (true, false, false, false);
                            if (amountOfRooms == 0) { finishRoomCoord = (i + 1, j); }
                            amountOfRooms--;
                        }

                        if ((j > 0) && (amountOfRooms >= 0) && (currPlace = roomDirectionsDataMatrix[i, j - 1]) != null && !currPlace.AtLeastOneDoor)
                        {
                            place.leftDoor = true;
                            currPlace.DoorParams = (false, true, false, false);
                            if (amountOfRooms == 0) { finishRoomCoord = (i, j - 1); }
                            amountOfRooms--;
                        }
                    }
                }
            }
        }
        private void CloseDoorsToNowhere()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    PlaceForRoom place = roomDirectionsDataMatrix[i, j];
                    if (amountOfRooms >= 0 || place == null || place.AtLeastOneDoor) { continue; }

                    PlaceForRoom rddm;
                    if (i > 0 && ((rddm = roomDirectionsDataMatrix[i - 1, j]) == null || !rddm.AtLeastOneDoor || rddm.bottomDoor == false)
                        ) { place.topDoor = false; }
                    if (j + 1 < columns && ((rddm = roomDirectionsDataMatrix[i, j + 1]) == null || !rddm.AtLeastOneDoor || rddm.leftDoor == false)
                        ) { place.rightDoor = false; }
                    if (i + 1 < rows && ((rddm = roomDirectionsDataMatrix[i + 1, j]) == null || !rddm.AtLeastOneDoor || rddm.topDoor == false) // todo: Exception
                        ) { place.bottomDoor = false; }
                    if (j > 0 && ((rddm = roomDirectionsDataMatrix[i, j - 1]) == null || !rddm.AtLeastOneDoor || rddm.rightDoor == false)
                        ) { place.leftDoor = false; }
                }
            }
        }

        private void SpawnDungeonMap()
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    if (roomDirectionsDataMatrix[i, j] == null) { continue; }

                    string key = roomDirectionsDataMatrix[i, j].ToString();

                    if (mapElementsPrefabsDictionary.ContainsKey(key))
                    {
                        MiniMapMatrix[i, j] = Instantiate(mapElementsPrefabsDictionary[key],
                            new Vector2(j * 10 - 300, -i * 10 + 300),
                            Quaternion.identity,
                            transform.GetChild(1).GetChild(1)
                            );
                    }
                }
            }
            // Coloring base room mini-map element to red, because it is the first active room
            MiniMapMatrix[CurrentRow, CurrentColumn].GetComponent<SpriteRenderer>().color = Color.red;
        }

        private void SpawnDungeon()
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    string key = roomDirectionsDataMatrix[i, j]?.ToString();

                    if (key == null || !mapElementsPrefabsDictionary.ContainsKey(key)) { continue; }
                    if (key == "0000")
                        continue;
                    (RoomsMatrix[i, j] = Instantiate(
                        roomsPrefabsDictionary[key][Random.Range(0, roomsPrefabsDictionary[key].Length)],
                        new Vector2(j * 250, -i * 200),
                        Quaternion.identity,
                        transform.GetChild(0)
                        )
                    ).SetActive((i == rows / 2) && (j == columns / 2));
                }
            }
            // Last added room = finish room, base room = start room
            RoomsMatrix[finishRoomCoord.x, finishRoomCoord.y].GetComponent<Room>().RoomType = RoomType.finish;
            RoomsMatrix[finishRoomCoord.x, finishRoomCoord.y].GetComponent<Room>().RoomStuff = "boss";

            ActiveRoom.RoomType = RoomType.start;
            ActiveRoom.RoomStuff = "market";
        }

        private void AddSecretRoom(int countOfNeighbors)
        {
            string etalonKey = "0000";

            List<SecretPretendent> pretendentForSecretRoom = new List<SecretPretendent>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (roomDirectionsDataMatrix[i, j]?.ToString() == etalonKey)
                    {
                        List<Pair<int, int>> neighbors = GetNeighbors(i, j);
                        if (neighbors.Count >= countOfNeighbors)
                        {
                            pretendentForSecretRoom.Add(new SecretPretendent() { Coordinate = new Pair<int, int>(i, j), Neighbors = neighbors });
                        }
                    }
                }
            }
            if (pretendentForSecretRoom.Count > 0)
            {
                int indexForRoom = Random.Range(0, pretendentForSecretRoom.Count);
                int i = pretendentForSecretRoom[indexForRoom].Coordinate.First;
                int j = pretendentForSecretRoom[indexForRoom].Coordinate.Second;
                (RoomsMatrix[i, j] = Instantiate(
                            roomsPrefabsDictionary[etalonKey][Random.Range(0, roomsPrefabsDictionary[etalonKey].Length)],
                            new Vector2(j * 250, -i * 200),
                            Quaternion.identity,
                            transform.GetChild(0)
                        )
                    ).SetActive((i == rows / 2) && (j == columns / 2));
                if (countOfNeighbors == 3)
                    RoomsMatrix[i, j].GetComponent<Room>().RoomType = RoomType.secret;
                if (countOfNeighbors == 4)
                    RoomsMatrix[i, j].GetComponent<Room>().RoomType = RoomType.TopSecret;

                int indexForNeighbors = Random.Range(0, pretendentForSecretRoom[indexForRoom].Neighbors.Count);
                int i1 = pretendentForSecretRoom[indexForRoom].Neighbors[indexForNeighbors].First;
                int j1 = pretendentForSecretRoom[indexForRoom].Neighbors[indexForNeighbors].Second;
                RoomsMatrix[i1, j1].GetComponent<Room>().RoomType = RoomType.EntranceToTheSecretRoom;
            }
        }

        private class SecretPretendent
        {
            public Pair<int, int> Coordinate { get; set; }
            public List<Pair<int, int>> Neighbors { get; set; }
        }

        private List<Pair<int, int>> GetNeighbors(int i, int j)
        {
            List<Pair<int, int>> coordinatesOfNeighbors = new List<Pair<int, int>>();
            if (i + 1 < rows && RoomsMatrix[i + 1, j] != null)
                coordinatesOfNeighbors.Add(new Pair<int, int>(i + 1, j));
            if (i - 1 >= 0 && RoomsMatrix[i - 1, j] != null)
                coordinatesOfNeighbors.Add(new Pair<int, int>(i - 1, j));
            if (j + 1 < columns && RoomsMatrix[i, j + 1] != null)
                coordinatesOfNeighbors.Add(new Pair<int, int>(i, j + 1));
            if (j - 1 >= 0 && RoomsMatrix[i, j - 1] != null)
                coordinatesOfNeighbors.Add(new Pair<int, int>(i, j - 1));
            return coordinatesOfNeighbors;
        }

        #endregion

        #region Properties of active room components

        public Vector2 ActiveRoomCoordinates => new Vector2(CurrentColumn * 250, -CurrentRow * 200);
        public GameObject ActiveRoomGameObject => RoomsMatrix[CurrentRow, CurrentColumn];
        public Room ActiveRoom => ActiveRoomGameObject.GetComponent<Room>();
        public Transform getTransformComponentOfTheActiveRoom => ActiveRoomGameObject.transform;

        #endregion

        #region Overloads of IsThereAnyEnemy()

        public bool IsThereAnyEnemy(int row, int column) => RoomsMatrix[row, column].transform.GetChild(2).childCount != 0;
        public bool IsThereAnyEnemy(Transform room) => room.GetChild(2).childCount != 0;

        #endregion

        #region Methods of going to next room operations

        public void GoToNextRoom(Transform playerTransform, int direction)
        {

            GameObject currentRoomGameObject = RoomsMatrix[CurrentRow, CurrentColumn];
            GameObject currentRoomMiniMapElement = MiniMapMatrix[CurrentRow, CurrentColumn];

            Room nextRoom = GetNextRoomGameObject(direction).GetComponent<Room>();
            GameObject nextRoomMiniMapElement = GetNextRoomMiniMapElement(direction);

            nextRoom.gameObject.SetActive(true);
            nextRoom.makeAllDoorsUnvisited();
            nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;

            playerTransform.position = GetNextRoomSpawnpoint(direction, nextRoom).position;

            FocusCameraOnANewRoom(nextRoomMiniMapElement.transform);

            currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
            currentRoomGameObject.SetActive(false);

            ChangeCurrentCoordAfterGoingToNextRoom(direction);

        }

        public GameObject GetNextRoomGameObject(int direction)
        {
            if (direction == 0)
            {
                return RoomsMatrix[CurrentRow - 1, CurrentColumn];
            }
            else if (direction == 1)
            {
                return RoomsMatrix[CurrentRow, CurrentColumn + 1];
            }
            else if (direction == 2)
            {
                return RoomsMatrix[CurrentRow + 1, CurrentColumn];
            }
            else if (direction == 3)
            {
                return RoomsMatrix[CurrentRow, CurrentColumn - 1];
            }
            return null;
        }

        public GameObject GetNextRoomMiniMapElement(int direction)
        {
            if (direction == 0)
            {
                return MiniMapMatrix[CurrentRow - 1, CurrentColumn];
            }
            else if (direction == 1)
            {
                return MiniMapMatrix[CurrentRow, CurrentColumn + 1];
            }
            else if (direction == 2)
            {
                return MiniMapMatrix[CurrentRow + 1, CurrentColumn];
            }
            else if (direction == 3)
            {
                return MiniMapMatrix[CurrentRow, CurrentColumn - 1];
            }
            return null;
        }

        public Transform GetNextRoomSpawnpoint(int direction, Room nextRoom)
        {
            if (direction == 0)
            {
                return nextRoom.BottomSpawnpoint;
            }
            else if (direction == 1)
            {
                return nextRoom.LeftSpawnpoint;
            }
            else if (direction == 2)
            {
                return nextRoom.TopSpawnpoint;
            }
            else if (direction == 3)
            {
                return nextRoom.RightSpawnpoint;
            }
            return null;
        }

        public void FocusCameraOnANewRoom(Transform nextRoomMiniMapElement)
        {
            float coordX = nextRoomMiniMapElement.position.x;
            float coordY = nextRoomMiniMapElement.position.y;
            float coordZ = transform.GetChild(2).position.z;
            this.transform.GetChild(2).position = new Vector3(coordX, coordY, coordZ);
        }

        public void ChangeCurrentCoordAfterGoingToNextRoom(int direction)
        {
            if (direction == 0)
            {
                CurrentRow -= 1;
            }
            else if (direction == 1)
            {
                CurrentColumn += 1;
            }
            else if (direction == 2)
            {
                CurrentRow += 1;
            }
            else if (direction == 3)
            {
                CurrentColumn -= 1;
            }
        }

        #endregion

        public bool DoWeNeedAnotherDoor(byte amountOfDoors, float randomNumber)
        {
            bool answer = false;
            answer |= (amountOfDoors == 0) && (randomNumber > 0.5f);
            answer |= (amountOfDoors == 1) && (randomNumber > 0.85f);
            answer |= (amountOfDoors == 2) && (randomNumber > 0.95f);
            answer |= (amountOfDoors == 3) && (randomNumber > 0.99f);
            return answer;
        }

        public void renderIconsOnMinimap()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    Room room = RoomsMatrix[i, j]?.GetComponent<Room>();

                    if (room == null) { continue; }

                    #region Properties initialization

                    Vector2 miniMapPos = MiniMapMatrix[i, j].transform.position;

                    float coordX = miniMapPos.x;
                    float coordY = miniMapPos.y;

                    #endregion

                    #region Adding icons

                    List<string> roomIcons = new List<string>();

                    if (room.RoomType != RoomType.regular) { roomIcons.Add(room.RoomType.ToString()); }
                    if (room.RoomStuff != null) { roomIcons.Add(room.RoomStuff); }
                    if (!room.isThereAnyEnemy()) { roomIcons.Add("no enemy"); }

                    roomIcons.Add("chest");

                    #endregion

                    #region Rendering icons
                    if (roomIcons.Count == 0) { break; }

                    System.Func<int, int, float> calcX = (int k, int l)
                        =>
                    { return l == 1 ? 0 : l % 2 == 0 ? k % 2 == 0 ? -1.75f : 1.75f : k == 0 ? -1.75f : k == 1 ? 1.75f : 0; };

                    System.Func<int, int, float> calcY = (int k, int l) => { return l < 3 ? 0 : k < 2 ? 1.75f : -1.75f; };

                    for (int k = 0; k < roomIcons.Count; k++)
                    {
                        Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                            new Vector3(coordX + calcX(k, roomIcons.Count), coordY + calcY(k, roomIcons.Count), 0),
                            Quaternion.identity,
                            transform.GetChild(1).GetChild(0));
                    }

                    //if (roomIcons.Count == 0) {
                    //    break;
                    //} else if (roomIcons.Count == 1) {
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                    //        new Vector3(coordX, coordY, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //} else if (roomIcons.Count == 2) {
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                    //        new Vector3(coordX - 1.75f, coordY, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                    //        new Vector3(coordX + 1.75f, coordY, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //} else if (roomIcons.Count == 3) {
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                    //        new Vector3(coordX - 1.75f, coordY + 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                    //        new Vector3(coordX + 1.75f, coordY + 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[2]],
                    //        new Vector3(coordX, coordY - 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //} else if (roomIcons.Count == 4) {
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[0]],
                    //        new Vector3(coordX - 1.75f, coordY + 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[1]],
                    //        new Vector3(coordX + 1.75f, coordY + 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[2]],
                    //        new Vector3(coordX - 1.75f, coordY - 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //    Instantiate(mapIconsPrefabsDictionary[roomIcons[3]],
                    //        new Vector3(coordX + 1.75f, coordY - 1.75f, 0),
                    //        Quaternion.identity,
                    //        this.transform.GetChild(1).GetChild(0));
                    //}

                    #endregion
                }
            }
        }

        #endregion

    }
}