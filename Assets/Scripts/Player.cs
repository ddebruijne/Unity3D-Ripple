using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown("P1_Green") ) {
			Debug.Log("HELLO IM P1_Green");
		}
		if ( Input.GetButtonDown("P1_Red") ) {
			Debug.Log("HELLO IM P1_Red");
		}
		if ( Input.GetButtonDown("P1_Blue") ) {
			Debug.Log("HELLO IM P1_Blue");
		}
		if ( Input.GetButtonDown("P1_Yellow") ) {
			Debug.Log("HELLO IM P1_Yellow");
		}
	}
}
