using System.Collections.Generic;
using UnityEngine;

namespace G6.Weapons.Cards {
    public class CardMeleeMemory : Card {

        const string TAG = "CardGunFly: ";

        #region Properties

        public NestedProps Props { get { return new NestedProps(memory); } }
        public override Dictionary<Sprite, string> Modules {
            get {
                var dict = new Dictionary<Sprite, string>();
                // To-Do: localization
                if (Props.Memory != MemoryType.Null)
                    dict.Add(moduleSprites[(int)Props.Memory], "Стиль боя: " + Props.Memory + ".");
                return dict;
            }
        }

        #endregion

        #region Public Variables

        [SerializeField]
        private MemoryType memory = MemoryType.Null;

        //////

        [Space]
        [SerializeField]
        protected List<Sprite> moduleSprites;

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardMeleeMemory (" + memory + ")";
        }

        #endregion

        #region Inner Structures

        public class NestedProps {

            #region Parameters

            public MemoryType Memory { get; }

            #endregion

            #region Constructors

            public NestedProps(MemoryType memory = MemoryType.Null) {
                this.Memory = memory;
            }

            #endregion

            #region Service Methods

            public override string ToString() {
                return "CardGunFly (" + Memory + ")";
            }

            #endregion
        }

        public enum MemoryType {
            Null = -1,
            PirateShipMemory = 0,
            JungleMemory = 1,
            DesertMemory = 2,
            ThisTimeMemory = 3,
            AlienMemory = 4,
        }

        #endregion

        #region Serialization

        [System.Serializable]
        public class Serialization {

            public int memory;

            private Serialization(CardMeleeMemory card) {
                memory = (int)card.memory;
            }

            public static Serialization Real2Serializable(CardMeleeMemory card) { return new Serialization(card); }

            public static void Serializable2Real(Serialization serialization, CardMeleeMemory card) {
                card.memory = (MemoryType)serialization.memory;
            }
        }

        #endregion
    }
}