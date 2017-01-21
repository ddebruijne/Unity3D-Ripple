using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float camShake = 0;

    private Vector3 sPosition;

    void Start() {
        sPosition = transform.localPosition;
    }

	void Update () {
        transform.localPosition = sPosition +
            new Vector3(
                Random.Range(-camShake, camShake),
                Random.Range(-camShake, camShake),
                Random.Range(-camShake, camShake));

        camShake = Mathf.Lerp(camShake, 0, 0.2f);
	}

    public void AddCameraShake(float shake) {
        camShake += shake;
    }
}
