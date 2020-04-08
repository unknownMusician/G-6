using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
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
    public short Level { get; protected set; }
    public Fractions Fraction { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    #endregion

    #region Protected Props
    [SerializeField]
    protected bool canFly;
    [SerializeField]
    protected float JumpForce;
    [SerializeField]
    protected float HorizontalSpeed;
    #endregion

    protected Rigidbody2D rb;
}
