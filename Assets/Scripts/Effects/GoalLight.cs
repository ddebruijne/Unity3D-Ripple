using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		GoalLight Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class GoalLight : MonoBehaviour {

    private Material mat;

    public Color flashColor;
    public float flashColorTime = 0;

    private int playerID = 0;

    private CubeEffectAll flashEffect;

    void Start() {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update() {
        if (flashEffect == null) return;

        flashEffect.GetSettings().Color = flashColor;
        flashEffect.GetSettings().Power = Mathf.Clamp01(flashColorTime) * 0.75f;
        flashColorTime = Mathf.Lerp(flashColorTime, 0, 0.1f);
    }

    public void FlashColor(Color c, int player) {
        flashColor = c;
        flashColorTime = 2;

        playerID = player;

        if (flashEffect == null) {
            flashEffect = new CubeEffectAll(new CubeEffectSettings(CubeEffectModes.COLOR, Vector2.zero, 0, flashColor));
            CubeGrid.Instance.AddEffect(flashEffect);
        }
    }
}
