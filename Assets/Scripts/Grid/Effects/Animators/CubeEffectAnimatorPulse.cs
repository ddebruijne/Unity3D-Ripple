using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 *		CubeEffectAnimatorPulse Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class CubeEffectAnimatorPulse : ICubeEffectAnimator {

    private CubeEffectModes mode;
    private float speed;
    private float height;
    private float offset;

    public CubeEffectAnimatorPulse(CubeEffectModes mode, float speed, float minMod, float maxMod) {
        this.speed = speed;
        this.height = (maxMod - minMod) / 2;
        this.offset = (maxMod + minMod) / 2;
        this.mode = mode;
    }

    public void Update(GridCubeMod mod) {
        float multiplier = (Mathf.Sin(Time.time * speed) * height) + offset;

        switch (mode) {
            case CubeEffectModes.ALL:
                ApplyColor(mod, multiplier);
                ApplyHeight(mod, multiplier);
                break;

            case CubeEffectModes.COLOR:
                ApplyColor(mod, multiplier);
                break;

            case CubeEffectModes.HEIGHT:
                ApplyHeight(mod, multiplier);
                break;
        }
    }

    public CubeEffectModes Mode { get { return mode; } }

    private void ApplyColor(GridCubeMod mod, float multiplier) {
        mod.Color *= multiplier;
    }

    private void ApplyHeight(GridCubeMod mod, float multiplier) {
        mod.Height *= multiplier;
    }
}