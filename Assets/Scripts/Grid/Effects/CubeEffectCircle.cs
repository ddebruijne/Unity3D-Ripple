using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectCircle : CubeEffect {

    private CubeEffectCircleSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectCircle(CubeEffectCircleSettings settings) {
        this.settings = settings;
    }

    public override GridCubeMod Update(GridCube cube) {
        float cubeDist = Vector2.Distance(settings.Position, new Vector2(cube.transform.position.x, cube.transform.position.z));
        float scaler = Mathf.Clamp01((settings.Radius - cubeDist) / settings.Radius);

        settings.CubeScaler = scaler;

        return base.Update(cube);
    }

    public override void ApplyHeight(GridCubeMod mod) {
        mod.Height = settings.Power * Mathf.Pow(settings.CubeScaler, settings.Sharpness) / settings.Sharpness;
    }

    public override void ApplyColor(GridCubeMod mod) {
        mod.Color = settings.Color * (Mathf.Pow(settings.CubeScaler, settings.Sharpness) * settings.Power / settings.Sharpness + (settings.ColorOffset * settings.CubeScaler));
    }

    public override CubeEffectSettings GetSettings() {
        return settings;
    }
}

public class CubeEffectCircleSettings : CubeEffectSettings {
    private float radius;
    private float sharpness;
    private float cubeDistance  = 0;

    public CubeEffectCircleSettings(CubeEffectModes mode, Vector2 position, Color color, float power, float radius, float sharpness)
        : base(mode, position, power, color) {
        this.radius = radius;
        this.sharpness = sharpness;
    }

    public float Radius {
        get { return radius; }
        set { radius = value; }
    }

    public float Sharpness {
        get { return sharpness; }
        set { sharpness = value; }
    }

    public float CubeScaler {
        get { return cubeDistance; }
        set { cubeDistance = value; }
    }
}
