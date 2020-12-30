using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace G6.EnvironmentBuilder {
    public class SelectedData : MonoBehaviour {

        #region Instance

        public static SelectedData instance { get; private set; }

        private void Awake() => instance = this;
        private void OnDestroy() => instance = null;

        #endregion

        private void Start() {
            UserLayer = UserLayer.Ground;
        }

        private int _itemID = 0;
        public int ItemID {
            get => _itemID;
            set {
                _itemID = value;
                Prefab = ItemGroup[ItemID];
                PrefabSprite = Prefab.GetComponent<SpriteRenderer>().sprite;
                OnItemChange?.Invoke();
            }
        }

        public UnityAction OnItemChange { get; set; } = default;
        public UnityAction OnLayerChange { get; set; } = default;

        public Dictionary<Vector2, GameObject> Grid { get; private set; }
        public GameObject[] ItemGroup { get; private set; }
        public GameObject Prefab { get; private set; }
        public Sprite PrefabSprite { get; private set; }

        public float GridSize { get; private set; }

        private UserLayer _userLayer;
        public UserLayer UserLayer {
            get => _userLayer;
            set {
                _userLayer = value;
                var common = CommonData.instance;

                switch (UserLayer) {
                    case UserLayer.Background:
                        Grid = common.BackgroundGrid;
                        ItemGroup = common.BlocksPrefabs;
                        GridSize = common.BlockSize;
                        break;
                    case UserLayer.Foreground:
                        Grid = common.ForegroundGrid;
                        ItemGroup = common.BlocksPrefabs;
                        GridSize = common.BlockSize;
                        break;
                    case UserLayer.Objects:
                        Grid = common.ObjectsGrid;
                        ItemGroup = common.ObjectsPrefabs;
                        GridSize = common.ObjectSize;
                        break;
                    case UserLayer.Specials:
                        Grid = common.SpecialsGrid;
                        ItemGroup = common.SpecialsPrefabs;
                        GridSize = common.BlockSize;
                        break;
                    default: // (case UserLayer.Ground:)
                        Grid = common.GroundGrid;
                        ItemGroup = common.BlocksPrefabs;
                        GridSize = common.BlockSize;
                        break;
                }
                ItemID = 0; // changing blockID

                OnLayerChange?.Invoke();
            }
        }
        public void SetUserLayer(int layerID) => UserLayer = (UserLayer)layerID;
    }
}