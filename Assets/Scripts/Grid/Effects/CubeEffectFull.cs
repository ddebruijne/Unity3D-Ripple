using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectAll : ICubeEffect {

    private CubeEffectAllSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectAll(CubeEffectAllSettings settings) {
        this.settings = settings;
    }

    public GridCubeMod Update(GridCube cube) {
        foreach(ICubeEffectAnimator animator in animators) {
            animator.Update(this);
        }

        CubeGrid grid = CubeGrid.Instance;
        GridCubeMod mod = new GridCubeMod();

        switch (settings.Mode) {
            case CubeEffectModes.ALL:
                ApplyHeight(mod);
                ApplyColor(mod);
                break;

            case CubeEffectModes.COLOR:
                ApplyColor(mod);
                break;

            case CubeEffectModes.HEIGHT:
                ApplyHeight(mod);
                break;
        }

        return mod;
    }

    private void ApplyHeight(GridCubeMod mod) {
        mod.Height = settings.FinalPower;
    }
    
    private void ApplyColor(GridCubeMod mod) {
        mod.Color = settings.Color * settings.FinalPower;
    }

    public void AddAnimator(ICubeEffectAnimator animator) {
        animators.Add(animator);
    }

    public ICubeEffectSettings GetSettings() {
        return settings;
    }
}

public class CubeEffectAllSettings : ICubeEffectSettings {

    private CubeEffectModes mode;
    private Vector2 position = Vector2.zero;
    private float power;
    private Color color;

    public CubeEffectAllSettings(CubeEffectModes mode, Color color, float power) {
        this.mode = mode;
        this.power = power;
        this.color = color;
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
}
