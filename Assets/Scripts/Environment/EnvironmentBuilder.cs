using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using G6.Data;
using System.Collections;

/// todo:
/// - Gamepad input
///     - Placing/Deleting
///     - Read position
///     - UI & Menu
/// - Layers placing object *
/// - Saving multiple rooms
///     - Connect to RoomSpawner
/// - Ability to PlayTest
///     - Choose where to start playtest
/// - Doors restrictions

namespace G6.Environment {
    [ExecuteInEditMode]
    public sealed class EnvironmentBuilder : MonoBehaviour {

        public static EnvironmentBuilder instance { get; private set; }

        [SerializeField] private UI ui;
        [SerializeField] private RoomCreator roomCreator;
        [SerializeField] private Selected selected;
        [SerializeField] private Common common;

        private GameObject cursorBlockSprite { get; set; }
        private Vector2 MouseGridPosition {
            get {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                return new Vector2(Mathf.Round(mouse.x / selected.GridSize) * selected.GridSize, Mathf.Round(mouse.y / selected.GridSize) * selected.GridSize);
            }
        }
        public void SetLayer(UserLayer layer) => selected.UserLayer = layer;
        public bool DoWePlaceBlocks { get; set; } = false;
        public bool DoWeDeleteBlocks { get; set; } = false;

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

        public void Awake() {
            instance = this;
            common.Start(this);
            selected.Start(this);
            ui.Start(this);
            roomCreator.Start(this);
            MainData.EnvironmentBuilderObject = this.gameObject;
        }
        private void Start() {
            Time.timeScale = 0;
            SelectItem(0);
        }
        private void OnDestroy() {
            MainData.EnvironmentBuilderObject = null;
            instance = null;
        }
        private IEnumerator Placing() { // todo: interolate
            while (true) {

                // deleting previous
                DeleteObject(MouseGridPosition);

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
        private IEnumerator Deleting() { // todo: interolate
            while (true) {
                DeleteObject(MouseGridPosition);
                yield return null;
            }
        }
        private void Update() { // todo: change
            cursorBlockSprite.transform.position = MouseGridPosition;
        }

        private void SelectItem(int itemID) {

            selected.ItemID = itemID;

            if (cursorBlockSprite == null) {
                cursorBlockSprite = new GameObject("alphaSprite");
                cursorBlockSprite.AddComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
                cursorBlockSprite.transform.position = MouseGridPosition;
            }
            cursorBlockSprite.GetComponent<SpriteRenderer>().sprite = selected.PrefabSprite;
        }

        private void DeleteObject(Vector2 gridPosition) {

            var selectedDict = selected.ItemsCoordsDict;

            if (selectedDict.ContainsKey(gridPosition)) {
                Destroy(selectedDict[gridPosition]);
                selectedDict.Remove(gridPosition);
            }
        }

        public void SaveRoomObjectAsAsset() => roomCreator.SaveRoom();

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


            private GameObject blockButtonPrefab = default;
            private GameObject barrierPrefab = default;

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
                CreateRoom();
            }
            #endregion
            public Transform Room { get; private set; } = default;

            public Transform Background { get; private set; } = default;
            public Transform Doors { get; private set; } = default;
            public Transform Enemies { get; private set; } = default;
            public Transform SpawnPoints { get; private set; } = default;
            public Transform Walls { get; private set; } = default;
            public Transform Foreground { get; private set; } = default;
            public Transform Objects { get; private set; } = default;
            public Transform Specials { get; private set; } = default;

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

            public void SaveRoom() {
                PrefabUtility.SaveAsPrefabAsset(Room.gameObject, "Assets/Resources/Prefabs/EnvironmentBuilder/Rooms/Room_1.prefab");
            }

            public void PlaceItem(Vector2 gridPosition) {

                GameObject clone = PrefabUtility.InstantiatePrefab(e.selected.Prefab) as GameObject;

                clone.transform.position = gridPosition;
                SetCorrectParent(clone.transform);

                e.selected.ItemsCoordsDict.Add(gridPosition, clone);
            }

            private void SetCorrectParent(Transform child) { // todo

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

            public Dictionary<Vector2, GameObject> GroundBlocksCoordsDict { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> BackgroundBlocksCoordsDict { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ForegroundBlocksCoordsDict { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ObjectsItemsCoordsDict { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> SpecialsItemsCoordsDict { get; private set; } = new Dictionary<Vector2, GameObject>();

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

            public int ItemID { get; set; } = 0;

            public Dictionary<Vector2, GameObject> ItemsCoordsDict { get; private set; }
            public GameObject[] ItemGroup { get; private set; }
            public GameObject Prefab => ItemGroup[ItemID];
            public Sprite PrefabSprite => Prefab.GetComponent<SpriteRenderer>().sprite;

            public float GridSize { get; private set; }

            private UserLayer _userLayer;
            public UserLayer UserLayer {
                get => _userLayer;
                set {
                    e.ui.OnLayerChange(_userLayer = value);
                    var common = e.common;

                    switch (UserLayer) {
                        case UserLayer.Background:
                            ItemsCoordsDict = common.BackgroundBlocksCoordsDict;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        case UserLayer.Foreground:
                            ItemsCoordsDict = common.ForegroundBlocksCoordsDict;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        case UserLayer.Objects:
                            ItemsCoordsDict = common.ObjectsItemsCoordsDict;
                            ItemGroup = common.ObjectsPrefabs;
                            GridSize = common.ObjectSize;
                            break;
                        case UserLayer.Specials:
                            ItemsCoordsDict = common.SpecialsItemsCoordsDict;
                            ItemGroup = common.SpecialsPrefabs;
                            GridSize = common.BlockSize;
                            break;
                        default: // (case UserLayer.Ground:)
                            ItemsCoordsDict = common.GroundBlocksCoordsDict;
                            ItemGroup = common.BlocksPrefabs;
                            GridSize = common.BlockSize;
                            break;
                    }

                    ItemID = 0; // changing blockID
                }
            }

        }
    }
}