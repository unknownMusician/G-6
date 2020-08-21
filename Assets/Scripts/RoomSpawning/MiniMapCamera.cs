using UnityEngine;

public class MiniMapCamera : MonoBehaviour {
    
    #region Properties
    
    private RoomSpawner roomSpawner;
    private GameObject[,] miniMapMatrix;

    #endregion

    #region Methods

    void Start() {

        #region Properties initialization

        roomSpawner = MainData.RoomSpawnerObject.GetComponent<RoomSpawner>();
        miniMapMatrix = roomSpawner.getMiniMapMatrix();

        #endregion

        #region Focusing camera on a base room

        int baseRoomRow = roomSpawner.rows / 2;
        int baseRoomColumn = roomSpawner.columns / 2;

        GameObject baseRoomMiniMapElement = miniMapMatrix[baseRoomRow, baseRoomColumn];
        
        float coordX = baseRoomMiniMapElement.transform.position.x;
        float coordY = baseRoomMiniMapElement.transform.position.y;
        float coordZ = this.transform.position.z;
        
        this.transform.position = new Vector3(coordX, coordY, coordZ);

        #endregion

    }

    #endregion

}
