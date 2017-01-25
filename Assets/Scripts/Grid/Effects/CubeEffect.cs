using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class CubeEffect {

    private List<ICubeEffectAnimator> animators = new List<ICubeEffectAnimator>();

    public virtual GridCubeMod Update(GridCube cube) {
        CubeEffectSettings tempSettings = GetSettings();

        GridCubeMod mod = new GridCubeMod();

        switch (GetSettings().Mode) {
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

        foreach (ICubeEffectAnimator animator in animators) {
            animator.Update(mod);
        }

        return mod;
    }

    public virtual void ApplyHeight(GridCubeMod mod) { }
    public virtual void ApplyColor(GridCubeMod mod) { }

    public virtual CubeEffectSettings GetSettings() { return null; }

    public void AddAnimator(ICubeEffectAnimator animator) {
        animators.Add(animator);
    }
}
