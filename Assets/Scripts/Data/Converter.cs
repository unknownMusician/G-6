using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Converter : MonoBehaviour
{
    private static RoomSpawner roomSpawner;
    public static Image GetMinimapImage() {
        GameObject[] rooms = roomSpawner.GetMiniMapMatrix();
        // HUI;
    }
}
