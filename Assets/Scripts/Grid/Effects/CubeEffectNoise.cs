using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectNoise : CubeEffect {

    private CubeEffectNoiseSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectNoise(CubeEffectNoiseSettings settings) {
        this.settings = settings;
    }

    public override GridCubeMod Update(GridCube cube) {
        float timeScale = Time.time * settings.Speed;
        float noiseValue = Mathf.PerlinNoise((settings.Position.x + cube.transform.position.x) * settings.Scale + timeScale, (settings.Position.y + cube.transform.position.z) * settings.Scale + timeScale);

        settings.CurrentNoiseValue = noiseValue;

        return base.Update(cube);
    }

    public override void ApplyHeight(GridCubeMod mod) {
        mod.Height = settings.Power * settings.CurrentNoiseValue;
    }

    public override void ApplyColor(GridCubeMod mod) {
        mod.Color = settings.Color * Mathf.Abs(settings.Power) * settings.CurrentNoiseValue;
    }

    public override CubeEffectSettings GetSettings() {
        return settings;
    }
}

public class CubeEffectNoiseSettings : CubeEffectSettings {
    private float speed;
    private float scale;
    private float currentNoiseValue = 0;

    public CubeEffectNoiseSettings(CubeEffectModes mode, Color color, float power, float speed, float scale) : base(mode, Vector2.zero, power, color) {
        this.speed = speed;
        this.scale = scale;
    }

    public float Speed {
        get { return speed; }
        set { speed = value; }
    }

    public float Scale {
        get { return scale; }
        set { scale = value; }
    }

    public float CurrentNoiseValue {
        get { return currentNoiseValue; }
        set { currentNoiseValue = value; }
    }
}
