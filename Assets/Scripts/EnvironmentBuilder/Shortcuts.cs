using System.Collections.Generic;
using UnityEngine;

namespace G6.EnvironmentBuilder {
    public sealed class Shortcuts : MonoBehaviour {

        public List<Dictionary<Vector2, GameObject>> moments { get; private set; } = new List<Dictionary<Vector2, GameObject>>();

        public void Undo() {
            moments.RemoveAt(moments.Count - 1);
            var currState = moments[moments.Count - 1];
            // todo: change values
            // e.common.GroundGrid
        }

        private sealed class StatesMoment {
            public Dictionary<Vector2, GameObject> GroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> BackgroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ForegroundGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> ObjectsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
            public Dictionary<Vector2, GameObject> SpecialsGrid { get; private set; } = new Dictionary<Vector2, GameObject>();
        }
    }
}