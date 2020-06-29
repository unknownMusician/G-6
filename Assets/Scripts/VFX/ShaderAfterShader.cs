using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShaderAfterShader : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;
    [SerializeField]
    private RenderTexture rt = null;
    [SerializeField]
    private List<Material> materials = null;
    [SerializeField]
    private RenderTexture finalRt = null;
    [SerializeField]
    private RenderTexture bufferRt = null;
    [SerializeField]
    private Vector2 centerOffset = Vector2.zero;
    [SerializeField]
    private Vector2 centerOffsetWichRotates = Vector2.zero;
    [SerializeField]
    private float viewWidth = 1.0f;
    [SerializeField]
    private bool showInColor = false;

    private void Update() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 mousePos = Input.mousePosition;
        Vector4 plPos = new Vector4(playerPos.x / Camera.main.pixelWidth, playerPos.y / Camera.main.pixelHeight, 0, 0);
        Vector4 lkPos = new Vector4(mousePos.x / Camera.main.pixelWidth, mousePos.y / Camera.main.pixelHeight, 0, 0);
        Vector4 diff4 = lkPos - plPos;
        Vector2 diff = new Vector2(diff4.x, diff4.y);
        float angle = (float)Mathf.Atan2(diff.y, diff.x);

        Vector4 centerOffset4 = new Vector4(centerOffset.x, centerOffset.y);
        Vector4 centerOffsetWichRotates4 = new Vector4(centerOffsetWichRotates.x, centerOffsetWichRotates.y);

        Vector4 resolution = new Vector4(Camera.main.pixelWidth, Camera.main.pixelHeight);

        //Debug.Log(
        //    "pl3: (" + Mathf.Floor(playerPos.x) + " ; " + Mathf.Floor(playerPos.y) + "); " + 
        //    "m3 : (" + Mathf.Floor(mousePos.x) + " ; " + Mathf.Floor(mousePos.y) + "); " +
        //    "pl4: (" + plPos.x + " ; " + plPos.y + "); " +
        //    "lk4: (" + lkPos.x + " ; " + lkPos.y + "); " +
        //    "ang: (" + angle + "); "
        //    );

        materials[0].SetVector("_PlayerPos", plPos);
        materials[0].SetVector("_LookPos", lkPos);
        materials[0].SetFloat("_LookAngle", angle);
        materials[0].SetVector("_Resolution", resolution);
        materials[0].SetFloat("_ViewWidth", viewWidth);
        materials[0].SetVector("_Offset", centerOffset4);
        materials[0].SetVector("_OffsetRot", centerOffsetWichRotates4);
        materials[0].SetFloat("_ShowInColor", showInColor ? 1.0f : 0.0f);

        materials[1].SetVector("_PlayerPos", plPos);
        materials[1].SetVector("_LookPos", lkPos);
        materials[1].SetFloat("_LookAngle", angle);
        materials[1].SetVector("_Resolution", resolution);
        materials[1].SetFloat("_ViewWidth", viewWidth);
        materials[1].SetVector("_Offset", centerOffset4);
        materials[1].SetVector("_OffsetRot", centerOffsetWichRotates4);
        materials[0].SetFloat("_ShowInColor", showInColor ? 1.0f : 0.0f);

        Graphics.Blit(rt, bufferRt, materials[0]);
        Graphics.Blit(bufferRt, finalRt, materials[1]);
    }
}
