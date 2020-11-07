using G6.Enums;
using TMPro;
using UnityEngine;

namespace G6.Characters {
    public class HPShow : MonoBehaviour
    {
        [SerializeField]
        public CharacterBase character;

        private TextMeshPro Text;

        private void Start()
        {
            Text = GetComponent<TextMeshPro>();
        }
        void Update()
        {
            Text.text = character.State == State.Dead ? "Dead" : $"{character.HP}/{character.MaxHP}";
        }
    }
}
