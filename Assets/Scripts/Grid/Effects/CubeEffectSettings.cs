using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectSettings {
    public CubeEffectModes Mode { get; set; }
    public Vector2 Position { get; set; }
    public float Power { get; set; }
    public Color Color { get; set; }
    public float ColorOffset { get; set; }
    public float HeightOffset { get; set; }

    public CubeEffectSettings(CubeEffectModes mode, Vector2 position, float power, Color color) {
        Mode = mode;
        Position = position;
        Power = power;
        Color = color;

        ColorOffset = 0;
        HeightOffset = 0;
    }
}

public enum CubeEffectModes {
    ALL,
    COLOR,
    HEIGHT
}
