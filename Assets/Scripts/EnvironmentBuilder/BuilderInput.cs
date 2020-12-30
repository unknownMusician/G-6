using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace G6.EnvironmentBuilder {
    public class BuilderInput : MonoBehaviour {

        #region Instance

        public static BuilderInput instance { get; private set; }

        private void OnDestroy() => instance = null;

        #endregion

        #region Public

        public void DeleteStart() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            coroutine = StartCoroutine(Deleting());
        }

        public void DeleteStop() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        public void PlaceStart() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            if (SelectedData.instance.ItemID == 0) { return; }
            coroutine = StartCoroutine(Placing());
        }

        public void PlaceStop() {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        #endregion

        #region Private

        private Vector2 MouseGridPosition => Service.NormalizeByGrid(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), SelectedData.instance.GridSize);

        private GameObject cursor { get; set; }

        private Coroutine coroutine;

        private void Awake() {
            instance = this;
            // Creating cursor
            cursor = new GameObject("alphaSprite");
            cursor.AddComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
            // Subscribe cursor
            SelectedData.instance.OnItemChange += () => cursor.GetComponent<SpriteRenderer>().sprite = SelectedData.instance.PrefabSprite;
        }

        private void Update() {
            cursor.transform.position = MouseGridPosition;
        }

        private IEnumerator Placing() { // todo: interpolate
            while (true) {
                var commonData = CommonData.instance;

                // creating new
                if ((MouseGridPosition.x <= commonData.RoomSize.x) &&
                    (MouseGridPosition.y <= commonData.RoomSize.y) &&
                    (MouseGridPosition.x >= 0) &&
                    (MouseGridPosition.y >= 0)) {

                    RoomCreator.instance.PlaceItem(MouseGridPosition);
                }
                yield return null;
            }
        }
        private IEnumerator Deleting() { // todo: interpolate
            while (true) {
                RoomCreator.instance.DeleteItem(MouseGridPosition);
                yield return null;
            }
        }

        #endregion
    }
}