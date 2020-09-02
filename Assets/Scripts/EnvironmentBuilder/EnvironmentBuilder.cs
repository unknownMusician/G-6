using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private Vector2 blocksOffset = new Vector2(5, 5);
    
    [SerializeField]
    private float blockSize = 2;

    [SerializeField]
    private Vector2 roomSize = new Vector2(5, 5);

    private int currentBlockID = 0;
    private GameObject alphaSprite;

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

    ////////////////////

    private Vector3 RoomTopRightCorner => new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0);
    private Vector3 RoomTopLeftCorner => new Vector3(0, roomSize.y * blockSize, 0); 
    private Vector3 RoomBottomRightCorner => new Vector3(roomSize.x * blockSize, 0, 0); 
    private Vector3 RoomBottomLeftCorner => Vector3.zero;

    private Sprite currentBlockSprite =>
        (currentBlockID > 0 && currentBlockID < blocks.Count) ? blocks[currentBlockID].GetComponent<SpriteRenderer>().sprite : null;
    private Vector2 mouseGridPosition {
        get {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(Mathf.Round(mouse.x / blockSize) * blockSize, Mathf.Round(mouse.y / blockSize) * blockSize);
        }
    }

    void Start() {
        ShowBuildBarrier();
        FillBlocksMenu();
    }

    private void Update() {
        if(currentBlockID > 0)
            alphaSprite.transform.position = mouseGridPosition;

        if (Input.GetMouseButtonDown(0)) {
            // deleting previous

            // creating new


        }
    }

    public void OnBlockMenuSelect(int blockID) {
        currentBlockID = blockID;
        Debug.Log("Click: " + currentBlockID);

        Destroy(alphaSprite);
        if (currentBlockID > 0) {
            alphaSprite = new GameObject("alphaSprite");
            var sr = alphaSprite.AddComponent<SpriteRenderer>();
            sr.sprite = currentBlockSprite;
            sr.color = new Color(1, 1, 1, 0.5f);
            alphaSprite.transform.position = mouseGridPosition;
        } else {
            Destroy(alphaSprite);
            alphaSprite = null;
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
