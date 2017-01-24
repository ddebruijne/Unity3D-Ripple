using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectCircle : ICubeEffect {

    private CubeEffectCircleSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectCircle(CubeEffectCircleSettings settings) {
        this.settings = settings;
    }

    public GridCubeMod Update(GridCube cube) {
        foreach(ICubeEffectAnimator animator in animators) {
            animator.Update(this);
        }

        CubeGrid grid = CubeGrid.Instance;
        GridCubeMod mod = new GridCubeMod();

        float distance = settings.Radius - Vector2.Distance(settings.Position, new Vector2(cube.transform.position.x, cube.transform.position.z));
        distance = Mathf.Clamp(distance / settings.Radius, 0, float.MaxValue);

        switch (settings.Mode) {
            case CubeEffectModes.ALL:
                ApplyHeight(mod, distance);
                ApplyColor(mod, settings.Radius, distance);
                break;

            case CubeEffectModes.COLOR:
                ApplyColor(mod, settings.Radius, distance);
                break;

            case CubeEffectModes.HEIGHT:
                ApplyHeight(mod, distance);
                break;

        }
        return mod;

    }

    private void ApplyHeight(GridCubeMod mod, float distance) {
        mod.Height = settings.FinalPower * Mathf.Pow(distance, settings.Sharpness);
    }

    private void ApplyColor(GridCubeMod mod, float distance, float radius) {
        mod.Color = settings.Color * (radius / (distance)) * settings.FinalPower;
    }

    public void AddAnimator(ICubeEffectAnimator animator) {
        animators.Add(animator);
    }

    public ICubeEffectSettings GetSettings() {
        return settings;
    }
}

public class CubeEffectCircleSettings : ICubeEffectSettings {

    private CubeEffectModes mode;
    private Vector2 position;
    private float power;
    private float finalPower;
    private float radius;
    private Color color;
    private float sharpness;

    public CubeEffectCircleSettings(CubeEffectModes mode, Vector2 position, Color color, float power, float radius, float sharpness) {
        this.mode = mode;
        this.position = position;
        this.power = power;
        this.radius = radius;
        this.color = color;
        this.sharpness = sharpness;
    }

    public Vector2 Position {
        get { return position; }
        set { position = value; }
    }

    public Color Color {
        get { return color; }
        set { color = value; }
    }

    public float Power {
        get { return power; }
        set { power = value; }
    }

    public float FinalPower {
        get { return finalPower; }
        set { finalPower = value; }
    }

    public float Radius {
        get { return radius; }
        set { radius = value; }
    }

    public float Sharpness {
        get { return sharpness; }
        set { sharpness = value; }
    }

    public CubeEffectModes Mode {
        get { return mode; }
        set { mode = value; }
    }
}
