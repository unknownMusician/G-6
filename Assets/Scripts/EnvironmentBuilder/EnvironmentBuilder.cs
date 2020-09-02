using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject roomObject;

    [SerializeField]
    private float terrainBlockSize = 2;

    [SerializeField]
    private Vector2 roomSize = new Vector2(5, 5);

    private int currentTerrainBlockID = 0;
    private GameObject cursorBlockSprite;

    private Dictionary<Vector2, GameObject> terrainBlocksCoords = new Dictionary<Vector2, GameObject>();

    #region UI

    [SerializeField, Space, Space]
    private List<GameObject> blocks = new List<GameObject>();
    [SerializeField, Space, Space]
    private GameObject blocksMenuCanvas = null;
    [SerializeField]
    private GameObject blockButtonPrefab = null;
    [SerializeField]
    private GameObject barrierPrefab = null;

    #endregion

    private Vector3 RoomTopRightCorner => new Vector3(roomSize.x * terrainBlockSize, roomSize.y * terrainBlockSize, 0);
    private Vector3 RoomTopLeftCorner => new Vector3(0, roomSize.y * terrainBlockSize, 0);
    private Vector3 RoomBottomRightCorner => new Vector3(roomSize.x * terrainBlockSize, 0, 0);
    private Vector3 RoomBottomLeftCorner => Vector3.zero;

    private Sprite currentBlockSprite =>
        (currentTerrainBlockID < blocks.Count) ? blocks[currentTerrainBlockID].GetComponent<SpriteRenderer>().sprite : null;
    private Vector2 mouseGridPosition {
        get {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(Mathf.Round(mouse.x / terrainBlockSize) * terrainBlockSize, Mathf.Round(mouse.y / terrainBlockSize) * terrainBlockSize);
        }
    }

    void Start() {
        ShowBuildBarrier();
        FillBlocksMenu();
        OnBlockMenuSelect(0);
    }

    private void Update() {

        Vector2 currentMouseGridPosition = mouseGridPosition;

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
        Debug.Log("Click: " + currentTerrainBlockID);

        cursorBlockSprite = new GameObject("alphaSprite");
        var sr = cursorBlockSprite.AddComponent<SpriteRenderer>();
        sr.sprite = currentBlockSprite;
        sr.color = new Color(1, 1, 1, 0.5f);
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
        Gizmos.DrawLine(Vector2.up * terrainBlockSize, Vector2.one * terrainBlockSize);
        Gizmos.DrawLine(Vector2.one * terrainBlockSize, Vector2.right * terrainBlockSize);
    }

    private void FillBlocksMenu() {
        for (int j = 0; ; j++) {
            for (int i = 0; i <= 3; i++) {
                int currentId = j * 4 + i;
                if (currentId >= blocks.Count)
                    return;
                var btn = Instantiate(blockButtonPrefab, blocksMenuCanvas.transform);
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
