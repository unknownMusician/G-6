using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShaderAfterShader : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private RenderTexture rt;
    [SerializeField]
    private List<Material> materials;
    [SerializeField]
    private RenderTexture finalRt;
    [SerializeField]
    private RenderTexture bufferRt;
    [SerializeField]
    private Vector2 offset;

    private void Update() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 mousePos = Input.mousePosition;
        materials[0].SetVector("_PlayerPos", new Vector4(playerPos.x / Camera.main.pixelWidth, playerPos.y / Camera.main.pixelHeight, 0, 0));
        materials[0].SetVector("_LookPos", new Vector4(mousePos.x / Camera.main.pixelWidth, mousePos.y / Camera.main.pixelHeight, 0, 0));
        materials[1].SetVector("_PlayerPos", new Vector4(playerPos.x / Camera.main.pixelWidth, playerPos.y / Camera.main.pixelHeight, 0, 0));
        materials[1].SetVector("_LookPos", new Vector4(mousePos.x / Camera.main.pixelWidth, mousePos.y / Camera.main.pixelHeight, 0, 0));
        Graphics.Blit(rt, bufferRt, materials[0]);
        Graphics.Blit(bufferRt, finalRt, materials[1]);
    }
}
