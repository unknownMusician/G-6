using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using G6.Data;

namespace G6.Environment {
    public class EnvironmentBuilder : MonoBehaviour {

        [SerializeField]
        private GameObject roomObject = null;

        [SerializeField]
        private float blockSize = 1;

        [SerializeField]
        private float objectSize = 0.5f;

        [SerializeField]
        private Vector2 roomSize = Vector2.one * 10;

        private int currentBlockID = 0;

        private List<GameObject> blocksPrefabs;
        private List<GameObject> objectsPrefabs;
        private List<GameObject> specialsPrefabs;

        private GameObject cursorBlockSprite;

        private Dictionary<Vector2, GameObject> terrainBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector2, GameObject> backgroundBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector2, GameObject> foregroundBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector2, GameObject> objectsBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector2, GameObject> specialsBlocksCoordsDict = new Dictionary<Vector2, GameObject>();

        #region UI

        [SerializeField, Space, Space]
        private GameObject blocksMenu = null;
        [SerializeField]
        private GameObject objectsMenu = null;
        [SerializeField]
        private GameObject specialsMenu = null;
        [Space, Space]
        [SerializeField]
        private GameObject blockButtonPrefab = null;
        [SerializeField]
        private GameObject barrierPrefab = null;
        [SerializeField]
        private GameObject layerMenu = null;

        private Vector3 RoomTopRightCorner => new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0);
        private Vector3 RoomTopLeftCorner => new Vector3(0, roomSize.y * blockSize, 0);
        private Vector3 RoomBottomRightCorner => new Vector3(roomSize.x * blockSize, 0, 0);
        private Vector3 RoomBottomLeftCorner => Vector3.zero;


        #endregion

