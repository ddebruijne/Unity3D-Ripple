using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCube : MonoBehaviour {

    private Vector3 defaultPos;

    public float raiseAmount = 0;
    private float actualRaiseAmount = 0;

    void Start() {
        defaultPos = new Vector3(transform.position.x, -4, transform.position.z);
    }

    public void SetRaiseAmount(float a) {
        raiseAmount = a;
    }

    public void AddRaiseAmount(float a) {
        raiseAmount += a;
    }

	void Update () {
        actualRaiseAmount = Mathf.Lerp(actualRaiseAmount, raiseAmount, 0.25f);
        transform.position = defaultPos + new Vector3(0, actualRaiseAmount, 0);
	}
}
