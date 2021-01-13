using UnityEngine;
using System.Collections.Generic;

namespace G6.EnvironmentBuilder {
    public class CommonData : MonoBehaviour {

        #region Instance

        public static CommonData instance { get; private set; }

        private void Awake() => instance = this;
        private void OnDestroy() => instance = null;

        #endregion

        private void OnEnable() {
            RoomTopRightCorner = new Vector2(RoomSize.x * BlockSize, RoomSize.y * BlockSize);
            RoomTopLeftCorner = new Vector2(0, RoomSize.y * BlockSize);
            RoomBottomRightCorner = new Vector2(RoomSize.x * BlockSize, 0);
            RoomBottomLeftCorner = Vector2.zero;
            LoadAssets();
        }

        private const float blockSize = 1;
        private const float objectSize = 0.5f;
        [SerializeField] private Vector2 roomSize = Vector2.one * 10;

        public float BlockSize => blockSize;
        public float ObjectSize => objectSize;
        public Vector2 RoomSize => roomSize;

        public GameObject[] BlocksPrefabs { get; private set; }
        public GameObject[] ObjectsPrefabs { get; private set; }
        public GameObject[] SpecialsPrefabs { get; private set; }

        public Dictionary<Vector2, GameObject> GroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
        public Dictionary<Vector2, GameObject> BackgroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
        public Dictionary<Vector2, GameObject> ForegroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
        public Dictionary<Vector2, GameObject> ObjectsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
        public Dictionary<Vector2, GameObject> SpecialsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();

        public Vector2 RoomTopRightCorner { get; private set; } = default;
        public Vector2 RoomTopLeftCorner { get; private set; } = default;
        public Vector2 RoomBottomRightCorner { get; private set; } = default;
        public Vector2 RoomBottomLeftCorner { get; private set; } = default;

        private void LoadAssets() {
            string path = "Prefabs/EnvironmentBuilder/Items/";
            var blankBlock = Resources.Load<GameObject>($"{path}BlockBases/000_BlockDefault");

            var blocksPrefabsList = new List<GameObject> { blankBlock };
            blocksPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Blocks"));
            BlocksPrefabs = blocksPrefabsList.ToArray();

            var specialsPrefabsList = new List<GameObject> { blankBlock };
            specialsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Specials"));

            specialsPrefabsList.AddRange(Resources.LoadAll<GameObject>("Prefabs/Characters/Enemy"));

            SpecialsPrefabs = specialsPrefabsList.ToArray();

            path = "Prefabs/Weapons/";

            var objectsPrefabsList = new List<GameObject> { blankBlock };
            objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Cards"));
            objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Guns"));
            objectsPrefabsList.AddRange(Resources.LoadAll<GameObject>($"{path}Melees"));

            ObjectsPrefabs = objectsPrefabsList.ToArray();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(Vector2.up * BlockSize, Vector2.one * BlockSize);
            Gizmos.DrawLine(Vector2.one * BlockSize, Vector2.right * BlockSize);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(RoomTopLeftCorner, RoomTopRightCorner);
            Gizmos.DrawLine(RoomTopRightCorner, RoomBottomRightCorner);
            Gizmos.DrawLine(RoomBottomRightCorner, RoomBottomLeftCorner);
            Gizmos.DrawLine(RoomBottomLeftCorner, RoomTopLeftCorner);
        }
    }
}