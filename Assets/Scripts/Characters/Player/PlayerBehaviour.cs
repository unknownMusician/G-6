using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase
{
    new private void Start()
    {
        base.Start();
    }

    new protected void Update()
    {
        base.Update();

        Side = CheckSideLR(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Debug.Log(State.ToString());

        Control();

    }

    protected void Control()
    {
        if (State == State.OnAir)
        {
            if (State != State.Climb)
            {
                if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump")))
                    MoveX(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));
            }
            else
            {
                if ((Input.GetButton("Vertical") || Input.GetButtonDown("Jump")))
                    MoveY(Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
            }
        }
        else
        {
            if (State != State.Climb)
            {
                //if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump")))
                    MoveX(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));
            }
            else
            {
                //if ((Input.GetButton("Vertical") || Input.GetButtonDown("Jump")))
                    MoveY(Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
            }
        }
    }

}
