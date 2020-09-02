using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject roomObject;

    [SerializeField]
    private float blockSize = 1;

    [SerializeField]
    private Vector2 roomSize = new Vector2(5, 5);

    private int currentTerrainBlockID = 0;
    private GameObject cursorBlockSprite;

    private Dictionary<Vector2, GameObject> terrainBlocksCoords = new Dictionary<Vector2, GameObject>();

    #region UI

    [SerializeField, Space, Space]
    private List<GameObject> blocks = new List<GameObject>();
    [SerializeField, Space, Space]
    private GameObject blocksMenu = null;
    [SerializeField]
    private GameObject blockButtonPrefab = null;
    [SerializeField]
    private GameObject barrierPrefab = null;
    [SerializeField]
    private GameObject layerMenu = null;

    #endregion

    private Vector3 RoomTopRightCorner => new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0);
    private Vector3 RoomTopLeftCorner => new Vector3(0, roomSize.y * blockSize, 0);
    private Vector3 RoomBottomRightCorner => new Vector3(roomSize.x * blockSize, 0, 0);
    private Vector3 RoomBottomLeftCorner => Vector3.zero;

    private Sprite currentBlockSprite =>
        (currentTerrainBlockID < blocks.Count) ? blocks[currentTerrainBlockID].GetComponent<SpriteRenderer>().sprite : null;
    private Vector2 mouseGridPosition {
        get {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(Mathf.Round(mouse.x / blockSize) * blockSize, Mathf.Round(mouse.y / blockSize) * blockSize);
        }
    }
    private int _cl;
    public int CurrentLayer {
        get => _cl;
        set {
            _cl = value;
            for (int i = 0; i < layerMenu.transform.childCount; i++) {
                layerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            layerMenu.transform.GetChild(_cl).gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    void Start() {
        ShowBuildBarrier();
        FillBlocksMenu();
        CurrentLayer = 1;
        OnBlockMenuSelect(0);
    }

    private void Update() {

        Vector2 currentMouseGridPosition = mouseGridPosition;
        if (cursorBlockSprite != null)
            cursorBlockSprite.transform.position = mouseGridPosition;

        if (Input.GetMouseButton(0)) {

            if (currentTerrainBlockID != 0) {

                // deleting previous
                DeleteRoom(currentMouseGridPosition);

                // creating new
                if ((currentMouseGridPosition.x <= roomSize.x) && (currentMouseGridPosition.y <= roomSize.y)) {

                    terrainBlocksCoords.Add(currentMouseGridPosition,

                    Instantiate(blocks[currentTerrainBlockID],
                    currentMouseGridPosition,
                    Quaternion.identity,
                    roomObject.transform.GetChild(4))
                    );
                }
            }
        }

        if (Input.GetMouseButton(1)) {
            DeleteRoom(currentMouseGridPosition);
        }
    }

    private void OnBlockMenuSelect(int blockID) {

        currentTerrainBlockID = blockID;

        cursorBlockSprite = new GameObject("alphaSprite");
        var sr = cursorBlockSprite.AddComponent<SpriteRenderer>();
        sr.sprite = currentBlockSprite;
        sr.color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
        cursorBlockSprite.transform.position = mouseGridPosition;
    }

    private void DeleteRoom(Vector2 currentGridPosition) {
        if (terrainBlocksCoords.ContainsKey(currentGridPosition)) {
            Destroy(terrainBlocksCoords[currentGridPosition]);
            terrainBlocksCoords.Remove(currentGridPosition);
        }
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

    private void FillBlocksMenu() {
        for (int j = 0; ; j++) {
            for (int i = 0; i <= 3; i++) {
                int currentId = j * 4 + i;
                if (currentId >= blocks.Count)
                    return;
                var btn = Instantiate(blockButtonPrefab, blocksMenu.transform);
                btn.transform.localPosition = new Vector2(-150 + i * 100, 490 - j * 100);
                btn.GetComponent<Image>().sprite = blocks[currentId].GetComponent<SpriteRenderer>().sprite;
                btn.GetComponent<Button>().onClick.AddListener(new UnityAction(() => OnBlockMenuSelect(currentId)));
            }
        }
    }

    private void ShowBuildBarrier() {
        var barrier = Instantiate(barrierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        barrier.transform.position = new Vector2((RoomTopRightCorner.x + RoomBottomLeftCorner.x) / 2, (RoomTopRightCorner.y + RoomBottomLeftCorner.y) / 2);
        barrier.transform.GetChild(1).localScale = new Vector2(RoomTopRightCorner.x - RoomBottomLeftCorner.x, RoomTopRightCorner.y - RoomBottomLeftCorner.y);
    }

    #endregion
}
