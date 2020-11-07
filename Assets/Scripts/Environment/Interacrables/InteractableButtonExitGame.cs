using UnityEngine;
using G6.Data.SaveLoad;
using G6.Environment.Interactables.Base;
using G6.Data;

namespace G6.Environment.Interactables {
    public class InteractableButtonExitGame : InteractableBase {
        public override bool Interact(GameObject whoInterracted) {
            SaveAndLoadGame.SaveGame();
            LevelManager.LoadMenu();
            return true;
        }
    }
}