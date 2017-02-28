using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		GlassPulse Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class GlassPulse : MonoBehaviour {

    private Material mat;
    public Color startColor;

    public Color flashColor;
    public float flashColorTime = 0;

    void Awake() {
        mat = GetComponent<MeshRenderer>().material;
    }

    public void Prepare() {
        startColor = GetComponent<MeshRenderer>().sharedMaterial.GetColor("_Color");
    }

    void Update() {
        mat.SetColor("_Color", 
            Color.Lerp(startColor + new Color(0, 0, 0, Mathf.Sin(Time.time) / 6), flashColor, flashColorTime)
            );

        flashColorTime = Mathf.Lerp(flashColorTime, 0, 0.1f);
    }

    public void FlashColor(Color c) {
        flashColor = c;
        flashColorTime = 2;
    }
}
