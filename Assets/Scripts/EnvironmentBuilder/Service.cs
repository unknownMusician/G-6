using UnityEngine;

namespace G6.EnvironmentBuilder {
    public static class Service {

        public static Vector2 NormalizeByGrid(Vector2 worldPos, float gridSize)
            => new Vector2(Mathf.Round(worldPos.x / gridSize) * gridSize, Mathf.Round(worldPos.y / gridSize) * gridSize);
    }
}