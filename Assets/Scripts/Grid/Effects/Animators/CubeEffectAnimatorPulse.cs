using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeEffectAnimatorPulse : ICubeEffectAnimator {

    private float speed;
    private float height;
    private float offset;

    public CubeEffectAnimatorPulse(float speed, float minMod, float maxMod) {
        this.speed = speed;
        this.height = (maxMod - minMod) / 2;
        this.offset = (maxMod + minMod) / 2;
    }

    public void Update(GridCubeMod mod) {
        float multiplier = (Mathf.Sin(Time.time * speed) * height) + offset;
        mod.Height *= multiplier;
        mod.Color *= multiplier;
    }
}