using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 *		CubeEffectAll Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class CubeEffectAll : CubeEffect {

    private CubeEffectSettings settings;
    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public CubeEffectAll(CubeEffectSettings settings) {
        this.settings = settings;
    }

    public override GridCubeMod Update(GridCube cube) {
        return base.Update(cube);
    }

    public override void ApplyHeight(GridCubeMod mod) {
        mod.Height = settings.Power;
    }

    public override void ApplyColor(GridCubeMod mod) {
        mod.Color = settings.Color * settings.Power;
    }

    public override CubeEffectSettings GetSettings() {
        return settings;
    }
}
