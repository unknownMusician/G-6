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
            string roomTypePath = GetRoomPath(SpawnPoints);
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

        private string GetRoomPath(Transform spawnPoints) {
            string path = "";

            System.Func<string, string> switchSmall = new System.Func<string, string>((doorName) => {
                switch (doorName) {
                    case "TopSpawnpoint":
                        return "T";
                    case "RightSpawnpoint":
                        return "R";
                    case "BottomSpawnpoint":
                        return "B";
                    case "LeftSpawnpoint":
                        return "L";
                    default:
                        return "";
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
                foreach (var l in new[] { switchSmall(doorName1), switchSmall(doorName2), switchSmall(doorName3) }) {
                    doorNames.Remove(l);
                }
                return "Not " + doorNames[0] + "/";
            });

            switch (spawnPoints.childCount) {
                case 1:
                    path = "1 exit/" + switch1(spawnPoints.GetChild(0).name);
                    break;
                case 2:
                    path = "2 exit/" + switch2(spawnPoints.GetChild(0).name, spawnPoints.GetChild(1).name);
                    break;
                case 3:
                    path = "3 exit/" + switch3(spawnPoints.GetChild(0).name, spawnPoints.GetChild(1).name, spawnPoints.GetChild(2).name);
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

        public void PlaceItem(Vector2 gridPosition, GameObject prefab, Dictionary<Vector2, GameObject> grid, float gridSize) {

            GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            clone.transform.position = gridPosition;
            if (!CheckAndSetItemProperties(clone.transform)) {
                Destroy(clone);
                return;
            }

            // deleting previous
            DeleteItem(gridPosition);

            grid.Add(gridPosition, clone);
            CheckGridSpritesAround(gridPosition, gridSize, grid);
        }
        public void PlaceItem(Vector2 gridPosition) {
            var selectedData = SelectedData.instance;
            PlaceItem(gridPosition, selectedData.Prefab, selectedData.Grid, selectedData.GridSize);
        }

        public void DeleteItem(Vector2 gridPosition, Dictionary<Vector2, GameObject> grid, float gridSize) {
            if (grid.ContainsKey(gridPosition)) {
                Destroy(grid[gridPosition]);
                grid.Remove(gridPosition);

                CheckGridSpritesAround(gridPosition, gridSize, grid);
            }
        }
        public void DeleteItem(Vector2 gridPosition) {
            var selectedData = SelectedData.instance;
            DeleteItem(gridPosition, selectedData.Grid, selectedData.GridSize);
        }

        public void ClearLevel() {
            var common = CommonData.instance;
            var sizes = new[] { common.BlockSize, common.BlockSize, common.BlockSize, common.ObjectSize, common.BlockSize, }; // todo
            var grids = new[] { common.BackgroundGrid, common.ForegroundGrid, common.GroundGrid, common.ObjectsGrid, common.SpecialsGrid }; // todo
            for (int i = 0; i < grids.Length; i++) {
                while (grids[i].Keys.Count > 0) {
                    DeleteItem(grids[i].Keys.First(), grids[i], sizes[i]);
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

            LoadRoomGrids(Walls, Background, Foreground, Objects, new[] { Doors, Enemies, SpawnPoints, Specials });
        }

        private void DeleteRoomTmp() {
            string path = $"{pathAssets}Room_tmp.prefab";
            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
                System.IO.File.Delete($"{path}.meta");
            }
        }
        private void LoadRoomGrids(Transform groundChild, Transform backgroundChild, Transform foregroundChild, Transform objectsChild, Transform[] specialsChildren) {
            var commonData = CommonData.instance;

            commonData.GroundGrid.Clear();
            for (int i = 0; i < groundChild.childCount; i++) {
                var child = groundChild.GetChild(i);
                Vector2 childNormPos = Service.NormalizeByGrid(child.position, commonData.BlockSize);
                commonData.GroundGrid.Add(childNormPos, child.gameObject);
                CheckGridSpritesAround(childNormPos, commonData.BlockSize, commonData.GroundGrid);
            }
            commonData.BackgroundGrid.Clear();
            for (int i = 0; i < backgroundChild.childCount; i++) {
                var child = backgroundChild.GetChild(i);
                Vector2 childNormPos = Service.NormalizeByGrid(child.position, commonData.BlockSize);
                commonData.BackgroundGrid.Add(childNormPos, child.gameObject);
                CheckGridSpritesAround(childNormPos, commonData.BlockSize, commonData.BackgroundGrid);
            }
            commonData.ForegroundGrid.Clear();
            for (int i = 0; i < foregroundChild.childCount; i++) {
                var child = foregroundChild.GetChild(i);
                Vector2 childNormPos = Service.NormalizeByGrid(child.position, commonData.BlockSize);
                commonData.ForegroundGrid.Add(childNormPos, child.gameObject);
                CheckGridSpritesAround(childNormPos, commonData.BlockSize, commonData.ForegroundGrid);
            }
            commonData.ObjectsGrid.Clear();
            for (int i = 0; i < objectsChild.childCount; i++) {
                var child = objectsChild.GetChild(i);
                Vector2 childNormPos = Service.NormalizeByGrid(child.position, commonData.ObjectSize);
                commonData.ObjectsGrid.Add(childNormPos, child.gameObject);
                CheckGridSpritesAround(childNormPos, commonData.ObjectSize, commonData.ObjectsGrid);
            }
            commonData.SpecialsGrid.Clear();
            for (int j = 0; j < specialsChildren.Length; j++) {
                var specialsChild = specialsChildren[j];
                for (int i = 0; i < specialsChild.childCount; i++) {
                    var child = specialsChild.GetChild(i);
                    Vector2 childNormPos = Service.NormalizeByGrid(child.position, commonData.BlockSize);
                    commonData.SpecialsGrid.Add(childNormPos, child.gameObject);
                    CheckGridSpritesAround(childNormPos, commonData.BlockSize, commonData.SpecialsGrid);
                }
            }
        }

        private void CheckGridSpritesAround(Vector2 gridPosition, float gridSize, Dictionary<Vector2, GameObject> grid) {
            // 3x3 matrix
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    var localNormNeighbourPos = new Vector2(i, j);
                    var neighbourPos = Service.NormalizeByGrid(gridPosition + localNormNeighbourPos * gridSize, gridSize);

                    if (!grid.ContainsKey(neighbourPos)) { continue; }
                    CheckGridSprite(neighbourPos, gridSize, grid);
                }
            }
        }

        private void CheckGridSprite(Vector2 gridPosition, float gridSize, Dictionary<Vector2, GameObject> grid) {
            if(grid[gridPosition].GetComponent<Block>() == null) { return; }
            string localMatrix = "";
            // 3x3 matrix
            for (int i = 1; i > -2; i--) {
                for (int j = -1; j < 2; j++) {
                    var localNormNeighbourPos = new Vector2(j, i);
                    var neighbourPos = Service.NormalizeByGrid(gridPosition + localNormNeighbourPos * gridSize, gridSize);

                    localMatrix += grid.ContainsKey(neighbourPos) ? "1" : "0";
                }
            }
            grid[gridPosition].GetComponent<Block>().CheckSelfSprite(localMatrix);
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