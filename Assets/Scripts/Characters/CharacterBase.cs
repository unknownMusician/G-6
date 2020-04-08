using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacterBehaviour
{
    #region Fields
    [SerializeField]
    protected bool canFly;
    [SerializeField]
    protected float JumpForce;
    [SerializeField]
    protected float HorizontalSpeed;
    protected Rigidbody2D rb;
    #endregion

    #region Properties
    #region MaxValues
    public float MaxHP { get; protected set; }
    public float MaxSP { get; protected set; }
    public float MaxOP { get; protected set; }
    // ? public float MaxMP { get; protected set; } // Mana Point
    #endregion

    #region CurrentValues
    public float HP { get; protected set; }
    public float SP { get; protected set; }
    public float OP { get; protected set; }
    // ? public float MP { get; protected set; } // Mana Point
    #endregion

    #region Other Public Props
    public short Level { get; protected set; }
    public Fractions Fraction { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    #endregion
    #endregion

    #region Methods
    // TODO
    #endregion

}
