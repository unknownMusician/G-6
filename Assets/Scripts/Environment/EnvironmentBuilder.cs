using G6.Characters;
using G6.Data;
using G6.Environment.Interactables.Base;
using G6.RoomSpawning;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using G6.UI;

/// done:
/// - Layers placing object +
/// - Ability to PlayTest +
///     - Choose where to start playtest +
/// 
/// todo:
/// - Saving multiple rooms + (with name +)
///     - Read doors and SpawnPoint position +
///     - Spawnpoint restrictions +
///     - Connect to RoomSpawner
///         - Sort rooms by type
/// 
/// - Load Grid info from Room
///     
/// - Gamepad input
///     - Placing/Deleting
///     - Read position
///     - UI & Menu

namespace G6.Environment {
    public sealed class EnvironmentBuilder : MonoBehaviour {

        public static EnvironmentBuilder instance { get; private set; }

        [SerializeField] private UI ui = default;
        [SerializeField] private RoomCreator roomCreator = default;
        [SerializeField] private Selected selected = default;
        [SerializeField] private Common common = default;
        [SerializeField] private PlayTest playTest = default;
        [SerializeField] private Shortcuts shortcuts = default;

        private GameObject cursor { get; set; }
        private Vector2 MouseGridPosition => NormalizeByGrid(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), selected.GridSize);
        private Vector2 NormalizeByGrid(Vector2 worldPos, float gridSize)
            => new Vector2(Mathf.Round(worldPos.x / gridSize) * gridSize, Mathf.Round(worldPos.y / gridSize) * gridSize);
        public void SetLayer(int layer) => selected.UserLayer = (UserLayer)layer;

        #region Input & Coroutines

