using UnityEngine;

namespace G6.Environment.Interactables.Base {
    public abstract class InteractableBase : MonoBehaviour {
        public abstract bool Interact(GameObject whoInterracted);
    }
}