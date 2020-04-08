using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacterBehaviour
{
    #region Fields
    /// <summary>
    /// Detects if character can fly
    /// </summary>
    [SerializeField]
    protected bool canFly;
    /// <summary>
    /// Multiplier for Jump Impulce Vector
    /// </summary>
    [SerializeField]
    protected float JumpForce;
    /// <summary>
    /// Multiplier for SpeedX Impulce Vector
    /// </summary>
    [SerializeField]
    protected float HorizontalSpeed;
    protected Rigidbody2D rb;
    #endregion

    #region Properties
    #region MaxValues
    /// <summary>
    /// Max Value of Hit Points
    /// </summary>
    public float MaxHP { get; protected set; }
    /// <summary>
    /// Max Value of Stamina Points
    /// </summary>
    public float MaxSP { get; protected set; }
    /// <summary>
    /// Max Value of Oxygen Points
    /// </summary>
    public float MaxOP { get; protected set; }
    // ? public float MaxMP { get; protected set; } // Mana Point
    #endregion

    #region CurrentValues
    /// <summary>
    /// Current Value of Hit Points
    /// </summary>
    public float HP { get; protected set; }
    /// <summary>
    /// Current Value of Stamina Points
    /// </summary>
    public float SP { get; protected set; }
    /// <summary>
    /// Current Value of Oxygen Points
    /// </summary>
    public float OP { get; protected set; }
    // ? public float MP { get; protected set; } // Mana Point
    #endregion

    #region Other Public Props
    /// <summary>
    /// Shows current level of character
    /// </summary>
    public short Level { get; protected set; }
    /// <summary>
    /// Shows current fraction of character
    /// </summary>
    public Fractions Fraction { get; protected set; }
    /// <summary>
    /// Shows Bounuses, which has a character
    /// </summary>
    public Bonuses Bonuses { get; protected set; }
    #endregion
    #endregion

    #region Methods
    // TODO
    #endregion

}
