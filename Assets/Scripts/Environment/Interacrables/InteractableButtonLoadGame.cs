using G6.Data;
using G6.Environment.Interactables.Base;
using UnityEngine;

namespace G6.Environment.Interactables {
    public class InteractableButtonLoadGame : InteractableBase {
        public override bool Interact(GameObject whoInterracted) {
            LevelManager.LoadLevel(LevelManager.Level.Level1); // todo: read scene from SaveFile and then load it
            return true;
        }
    }
}