        private Coroutine coroutine;
        public void DeleteStart() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            coroutine = StartCoroutine(Deleting());
        }
        public void DeleteStop() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        public void PlaceStart() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            if (selected.ItemID == 0) { return; }
            coroutine = StartCoroutine(Placing());
        }
        public void PlaceStop() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        private IEnumerator Placing() { // todo: interpolate
            while (true) {

                // creating new
                if ((MouseGridPosition.x <= common.RoomSize.x) &&
                    (MouseGridPosition.y <= common.RoomSize.y) &&
                    (MouseGridPosition.x >= 0) &&
                    (MouseGridPosition.y >= 0)) {

                    roomCreator.PlaceItem(MouseGridPosition);

                }
                yield return null;
            }
        }
        private IEnumerator Deleting() { // todo: interpolate
            while (true) {
                roomCreator.DeleteItem(MouseGridPosition);
                yield return null;
            }
        }
        #endregion

        #region Mono
        public void Awake() {
            // Singleton
            instance = this;
            // Creating cursor
            cursor = new GameObject("alphaSprite");
            cursor.AddComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
            // Inner classes
            common.Start(this);
            selected.Start(this);
            ui.Start(this);
            roomCreator.Start(this);
            playTest.Start(this);
            shortcuts.Start(this);
            // MainData
            MainData.EnvironmentBuilderObject = this.gameObject;
        }
        private void Start() {
            SelectItem(0);
        }
        private void OnDestroy() {
            MainData.EnvironmentBuilderObject = null;
            instance = null;
        }
        private void Update() {
            cursor.transform.position = MouseGridPosition;
        }
        #endregion

        private void SelectItem(int itemID) => selected.ItemID = itemID;
        public void SaveBtn() => roomCreator.SaveRoom();
        public void PlayTestBtn() => playTest.Play();
        public void PlayTestDirectionBtn(int dir) => playTest.PlayTestStartDir = dir;

        private void OnDrawGizmos() {
            ui.OnDrawGizmos();

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(Vector2.up * common.BlockSize, Vector2.one * common.BlockSize);
            Gizmos.DrawLine(Vector2.one * common.BlockSize, Vector2.right * common.BlockSize);
        }

        public enum UserLayer { Background, Ground, Foreground, Objects, Specials }

        [System.Serializable]
        public sealed class UI {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;

                blockButtonPrefab = Resources.Load<GameObject>("Prefabs/EnvironmentBuilder/UI/BlockButton");
                barrierPrefab = Resources.Load<GameObject>("Prefabs/EnvironmentBuilder/UI/Barrier");

                ShowBuildBarrier();
                var common = e.common;
                FillMenu(blocksMenu, common.BlocksPrefabs);
                FillMenu(objectsMenu, common.ObjectsPrefabs);
                FillMenu(specialsMenu, common.SpecialsPrefabs);
            }
            #endregion

            [SerializeField, Space, Space]
            private GameObject blocksMenu = null;
            [SerializeField]
            private GameObject objectsMenu = null;
            [SerializeField]
            private GameObject specialsMenu = null;
            [SerializeField]
            private GameObject layerMenu = null;
            [SerializeField]
            private Dropdown sideChooseDropdown = null;
            [SerializeField]
            private InputField fileNameInput = null;

            private GameObject blockButtonPrefab = default;
            private GameObject barrierPrefab = default;

            public Enums.Side PlayTestSide => (Enums.Side)sideChooseDropdown.value;
            public string SaveFileName => fileNameInput.text;

            private Vector3 RoomTopRightCorner => new Vector3(e.common.RoomSize.x * e.common.BlockSize, e.common.RoomSize.y * e.common.BlockSize, 0);
            private Vector3 RoomTopLeftCorner => new Vector3(0, e.common.RoomSize.y * e.common.BlockSize, 0);
            private Vector3 RoomBottomRightCorner => new Vector3(e.common.RoomSize.x * e.common.BlockSize, 0, 0);
            private Vector3 RoomBottomLeftCorner => Vector3.zero;

            private void FillMenu(GameObject menu, GameObject[] prefabs) {
                for (int j = 0; ; j++) {
                    for (int i = 0; i <= 3; i++) {
                        int currentId = j * 4 + i;
                        if (currentId >= prefabs.Length)
                            return;
                        var btn = Instantiate(blockButtonPrefab, menu.transform);
                        btn.transform.localPosition = new Vector2(-150 + i * 100, 490 - j * 100);
                        btn.GetComponent<Image>().sprite = prefabs[currentId].GetComponent<SpriteRenderer>().sprite;
                        btn.GetComponent<Button>().onClick.AddListener(new UnityAction(() => e.SelectItem(currentId)));
                    }
                }
            }
            private void ShowBuildBarrier() {
                var barrier = Instantiate(barrierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                barrier.transform.position = new Vector2((RoomTopRightCorner.x + RoomBottomLeftCorner.x) / 2, (RoomTopRightCorner.y + RoomBottomLeftCorner.y) / 2);
                barrier.transform.GetChild(1).localScale =
                    new Vector2(RoomTopRightCorner.x - RoomBottomLeftCorner.x + e.common.BlockSize, RoomTopRightCorner.y - RoomBottomLeftCorner.y + e.common.BlockSize);
            }

            public void OnLayerChange(UserLayer newLayer) {
                // LayerButtons
                for (int i = 0; i < layerMenu.transform.childCount; i++) {
                    layerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
                }
                layerMenu.transform.GetChild((int)newLayer).gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                // PrefabMenus
                objectsMenu.SetActive(false);
                specialsMenu.SetActive(false);
                blocksMenu.SetActive(false);
                switch (newLayer) {
                    case UserLayer.Objects:
                        objectsMenu.SetActive(true);
                        break;
                    case UserLayer.Specials:
                        specialsMenu.SetActive(true);
                        break;
                    default:
                        blocksMenu.SetActive(true);
                        break;
                }
            }
            public void OnDrawGizmos() {
                if (e == null) { return; }
                Gizmos.color = Color.green;

                Gizmos.DrawLine(RoomTopLeftCorner, RoomTopRightCorner);
                Gizmos.DrawLine(RoomTopRightCorner, RoomBottomRightCorner);
                Gizmos.DrawLine(RoomBottomRightCorner, RoomBottomLeftCorner);
                Gizmos.DrawLine(RoomBottomLeftCorner, RoomTopLeftCorner);
            }
        }
        [System.Serializable]
        public sealed class RoomCreator {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;
                if (!LoadRoomTmp()) { CreateRoom(); }
            }
            #endregion
            public Transform Room { get; private set; } = default;

            public Transform Background { get; private set; } = default;
            public Transform Walls { get; private set; } = default;
            public Transform Foreground { get; private set; } = default;
            public Transform Doors { get; private set; } = default;
            public Transform Enemies { get; private set; } = default;
            public Transform Objects { get; private set; } = default;
            public Transform Specials { get; private set; } = default;
            public Transform SpawnPoints { get; private set; } = default;

            public const string pathAssets = "Assets/Resources/Prefabs/EnvironmentBuilder/Rooms/";
            public const string pathResources = "Prefabs/EnvironmentBuilder/Rooms/";

            private void CreateRoom() {
                // New Room
                Room = new GameObject("Room").transform;
                // New RoomParts
                (Background = new GameObject("Background").transform).SetParent(Room);
                (Doors = new GameObject("Doors").transform).SetParent(Room);
                (Enemies = new GameObject("Enemies").transform).SetParent(Room);
                (SpawnPoints = new GameObject("SpawnPoints").transform).SetParent(Room);
                (Walls = new GameObject("Walls").transform).SetParent(Room);
                (Foreground = new GameObject("Foreground").transform).SetParent(Room);
                (Objects = new GameObject("Objects").transform).SetParent(Room);
                (Specials = new GameObject("Specials").transform).SetParent(Room);
            }
            public void SaveRoom() { // add different folders for different types of rooms
                if (Room.gameObject.GetComponent<Room>() == null) {
                    Room.gameObject.AddComponent<Room>();
                }
                string fileName = e.ui.SaveFileName;
                if (fileName == "" || fileName == "Room_tmp") {
                    for (int i = 1; i < 100; i++) { // todo: restriction of 98 files
                        if (!System.IO.File.Exists($"{pathAssets}Room_{i}.prefab")) {
                            PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}Room_{i}.prefab");
                            return;
                        }
                    }
                } else {
                    if (!System.IO.File.Exists($"{pathAssets}{fileName}.prefab")) {
                        PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}{fileName}.prefab");
                        return;
                    }
                }
            }
            public void SaveRoomTmp() {
                if (Room.gameObject.GetComponent<Room>() == null) {
                    Room.gameObject.AddComponent<Room>();
                }
                PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}Room_tmp.prefab");
                // todo
            }
            public bool LoadRoomTmp() {
                if (!System.IO.File.Exists($"{pathAssets}Room_tmp.prefab")) { return false; }
                var roomPrefab = Resources.Load<GameObject>($"{pathResources}Room_tmp");
                var roomObj = Instantiate(roomPrefab, Vector2.zero, Quaternion.identity);
                roomObj.name = "Room";
                // Load Room
                Room = roomObj.transform;
                // Load RoomParts
                Background = Room.GetChild(0);
                Doors = Room.GetChild(3);
                Enemies = Room.GetChild(4);
                SpawnPoints = Room.GetChild(7);
                Walls = Room.GetChild(1);
                Foreground = Room.GetChild(2);
                Objects = Room.GetChild(5);
                Specials = Room.GetChild(6);
                // Delete saveFile
                DeleteRoomTmp();

                if (Room.gameObject.GetComponent<Room>() != null) {
                    Destroy(Room.gameObject.GetComponent<Room>());
                }

                return true;
                // todo
            }
            public void DeleteRoomTmp() {
                string path = $"{pathAssets}Room_tmp.prefab";
                if (System.IO.File.Exists(path)) {
                    System.IO.File.Delete(path);
                    System.IO.File.Delete($"{path}.meta");
                }
            }

            public void PlaceItem(Vector2 gridPosition) {

                GameObject clone = PrefabUtility.InstantiatePrefab(e.selected.Prefab) as GameObject;

                clone.transform.position = gridPosition;
                if (!CheckAndSetItemProperties(clone.transform)) {
                    Destroy(clone);
                    return;
                }

                // deleting previous
                DeleteItem(gridPosition);

                e.selected.Grid.Add(gridPosition, clone);
                CheckNeighbours(gridPosition, e.selected.GridSize, e.selected.Grid);
            }
            public void DeleteItem(Vector2 gridPosition) {

                var selectedDict = e.selected.Grid;

                if (selectedDict.ContainsKey(gridPosition)) {
                    Destroy(selectedDict[gridPosition]);
                    selectedDict.Remove(gridPosition);
                }
            }

            private void CheckNeighbours(Vector2 gridPosition, float gridSize, Dictionary<Vector2, GameObject> grid) {
                var obj = grid[gridPosition];
                // 3x3 matrix-donut
                for (int i = -1; i < 2; i++) {
                    for (int j = -1; j < 2; j++) {
                        if (i == 0 && j == 0) { continue; }
                        var localNormNeighbourPos = new Vector2(i, j);
                        var neighbourPos = e.NormalizeByGrid(gridPosition + localNormNeighbourPos * gridSize, gridSize);

                        if (!grid.ContainsKey(neighbourPos)) { continue; }
                        CheckNeighbour(grid[neighbourPos], obj, -localNormNeighbourPos);
                    }
                }

            }
            private void CheckNeighbour(GameObject neighbourObj, GameObject changedBlock, Vector2 changedNormalizedDirection) {
                // todo: change Sprite to look more natural;
            }
            private bool CheckAndSetItemProperties(Transform item) {
                var itemObj = item.gameObject;
                var sr = itemObj.GetComponent<SpriteRenderer>();

                if (itemObj.GetComponent<Block>() != null) {
                    Behaviour componentTmp;
                    switch (e.selected.UserLayer) {
                        case UserLayer.Background:
                            item.SetParent(Background);
                            sr.sortingLayerName = "Background";
                            if ((componentTmp = item.GetComponent<Collider2D>()) != null) { Destroy(componentTmp); }
                            break;
                        case UserLayer.Foreground:
                            item.SetParent(Foreground);
                            sr.sortingLayerName = "Foreground";
                            if ((componentTmp = item.GetComponent<Collider2D>()) != null) { Destroy(componentTmp); }
                            break;
                        default: // (case UserLayer.Ground:)
                            item.SetParent(Walls);
                            sr.sortingLayerName = "Ground";
                            break;
                    }
                } else if (itemObj.GetComponent<CharacterBase>() != null) {
                    item.SetParent(Enemies);
                } else if (itemObj.GetComponent<InteractableBase>() != null) {
                    item.SetParent(Objects);
                } else if (e.selected.UserLayer == UserLayer.Specials) {
                    string itemName = itemObj.name;
                    if (itemObj.GetComponent<Door>() != null) {
                        item.SetParent(Doors);
                    } else if (itemName == "BottomSpawnpoint" ||
                         itemName == "LeftSpawnpoint" ||
                         itemName == "RightSpawnpoint" ||
                         itemName == "TopSpawnpoint"
                         ) {
                        if (SpawnPoints.Find(itemName) != null) { return false; }
                        item.SetParent(SpawnPoints);
                    }
                } else {
                    item.SetParent(Room);
                    sr.sortingLayerName = "UI";
                }
                return true;
                // todo
            }
        }
        [System.Serializable]
        public sealed class Common {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;
                LoadAssets();
            }
            #endregion

            [SerializeField] private float blockSize = 1;
            [SerializeField] private float objectSize = 0.5f;
            [SerializeField] private Vector2 roomSize = Vector2.one * 10;

            public float BlockSize => blockSize;
            public float ObjectSize => objectSize;
            public Vector2 RoomSize => roomSize;

            public GameObject[] BlocksPrefabs { get; private set; }
            public GameObject[] ObjectsPrefabs { get; private set; }
            public GameObject[] SpecialsPrefabs { get; private set; }

            public Dictionary<Vector2, GameObject> GroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> BackgroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ForegroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ObjectsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> SpecialsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();

            private void LoadAssets() {
                string path = "Prefabs/EnvironmentBuilder/Items/";
                var blankBlock = Resources.Load<GameObject>($"{path}BlockBases/000_BlockDefault");

                var blocksPrefabsList = new List<GameObject> { blankBlock };
                blocksPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Blocks"));
                BlocksPrefabs = blocksPrefabsList.ToArray();

                var specialsPrefabsList = new List<GameObject> { blankBlock };
                specialsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Specials"));
                SpecialsPrefabs = specialsPrefabsList.ToArray();

                path = "Prefabs/Weapons/";

                var objectsPrefabsList = new List<GameObject> { blankBlock };
                objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Cards"));
                objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Guns"));
                objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Melees"));
                ObjectsPrefabs = objectsPrefabsList.ToArray();
            }

        }
        [System.Serializable]
        public sealed class Selected {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;
                UserLayer = UserLayer.Ground;
            }
            #endregion

            private int _itemID = 0;
            public int ItemID {
                get => _itemID;
                set {
                    _itemID = value;
                    Prefab = ItemGroup[ItemID];
                    PrefabSprite = Prefab.GetComponent<SpriteRenderer>().sprite;
                    e.cursor.GetComponent<SpriteRenderer>().sprite = PrefabSprite;
                }
            }

            public Dictionary<Vector2, GameObject> Grid { get; private set; }
            public GameObject[] ItemGroup { get; private set; }
            public GameObject Prefab { get; private set; }
            public Sprite PrefabSprite { get; private set; }

            public float GridSize { get; private set; }

            private UserLayer _userLayer;
            public UserLayer UserLayer {
                get => _userLayer;
                set {
                    e.ui.OnLayerChange(_userLayer = value);
                    var common = e.common;

                    switch (UserLayer) {
                        case UserLayer.Background:
                            Grid = common.BackgroundGrid;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        case UserLayer.Foreground:
                            Grid = common.ForegroundGrid;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        case UserLayer.Objects:
                            Grid = common.ObjectsGrid;
                            ItemGroup = common.ObjectsPrefabs;
                            GridSize = common.ObjectSize;
                            break;
                        case UserLayer.Specials:
                            Grid = common.SpecialsGrid;
                            ItemGroup = common.SpecialsPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        default: // (case UserLayer.Ground:)
                            Grid = common.GroundGrid;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                    }

                    ItemID = 0; // changing blockID
                }
            }
        }
        [System.Serializable]
        public sealed class PlayTest {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;
                Pause.GameIsPaused = true;
            }
            #endregion

            public int PlayTestStartDir { get; set; } = default;

            public void Play() {
                e.roomCreator.SaveRoomTmp();
                BetweenScenes.EnvironmentBuilder.WhereToStart = e.ui.PlayTestSide;
                LevelManager.LoadPlayTest();
                // todo
            }

        }
        [System.Serializable]
        public sealed class Shortcuts {
            #region Start & e

            private EnvironmentBuilder e;
            public void Start(EnvironmentBuilder e) {
                this.e = e;
            }
            #endregion

            public List<Dictionary<Vector2, GameObject>> moments { get; private set; } = new List<Dictionary<Vector2, GameObject>>();

            public void Undo() {
                moments.RemoveAt(moments.Count - 1);
                var currState = moments[moments.Count - 1];
                // todo: change values
                // e.common.GroundGrid
            }

            private sealed class StatesMoment {
                public Dictionary<Vector2, GameObject> GroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
                public Dictionary<Vector2, GameObject> BackgroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
                public Dictionary<Vector2, GameObject> ForegroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
                public Dictionary<Vector2, GameObject> ObjectsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
                public Dictionary<Vector2, GameObject> SpecialsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            }
        }
    }
}