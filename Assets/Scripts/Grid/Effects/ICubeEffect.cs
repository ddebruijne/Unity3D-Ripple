using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICubeEffect {

    GridCubeMod Update(GridCube cube);
    ICubeEffectSettings GetSettings();
    void AddAnimator(ICubeEffectAnimator animator);

}
