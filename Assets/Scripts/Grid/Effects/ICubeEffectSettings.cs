using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ICubeEffectSettings {
    CubeEffectModes Mode { get; set; }
    Vector2 Position { get; set; }
    float Power { get; set; }
    float FinalPower { get; set; }
    Color Color { get; set; }

}

public enum CubeEffectModes {
    ALL,
    COLOR,
    HEIGHT
}
