using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPulse : MonoBehaviour {

	float amplitude = 0.0025f;
	float period = 1f;
	Vector3 startscale;

	void Start() {
		startscale = transform.localScale;
	}

	void FixedUpdate () {
		float theta = Time.timeSinceLevelLoad / period;
		float distance = amplitude * Mathf.Sin(theta);
		transform.localScale = transform.localScale + new Vector3(1,1,0) * distance;
	}
}
