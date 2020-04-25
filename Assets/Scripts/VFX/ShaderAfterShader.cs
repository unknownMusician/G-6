using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShaderAfterShader : MonoBehaviour
{
    [SerializeField]
    private RenderTexture rt;
    [SerializeField]
    private List<Material> materials;

    private RenderTexture finalRt;

    private void Start() {
        finalRt = new RenderTexture(rt);
        gameObject.GetComponent<RawImage>().texture = finalRt;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst) {
        RenderTexture rt1 = RenderTexture.GetTemporary(1920, 1080);
        RenderTexture rt2;
        Graphics.Blit(rt, rt1);
        int count = materials.Count;
        for(int i = 0; i < count; i++) {
            rt2 = RenderTexture.GetTemporary(1920, 1080);
            Graphics.Blit(rt1, rt2, materials[i]);
            RenderTexture.ReleaseTemporary(rt1);
            rt1 = rt2;
        }
        Graphics.Blit(rt1, finalRt);
        RenderTexture.ReleaseTemporary(rt1);
    }
}
