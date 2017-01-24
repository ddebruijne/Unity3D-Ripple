using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectNoise : ICubeEffect {

    private CubeEffectNoiseSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectNoise(CubeEffectNoiseSettings settings) {
        this.settings = settings;
    }

    public GridCubeMod Update(GridCube cube) {
        foreach(ICubeEffectAnimator animator in animators) {
            animator.Update(this);
        }

        CubeGrid grid = CubeGrid.Instance;
        GridCubeMod mod = new GridCubeMod();

        float timeScale = Time.time * settings.Speed;
        float noiseValue = Mathf.PerlinNoise(settings.Position.x + timeScale, settings.Position.y + timeScale);

        switch (settings.Mode) {
            case CubeEffectModes.ALL:
                ApplyHeight(mod, noiseValue);
                ApplyColor(mod, noiseValue);
                break;

            case CubeEffectModes.COLOR:
                ApplyColor(mod, noiseValue);
                break;

            case CubeEffectModes.HEIGHT:
                ApplyHeight(mod, noiseValue);
                break;
        }

        return mod;
    }

    private void ApplyHeight(GridCubeMod mod, float noiseValue) {
        mod.Height = settings.FinalPower * noiseValue;
    }
    
    private void ApplyColor(GridCubeMod mod, float noiseValue) {
        mod.Color = settings.Color * Mathf.Abs(settings.FinalPower) * noiseValue;
    }

    public void AddAnimator(ICubeEffectAnimator animator) {
        animators.Add(animator);
    }

    public ICubeEffectSettings GetSettings() {
        return settings;
    }
}

public class CubeEffectNoiseSettings : ICubeEffectSettings {

    private CubeEffectModes mode;
    private Vector2 position = Vector2.zero;
    private float power;
    private Color color;
    private float speed;

    public CubeEffectNoiseSettings(CubeEffectModes mode, Color color, float power, float speed) {
        this.mode = mode;
        this.power = power;
        this.color = color;
        this.speed = speed;
    }

    public CubeEffectModes Mode {
        get { return mode; }
        set { mode = value; }
    }

    public Vector2 Position {
        get { return position; }
        set { position = value; }
    }

    public Color Color {
        get { return color; }
        set { color = value; }
    }

    public float FinalPower {
        get { return power; }
        set { power = value; }
    }

    public float Speed {
        get { return speed; }
        set { speed = value; }
    }
}
