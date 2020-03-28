using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour {
    public Material effectMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src, dst, effectMaterial);
    }
}
