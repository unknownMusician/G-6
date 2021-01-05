using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace G6.EnvironmentBuilder {
    public class BuilderInput : MonoBehaviour {

        #region Instance

        public static BuilderInput instance { get; private set; }

        private void OnDestroy() => instance = null;

        #endregion

        #region Place-Delete

        #region Public Access methods

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

        #region Private Coroutines

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

        #endregion

        #region Camera movement

        #region Public Access methods

        public void ZoomInStart() {
            ZoomEnd();
            cameraZoomingCoroutine = StartCoroutine(Zooming(true));
        }

        public void ZoomOutStart() {
            ZoomEnd();
            cameraZoomingCoroutine = StartCoroutine(Zooming(false));
        }

        public void ZoomEnd() {
            if (cameraZoomingCoroutine != null) {
                StopCoroutine(cameraZoomingCoroutine);
                cameraZoomingCoroutine = null;
            }
        }

        public void CameraMoveStart() {
            CameraMoveEnd();
            cameraMovingCoroutine = StartCoroutine(CameraMoving());
        }

        public void CameraMoveEnd() {
            if (cameraMovingCoroutine != null) {
                StopCoroutine(cameraMovingCoroutine);
                cameraMovingCoroutine = null;
            }
        }

        #endregion

        #region private Coroutines...

        private Coroutine cameraZoomingCoroutine;
        private Coroutine cameraMovingCoroutine;

        private IEnumerator Zooming(bool zoomIn) {
            float zoomFactor = 0.15f;
            while (true) {
                vcam.m_Lens.OrthographicSize += zoomFactor * (zoomIn ? -1 : 1);
                if (zoomFactor < 2) { zoomFactor *= 1.015f; }
                yield return null;
            }
        }

        private IEnumerator CameraMoving() {
            float moveFactor = 0.15f;
            while (true) {
                Camera.main.transform.position += (Vector3)Data.MainData.Controls.EnvironmentBuilder.CameraMoveStart.ReadValue<Vector2>() * moveFactor;
                if (moveFactor < 2) { moveFactor *= 1.015f; }
                yield return null;
            }
        }

        #endregion

        #endregion

        #region Private Variables

        [SerializeField] private CinemachineVirtualCamera vcam = null;

        private Vector2 MouseGridPosition => Service.NormalizeByGrid(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), SelectedData.instance.GridSize);

        private GameObject cursor { get; set; }

        private Coroutine coroutine;

        #endregion

        #region Mono

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

        #endregion
    }
}