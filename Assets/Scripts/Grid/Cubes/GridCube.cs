using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCube : MonoBehaviour {

    public Vector3 defaultPos;
    private Material material;

    public float heightOffset = 0;
    public bool isMoveable = true;

    public Vector3 originalScale;

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
        transform.position = GetDefaultPosition();
        transform.localScale = new Vector3(transform.localScale.x, 8, transform.localScale.z);
    }

    public Vector3 GetDefaultPosition() {
        return new Vector3(transform.position.x, -4 + heightOffset, transform.position.z);
    }

	public void UpdateCube (float height, Color color) {
        if (isMoveable) transform.position = defaultPos + new Vector3(0, height * 2, 0);
        if (Material != null) Material.SetColor("_Color", color);

        h = height;
    }
}
