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
        this.offset = (minMod + maxMod) / 2;
    }

    public void Update(ICubeEffect effect) {
        ICubeEffectSettings settings = effect.GetSettings();
        settings.FinalPower = (Mathf.Sin(Time.time * speed) * height) + offset;
    }
}