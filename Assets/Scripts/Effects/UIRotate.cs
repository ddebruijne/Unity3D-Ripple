using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour {

	/*
	 *		UIRotate Class
	 *		...
	 *		
	 *		Created 28-01-2017 by Danny de Bruijne
	 */

	float speed = 0.0065f;

	private void FixedUpdate() {
		//transform.localRotation = transform.localRotation.z + new Vector3(0, 0, 1) * speed;
		transform.RotateAroundLocal(new Vector3(0, 0, -1), speed);
	}
}
