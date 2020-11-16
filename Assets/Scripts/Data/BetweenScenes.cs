using G6.Characters.Player;
using G6.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace G6.Data {
    public static class BetweenScenes {

        public static class Player {
            public static PlayerBehaviour.Serialization PlayerData { get; set; } = null;
        }
        public static class UI {
            // todo
        }
        public static class EnvironmentBuilder {
            public static Side WhereToStart { get; set; } = default;
        }
    }
}