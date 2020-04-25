using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(ViewBlurEffectRenderer), PostProcessEvent.AfterStack, "Custom/ViewBlurEffect")]
public sealed class ViewBlurEffect : PostProcessEffectSettings {
    [Range(0f, 1f), Tooltip("ViewBlurEffect effect intensity.")]
    public FloatParameter resolutionFactor = new FloatParameter { value = 0.5f };
    public IntParameter intensity = new IntParameter { value = 1 };
    public Vector2Parameter playerPos = new Vector2Parameter { value = Vector2.zero };
    public Vector2Parameter lookPos = new Vector2Parameter { value = Vector2.zero };
}

public sealed class ViewBlurEffectRenderer : PostProcessEffectRenderer<ViewBlurEffect> {
    public override void Render(PostProcessRenderContext context) {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/ViewBlurEffect"));
        sheet.properties.SetFloat("_ResolutionFactor", settings.resolutionFactor);
        sheet.properties.SetInt("_Intensity", settings.intensity);
        sheet.properties.SetVector("_PlayerPos", settings.playerPos);
        sheet.properties.SetVector("_LookPos", settings.lookPos);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}