using G6.RoomSpawning;
using G6.Characters;
using G6.Environment.Interactables.Base;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace G6.EnvironmentBuilder {
    public sealed class RoomCreator : MonoBehaviour {

        #region Instance

        public static RoomCreator instance { get; private set; }

        private void Awake() => instance = this;
        private void OnDestroy() => instance = null;

        #endregion

        #region Variables

        public Transform Room { get; private set; } = default;

        public Transform Background { get; private set; } = default;
        public Transform Walls { get; private set; } = default;
        public Transform Foreground { get; private set; } = default;
        public Transform Doors { get; private set; } = default;
        public Transform Enemies { get; private set; } = default;
        public Transform Objects { get; private set; } = default;
        public Transform Specials { get; private set; } = default;
        public Transform SpawnPoints { get; private set; } = default;

        public const string pathAssets = "Assets/Resources/Prefabs/RoomSpawning/Rooms/Rooms/";
        public const string pathResources = "Prefabs/RoomSpawning/Rooms/Rooms/";

        #endregion

        #region Public

        public void SaveRoom() { // add different folders for different types of rooms
            string roomTypePath = GetRoomPath(Doors);
            if (Room.gameObject.GetComponent<Room>() == null) {
                Room.gameObject.AddComponent<Room>();
            }
            string fileName = BuilderUI.instance.SaveFileName;
            if (fileName == "" || fileName == "Room_tmp") {
                for (int i = 1; i < 100; i++) { // todo: restriction of 98 files
                    if (!System.IO.File.Exists($"{pathAssets}{roomTypePath}Room_{i}.prefab")) {
                        PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}{roomTypePath}Room_{i}.prefab");
                        return;
                    }
                }
            } else {
                if (!System.IO.File.Exists($"{pathAssets}{roomTypePath}{fileName}.prefab")) {
                    PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}{roomTypePath}{fileName}.prefab");
                    return;
                }
            }
        }

        private string GetRoomPath(Transform doors) {
            string path = "";

            System.Func<string, string> switchSmall = new System.Func<string, string>((doorName) => {
                switch (doorName) {
                    case "TopDoor": return "T";
                    case "RightDoor": return "R";
                    case "BottomDoor": return "B";
                    case "LeftDoor": return "L";
                    default: return "";
                }
            });
            System.Func<string, string> switch1 = new System.Func<string, string>((doorName) => { return switchSmall(doorName) + "/"; });
            System.Func<string, string, string> switch2 = new System.Func<string, string, string>((doorName1, doorName2) => {
                string finPath = "";
                foreach (var t in new[] { "T", "R", "B", "L" }) { foreach (var l in new[] { switchSmall(doorName1), switchSmall(doorName2) }) { if (t == l) { finPath += t; } } }
                return finPath + "/";
            });
            System.Func<string, string, string, string> switch3 = new System.Func<string, string, string, string>((doorName1, doorName2, doorName3) => {
                var doorNames = new List<string> { "T", "R", "B", "L" };
                foreach(var l in new[] { switchSmall(doorName1), switchSmall(doorName2), switchSmall(doorName3) }) {
                    doorNames.Remove(l);
                }
                return "Not " + doorNames[0] + "/";
            });

            switch (doors.childCount) {
                case 1:
                    path = "1 exit/" + switch1(doors.GetChild(0).name);
                    break;
                case 2:
                    path = "2 exit/" + switch2(doors.GetChild(0).name, doors.GetChild(1).name);
                    break;
                case 3:
                    path = "3 exit/" + switch3(doors.GetChild(0).name, doors.GetChild(1).name, doors.GetChild(2).name);
                    break;
                case 4:
                    path = "Base/";
                    break;
                default:
                    break;
            }

            return path;
        }

        public void SaveRoomTmp() {
            if (Room.gameObject.GetComponent<Room>() == null) {
                Room.gameObject.AddComponent<Room>();
            }
            PrefabUtility.SaveAsPrefabAsset(Room.gameObject, $"{pathAssets}Room_tmp.prefab");
            // todo
        }

        public void PlaceItem(Vector2 gridPosition, Dictionary<Vector2, GameObject> grid) {

            var selectedData = SelectedData.instance;

            GameObject clone = PrefabUtility.InstantiatePrefab(selectedData.Prefab) as GameObject;

            clone.transform.position = gridPosition;
            if (!CheckAndSetItemProperties(clone.transform)) {
                Destroy(clone);
                return;
            }

            // deleting previous
            DeleteItem(gridPosition);

            grid.Add(gridPosition, clone);
            CheckNeighbours(gridPosition, selectedData.GridSize, selectedData.Grid);
        }
        public void PlaceItem(Vector2 gridPosition) => PlaceItem(gridPosition, SelectedData.instance.Grid);

        public void DeleteItem(Vector2 gridPosition, Dictionary<Vector2, GameObject> grid) {
            if (grid.ContainsKey(gridPosition)) {
                Destroy(grid[gridPosition]);
                grid.Remove(gridPosition);
            }
        }
        public void DeleteItem(Vector2 gridPosition) => DeleteItem(gridPosition, SelectedData.instance.Grid);

        public void ClearLevel() {
            var common = CommonData.instance;
            var grids = new[] { common.BackgroundGrid, common.ForegroundGrid, common.GroundGrid, common.ObjectsGrid, common.SpecialsGrid };
            for(int i = 0; i < grids.Length; i++) {
                while (grids[i].Keys.Count > 0) {
                    DeleteItem(grids[i].Keys.First(), grids[i]);
                }
            }
        }

        #endregion

        #region Private

        private void Start() {
            if (!LoadRoomTmp()) { CreateRoom(); }
        }

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

        private bool LoadRoomTmp() {
            if (!System.IO.File.Exists($"{pathAssets}Room_tmp.prefab")) { return false; }

            var assetObject = PrefabUtility.LoadPrefabContents($"{pathAssets}Room_tmp.prefab");
            var roomObj = new GameObject("Room");

            while (assetObject.transform.childCount > 0) {
                assetObject.transform.GetChild(0).SetParent(roomObj.transform);
            }
            PrefabUtility.UnloadPrefabContents(assetObject);

            roomObj.AddComponent<Room>();

            roomObj.transform.position = Vector2.zero;

            LoadRoomParts(roomObj.transform);

            if (Room.gameObject.GetComponent<Room>() != null) {
                Destroy(Room.gameObject.GetComponent<Room>());
            }

            return true;
            // todo
        }

        public void Test() {
            // Get the Prefab Asset root GameObject and its asset path.
            GameObject assetRoot = Resources.Load<GameObject>($"{pathResources}Room_tmp");
            string assetPath = AssetDatabase.GetAssetPath(assetRoot);

            // Load the contents of the Prefab Asset.
            GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);

            // Modify Prefab contents.
            contentsRoot.AddComponent<BoxCollider>();

            // Save contents back to Prefab Asset and unload contents.
            PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
            PrefabUtility.UnloadPrefabContents(contentsRoot);
        }

        private void LoadRoomParts(Transform room) {
            // Load Room
            Room = room;
            // Load RoomParts
            Background = Room.Find("Background");
            Doors = Room.Find("Doors");
            Enemies = Room.Find("Enemies");
            SpawnPoints = Room.Find("SpawnPoints");
            Walls = Room.Find("Walls");
            Foreground = Room.Find("Foreground");
            Objects = Room.Find("Objects");
            Specials = Room.Find("Specials");

            CommonData.instance.LoadRoomGrids(Walls, Background, Foreground, Objects, new[] { Doors, Enemies, SpawnPoints, Specials });
        }

        private void DeleteRoomTmp() {
            string path = $"{pathAssets}Room_tmp.prefab";
            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
                System.IO.File.Delete($"{path}.meta");
            }
        }

        private void CheckNeighbours(Vector2 gridPosition, float gridSize, Dictionary<Vector2, GameObject> grid) {
            var obj = grid[gridPosition];
            // 3x3 matrix-donut
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (i == 0 && j == 0) { continue; }
                    var localNormNeighbourPos = new Vector2(i, j);
                    var neighbourPos = Service.NormalizeByGrid(gridPosition + localNormNeighbourPos * gridSize, gridSize);

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
                switch (SelectedData.instance.UserLayer) {
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
            } else if (SelectedData.instance.UserLayer == UserLayer.Specials) {
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

        #endregion
    }
}