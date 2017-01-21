using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPulse : MonoBehaviour {

    private Material mat;
    private Color startColor;

    void Start() {
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.GetColor("_Tint");
    }

    void Update() {
        mat.SetColor("_Tint", startColor + new Color(0, 0, 0, Mathf.Sin(Time.time) / 6));
    }
}
