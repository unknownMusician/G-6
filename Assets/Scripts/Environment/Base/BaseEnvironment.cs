using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnvironment : MonoBehaviour
{

    #region MonoBehaviourMethods
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    #endregion

    public abstract void Interact();
}
