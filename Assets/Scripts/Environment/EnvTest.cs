using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTest : BaseEnvironment
{
    public override void Interact(GameObject whoInterracted)
    {
        Debug.Log("He intered? oh my!");
    }
}
