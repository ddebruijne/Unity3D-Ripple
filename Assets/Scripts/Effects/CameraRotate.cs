using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public float camDistance = 10;
    private Camera cam;

	void Start () {
        cam = GetComponentInChildren<Camera>();
	}


	void Update () {
        transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0), Space.World);
    }
}
