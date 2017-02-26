using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 *		ICubeEffectAnimator Interface
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public interface ICubeEffectAnimator {
    CubeEffectModes Mode { get; }
    void Update(GridCubeMod effect);
}