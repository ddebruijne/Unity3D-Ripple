using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		GridCube Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class GridCube : MonoBehaviour {

    public Vector3 defaultPos;
    private Material material;

    public float heightOffset = 0;
    public bool isMoveable = true;

    public Vector3 originalScale;

    public bool updateCube = true;

    private float h = 0;

    private Material Material {
        get {
            if (material == null) material = GetComponent<MeshRenderer>().material;
            return material;
        }
    }

    void Awake() {
        material = GetComponent<MeshRenderer>().material;
        originalScale = transform.localScale;
        defaultPos = GetDefaultPosition();
    }

    public void SetToDefaultPosition() {
        transform.localPosition = GetDefaultPosition();
        transform.localScale = new Vector3(transform.localScale.x, 8, transform.localScale.z);
    }

    public Vector3 GetDefaultPosition() {
        return new Vector3(transform.localPosition.x, -4 + heightOffset, transform.localPosition.z);
    }

	public void UpdateCube (float height, Color color) {
        if (isMoveable) transform.localPosition = defaultPos + new Vector3(0, height * 2, 0);
        if (Material != null) Material.SetColor("_Color", color);

        h = height;
    }
}
