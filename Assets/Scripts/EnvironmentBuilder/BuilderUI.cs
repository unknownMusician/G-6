using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace G6.EnvironmentBuilder {
    public sealed class BuilderUI : MonoBehaviour {

        #region Instance

        public static BuilderUI instance { get; private set; }

        private void OnDestroy() => instance = null;

        #endregion

        #region Variables

        [SerializeField]
        private GameObject blocksMenu = null;
        [SerializeField] private GameObject objectsMenu = null;
        [SerializeField] private GameObject specialsMenu = null;
        [SerializeField] private GameObject layerMenu = null;
        [SerializeField] private Dropdown sideChooseDropdown = null;
        [SerializeField] private InputField fileNameInput = null;

        private GameObject blockButtonPrefab = default;
        private GameObject barrierPrefab = default;

        #endregion

        #region Public

        public Enums.Side PlayTestSide => (Enums.Side)sideChooseDropdown.value;
        public string SaveFileName => fileNameInput.text;

        #endregion

        #region Private

        private void Awake() {
            // Singleton
            instance = this;
            // Subscribtion
            SelectedData.instance.OnLayerChange += OnLayerChange;
        }

        private void Start() {
            blockButtonPrefab = Resources.Load<GameObject>("Prefabs/EnvironmentBuilder/UI/BlockButton");
            barrierPrefab = Resources.Load<GameObject>("Prefabs/EnvironmentBuilder/UI/Barrier");

            ShowBuildBarrier();
            var common = CommonData.instance;
            FillMenu(blocksMenu, common.BlocksPrefabs);
            FillMenu(objectsMenu, common.ObjectsPrefabs);
            FillMenu(specialsMenu, common.SpecialsPrefabs);
        }

        private void FillMenu(GameObject menu, GameObject[] prefabs) {
            for (int j = 0; ; j++) {
                for (int i = 0; i <= 3; i++) {
                    int currentId = j * 4 + i;
                    if (currentId >= prefabs.Length)
                        return;
                    var btn = Instantiate(blockButtonPrefab, menu.transform);
                    btn.transform.localPosition = new Vector2(-150 + i * 100, 490 - j * 100);
                    btn.GetComponent<Image>().sprite = prefabs[currentId].GetComponent<SpriteRenderer>().sprite;
                    btn.GetComponent<Button>().onClick.AddListener(new UnityAction(() => SelectedData.instance.ItemID = currentId));
                }
            }
        }

        private void ShowBuildBarrier() {
            var common = CommonData.instance;
            var barrier = Instantiate(barrierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            barrier.transform.position = new Vector2(
                (common.RoomTopRightCorner.x + common.RoomBottomLeftCorner.x) / 2,
                (common.RoomTopRightCorner.y + common.RoomBottomLeftCorner.y) / 2);
            barrier.transform.GetChild(1).localScale = new Vector2(
                common.RoomTopRightCorner.x - common.RoomBottomLeftCorner.x + common.BlockSize,
                common.RoomTopRightCorner.y - common.RoomBottomLeftCorner.y + common.BlockSize);
        }

        private void OnLayerChange() {
            var newLayer = SelectedData.instance.UserLayer;
            // LayerButtons
            for (int i = 0; i < layerMenu.transform.childCount; i++) {
                layerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            layerMenu.transform.GetChild((int)newLayer).gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
            // PrefabMenus
            objectsMenu.SetActive(false);
            specialsMenu.SetActive(false);
            blocksMenu.SetActive(false);
            switch (newLayer) {
                case UserLayer.Objects:
                    objectsMenu.SetActive(true);
                    break;
                case UserLayer.Specials:
                    specialsMenu.SetActive(true);
                    break;
                default:
                    blocksMenu.SetActive(true);
                    break;
            }
        }

        #endregion
    }
}
