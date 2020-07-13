using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : EncyclopediaObject {


    #region Properties

    public GameObject Prefab { get; }

    public abstract NestedInfo Info { get; }

    #endregion

    public class NestedInfo {
        GameObject 
    }
}
