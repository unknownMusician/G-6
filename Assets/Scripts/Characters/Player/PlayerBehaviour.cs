using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase
{
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Horizontal"))
            Go(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump")) 
            Jump();

        rb.AddForce(new Vector2(HorizontalSpeed * 2f, 0), ForceMode2D.Impulse);
    }
}