using UnityEngine;

public class MiniMapCamera : MonoBehaviour {

    void Start() {

        #region Parameters initialization
        RoomSpawner roomSpawner = MainData.RoomSpawnerObject.GetComponent<RoomSpawner>();
        GameObject[,] miniMapMatrix = roomSpawner.getMiniMapMatrix();
        int baseRoomRow = roomSpawner.rows / 2;
        int baseRoomColumn = roomSpawner.columns / 2;
        GameObject baseRoomMiniMapElement = miniMapMatrix[baseRoomRow, baseRoomColumn];
        #endregion

        #region Focusing camera on a base room
        float coordX = baseRoomMiniMapElement.transform.position.x;
        float coordY = baseRoomMiniMapElement.transform.position.y;
        float coordZ = this.transform.position.z;
        this.transform.position = new Vector3(coordX, coordY, coordZ);
        #endregion

    }
}
