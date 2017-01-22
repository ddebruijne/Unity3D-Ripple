using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    private Camera cam;

    public float camShake = 0;
    public float sizeGoal = 15;

    private Vector3 sPosition;

    void Awake() {
        Instance = this;
    }

    void Start() {
        cam = GetComponent<Camera>();
        sPosition = transform.localPosition;
    }

	void Update () {
        transform.localPosition = sPosition +
            new Vector3(
                Random.Range(-camShake, camShake),
                Random.Range(-camShake, camShake),
                Random.Range(-camShake, camShake));

        camShake = Mathf.Lerp(camShake, 0, 0.2f);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, sizeGoal + camShake, 0.1f);
    }

    public void AddCameraShake(float shake) {
        camShake += shake;
    }

    public void SetOrtographicSize(float size) {
        sizeGoal = size;
    }
}
