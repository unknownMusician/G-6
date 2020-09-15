using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTest : InteractableBase
{
    public override bool Interact(GameObject whoInterracted)
    {
        Debug.Log("He intered? oh my!");
        return true;
    }
}
