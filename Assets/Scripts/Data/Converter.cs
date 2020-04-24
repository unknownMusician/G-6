using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Converter
{
    private static RoomSpawner roomSpawner;
    private GameObject[,] rooms;

    public static Image GetMinimapImage() {
        GameObject[,] rooms = roomSpawner.GetMiniMapMatrix();
        // HUI;
        // images -> 1 image;
        // array of rooms;
        return null;
    }
}
