using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject roomObject;

    [SerializeField]
    private float blockSize;

    [SerializeField]
    private float objectSize;

    [SerializeField]
    private Vector2 roomSize;

    private int currentBlockID = 0;

    private GameObject cursorBlockSprite;

    private Dictionary<Vector2, GameObject> terrainBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2, GameObject> backgroundBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2, GameObject> foregroundBlocksCoordsDict = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2, GameObject> objectsBlocksCoordsDict = new Dictionary<Vector2, GameObject>();

    #region UI

    [SerializeField, Space, Space]
    private List<GameObject> blocksPrefabs = new List<GameObject>();
    [SerializeField, Space, Space]
    private List<GameObject> objectsPrefabs = new List<GameObject>();

    [SerializeField, Space, Space]
    private GameObject blocksMenu = null;
    [SerializeField]
    private GameObject objectsMenu = null;
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

    private Sprite currentPrefabSprite => (CurrentLayer < 3) ?
                ((currentBlockID < blocksPrefabs.Count) ? blocksPrefabs[currentBlockID].GetComponent<SpriteRenderer>().sprite : null) :
                ((currentBlockID < objectsPrefabs.Count) ? objectsPrefabs[currentBlockID].GetComponent<SpriteRenderer>().sprite : null);
    private Vector2 mouseGridPosition {
        get {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(Mathf.Round(mouse.x / blockSize) * blockSize, Mathf.Round(mouse.y / blockSize) * blockSize);
        }
    }

    private int _cl;
    // 0 - background
    // 1 - terrain
    // 2 - forground
    // 3 - objects
    public int CurrentLayer {
        get => _cl;
        set {
            _cl = value;
            // LayerButtons
            for (int i = 0; i < layerMenu.transform.childCount; i++) {
                layerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            layerMenu.transform.GetChild(_cl).gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
            // PrefabMenus
            bool prefab = _cl < 3; // true - blocks, false - objects
            objectsMenu.SetActive(!prefab);
            blocksMenu.SetActive(prefab);
            // changing blockID
            currentBlockID = 0;
        }
    }

    private void Start() {
        Time.timeScale = 0;
        ShowBuildBarrier();
        FillMenu(blocksMenu, blocksPrefabs);
        FillMenu(objectsMenu, objectsPrefabs);
        CurrentLayer = 1;
        OnBlockMenuSelect(0);
    }

    private void Update() {

        Vector2 currentMouseGridPosition = mouseGridPosition;
        if (cursorBlockSprite != null)
            cursorBlockSprite.transform.position = mouseGridPosition;

        if (Input.GetMouseButton(0)) {

            if (currentBlockID != 0) {

                // deleting previous
                DeleteObject(currentMouseGridPosition);

                // creating new
                if ((currentMouseGridPosition.x <= roomSize.x) && (currentMouseGridPosition.y <= roomSize.y) && (currentMouseGridPosition.x >= 0) && (currentMouseGridPosition.y >= 0)) {

                    PlaceObject(currentMouseGridPosition);

                }
            }
        }

        if (Input.GetMouseButton(1)) {
            DeleteObject(currentMouseGridPosition);
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

    private void DeleteObject(Vector2 currentGridPosition) {

        Dictionary<Vector2, GameObject> objectsCoordsDict = new Dictionary<Vector2, GameObject>();

        if (CurrentLayer == 0) {
            objectsCoordsDict = backgroundBlocksCoordsDict;
        } else if (CurrentLayer == 1) {
            objectsCoordsDict = terrainBlocksCoordsDict;

        } else if (CurrentLayer == 2) {
            objectsCoordsDict = foregroundBlocksCoordsDict;
        } else if (CurrentLayer == 3) {
            objectsCoordsDict = objectsBlocksCoordsDict;
        }

        if (objectsCoordsDict.ContainsKey(currentGridPosition)) {
            Destroy(objectsCoordsDict[currentGridPosition]);
            objectsCoordsDict.Remove(currentGridPosition);
        }
    }

    private void PlaceObject(Vector2 currentGridPosition) {

        Dictionary<Vector2, GameObject> objectsCoordsDict = new Dictionary<Vector2, GameObject>();
        Transform parentInRoomGameObject = roomObject.transform;
        List<GameObject> prefabsList = blocksPrefabs;

        if (CurrentLayer == 0) {
            objectsCoordsDict = backgroundBlocksCoordsDict;
            parentInRoomGameObject = roomObject.transform.GetChild(0);
        } else if (CurrentLayer == 1) {
            objectsCoordsDict = terrainBlocksCoordsDict;
            parentInRoomGameObject = roomObject.transform.GetChild(4);
        } else if (CurrentLayer == 2) {
            objectsCoordsDict = foregroundBlocksCoordsDict;
            parentInRoomGameObject = roomObject.transform.GetChild(5);
        } else if (CurrentLayer == 3) {
            objectsCoordsDict = objectsBlocksCoordsDict;
            parentInRoomGameObject = roomObject.transform.GetChild(6);
            prefabsList = objectsPrefabs;
        }

        objectsCoordsDict.Add(currentGridPosition,

        Instantiate(prefabsList[currentBlockID],
            currentGridPosition,
            Quaternion.identity,
            parentInRoomGameObject)
        );

    }

    public void SaveRoomObjectAsAsset() {
        AssetDatabase.CreateAsset(roomObject, "Assets/Prefabs/EnvironmentBuilder/Rooms");
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

}
