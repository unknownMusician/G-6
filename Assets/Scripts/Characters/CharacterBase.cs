using System.Collections;
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
    public States State { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    public Fractions Fraction { get; protected set; }
    #endregion
    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">Этот показатель отвечает за направление движения персонажа. d > 0 - Идти вправо, d < 0 - Идти влево</param>
    public void Go(float direction)
    {
        rb.AddForce(new Vector2(HorizontalSpeed * direction, 0), ForceMode2D.Impulse);
    }
    public void Jump()
    {
        if(State != States.OnAir)
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }
    #endregion

    #region MonoBehafiour Overrides
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion
}
