using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		Player Class
 *		Handles input from the controller and follows the appropriate tiles on the wave.
 *		Created 20-01-2017 by Danny de Bruijne
 */

public class Player : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		ControllerInput();
	}

	void FixedUpdate() {
		FollowSprites();
	}

	//Checks for input from the player and takes appropriate action.
	void ControllerInput() {
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

	//The wave are individual sprites the player follows. This function places you on the next tile.
	void FollowSprites() {

	}
}