        private Sprite currentPrefabSprite {
            get {
                switch (CurrentLayer) {
                    case Layer.Objects:
                        return objectsPrefabs[currentBlockID].GetComponent<SpriteRenderer>().sprite;
                    case Layer.Special:
                        return specialsPrefabs[currentBlockID].GetComponent<SpriteRenderer>().sprite;
                    default:
                        return blocksPrefabs[currentBlockID].GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
        private Vector2 mouseGridPosition {
            get {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                return new Vector2(Mathf.Round(mouse.x / CurrentGridSize) * CurrentGridSize, Mathf.Round(mouse.y / CurrentGridSize) * CurrentGridSize);
            }
        }
        private float CurrentGridSize => (CurrentLayer == Layer.Objects) ? objectSize : blockSize;

        public void SetLayer(int layer) {
            CurrentLayer = (Layer)layer;
        }
        private Layer _cl;
        public Layer CurrentLayer {
            get => _cl;
            set {
                _cl = value;
                // LayerButtons
                for (int i = 0; i < layerMenu.transform.childCount; i++) {
                    layerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
                }
                layerMenu.transform.GetChild((int)_cl).gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                // PrefabMenus
                objectsMenu.SetActive(false);
                specialsMenu.SetActive(false);
                blocksMenu.SetActive(false);
                switch (_cl) {
                    case Layer.Objects:
                        objectsMenu.SetActive(true);
                        break;
                    case Layer.Special:
                        specialsMenu.SetActive(true);
                        break;
                    default:
                        blocksMenu.SetActive(true);
                        break;
                }
                currentBlockID = 0; // changing blockID
            }
        }

        public bool DoWePlaceBlocks { get; set; } = false;
        public bool DoWeDeleteBlocks { get; set; } = false;

        private void Start() {
            Time.timeScale = 0;
            LoadAssets();
            ShowBuildBarrier();
            FillMenu(blocksMenu, blocksPrefabs);
            FillMenu(objectsMenu, objectsPrefabs);
            FillMenu(specialsMenu, specialsPrefabs);
            CurrentLayer = Layer.Terrain;
            OnBlockMenuSelect(0);
            MainData.EnvironmentBuilderObject = this.gameObject;
        }

        private void Update() {

            if (cursorBlockSprite != null)
                cursorBlockSprite.transform.position = mouseGridPosition;

            if (DoWePlaceBlocks) {

                if (currentBlockID != 0) {

                    // deleting previous
                    DeleteObject();

                    // creating new
                    if ((mouseGridPosition.x <= roomSize.x) && (mouseGridPosition.y <= roomSize.y) && (mouseGridPosition.x >= 0) && (mouseGridPosition.y >= 0)) {

                        PlaceObject();

                    }
                }
            }

            if (DoWeDeleteBlocks) {
                DeleteObject();
            }
        }

        private void OnBlockMenuSelect(int blockID) {

            currentBlockID = blockID;

            if (cursorBlockSprite == null) {
                cursorBlockSprite = new GameObject("alphaSprite");
                cursorBlockSprite.AddComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
                cursorBlockSprite.transform.position = mouseGridPosition;
            }
            cursorBlockSprite.GetComponent<SpriteRenderer>().sprite = currentPrefabSprite;
        }

        private void DeleteObject() {

            Dictionary<Vector2, GameObject> objectsCoordsDict = new Dictionary<Vector2, GameObject>();

            switch (CurrentLayer) {
                case Layer.Background:
                    objectsCoordsDict = backgroundBlocksCoordsDict;
                    break;
                case Layer.Terrain:
                    objectsCoordsDict = terrainBlocksCoordsDict;
                    break;
                case Layer.Forground:
                    objectsCoordsDict = foregroundBlocksCoordsDict;
                    break;
                case Layer.Objects:
                    objectsCoordsDict = objectsBlocksCoordsDict;
                    break;
                case Layer.Special:
                    objectsCoordsDict = specialsBlocksCoordsDict;
                    break;
            }

            if (objectsCoordsDict.ContainsKey(mouseGridPosition)) {
                Destroy(objectsCoordsDict[mouseGridPosition]);
                objectsCoordsDict.Remove(mouseGridPosition);
            }
        }

        private void PlaceObject() {

            Dictionary<Vector2, GameObject> objectsCoordsDict = new Dictionary<Vector2, GameObject>();
            Transform parentInRoomGameObject = roomObject.transform;
            List<GameObject> prefabsList = blocksPrefabs;

            switch (CurrentLayer) {
                case Layer.Background:
                    objectsCoordsDict = backgroundBlocksCoordsDict;
                    parentInRoomGameObject = roomObject.transform.GetChild(0);
                    break;
                case Layer.Terrain:
                    objectsCoordsDict = terrainBlocksCoordsDict;
                    parentInRoomGameObject = roomObject.transform.GetChild(4);
                    break;
                case Layer.Forground:
                    objectsCoordsDict = foregroundBlocksCoordsDict;
                    parentInRoomGameObject = roomObject.transform.GetChild(5);
                    break;
                case Layer.Objects:
                    objectsCoordsDict = objectsBlocksCoordsDict;
                    parentInRoomGameObject = roomObject.transform.GetChild(6);
                    prefabsList = objectsPrefabs;
                    break;
                case Layer.Special:
                    objectsCoordsDict = specialsBlocksCoordsDict;
                    parentInRoomGameObject = roomObject.transform.GetChild(7);
                    prefabsList = specialsPrefabs;
                    break;
            }

            GameObject clone = PrefabUtility.InstantiatePrefab(prefabsList[currentBlockID]) as GameObject;
            clone.transform.position = mouseGridPosition;
            clone.transform.parent = parentInRoomGameObject;

            objectsCoordsDict.Add(mouseGridPosition, clone);

        }

        public void SaveRoomObjectAsAsset() {
            PrefabUtility.SaveAsPrefabAsset(roomObject, "Assets/Prefabs/EnvironmentBuilder/Rooms/Room_1.prefab");
        }

        public void LoadAssets() {
            string path = "Prefabs/EnvironmentBuilder/Items/";
            var blankBlock = Resources.Load<GameObject>($"{path}BlockBases/000_BlockDefault");

            blocksPrefabs = new List<GameObject> { blankBlock };
            blocksPrefabs.AddRange(Resources.LoadAll<GameObject>($"{path}Blocks"));

            specialsPrefabs = new List<GameObject> { blankBlock };
            specialsPrefabs.AddRange(Resources.LoadAll<GameObject>($"{path}Specials"));

            path = "Prefabs/Weapons/";

            objectsPrefabs = new List<GameObject> { blankBlock };
            objectsPrefabs.AddRange(Resources.LoadAll<GameObject>($"{path}Cards"));
            objectsPrefabs.AddRange(Resources.LoadAll<GameObject>($"{path}Guns"));
            objectsPrefabs.AddRange(Resources.LoadAll<GameObject>($"{path}Melees"));
        }

        #region UI

        private void OnDrawGizmos() {

            Gizmos.color = Color.green;

            Gizmos.DrawLine(RoomTopLeftCorner, RoomTopRightCorner);
            Gizmos.DrawLine(RoomTopRightCorner, RoomBottomRightCorner);
            Gizmos.DrawLine(RoomBottomRightCorner, RoomBottomLeftCorner);
            Gizmos.DrawLine(RoomBottomLeftCorner, RoomTopLeftCorner);

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(Vector2.up * blockSize, Vector2.one * blockSize);
            Gizmos.DrawLine(Vector2.one * blockSize, Vector2.right * blockSize);
        }

        private void FillMenu(GameObject menu, List<GameObject> prefabs) {
            for (int j = 0; ; j++) {
                for (int i = 0; i <= 3; i++) {
                    int currentId = j * 4 + i;
                    if (currentId >= prefabs.Count)
                        return;
                    var btn = Instantiate(blockButtonPrefab, menu.transform);
                    btn.transform.localPosition = new Vector2(-150 + i * 100, 490 - j * 100);
                    btn.GetComponent<Image>().sprite = prefabs[currentId].GetComponent<SpriteRenderer>().sprite;
                    btn.GetComponent<Button>().onClick.AddListener(new UnityAction(() => OnBlockMenuSelect(currentId)));
                }
            }
        }

        private void ShowBuildBarrier() {
            var barrier = Instantiate(barrierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            barrier.transform.position = new Vector2((RoomTopRightCorner.x + RoomBottomLeftCorner.x) / 2, (RoomTopRightCorner.y + RoomBottomLeftCorner.y) / 2);
            barrier.transform.GetChild(1).localScale =
                new Vector2(RoomTopRightCorner.x - RoomBottomLeftCorner.x + blockSize, RoomTopRightCorner.y - RoomBottomLeftCorner.y + blockSize);
        }

        #endregion

        public enum Layer {
            Background = 0,
            Terrain = 1,
            Forground = 2,
            Objects = 3,
            Special = 4
        }
    }
}