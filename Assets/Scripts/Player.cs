using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		Player Class
 *		Handles input from the controller and follows the appropriate tiles on the wave.
 *		Created 20-01-2017 by Danny de Bruijne
 */

public class Player : MonoBehaviour {

	public float ButtonActivationRadius = 5;

	void Start () {
		//Maybe this can be HP?
		SetPlayerColor(Color.black);
	}

	void Update () {
		ControllerInput();
	}

	void FixedUpdate() {
		FollowSprites();
	}

	//Shortens the color setting command.
	void SetPlayerColor(Color col) { GetComponent<Renderer>().material.SetColor("_Color", col); }

	//Checks for input from the player and takes appropriate action.
	void ControllerInput() {

		if ( Input.GetButtonDown("P1_Green") ) {
			CheckIfNoteIsNear(NoteType.Green);
		}
		if ( Input.GetButtonDown("P1_Red") ) {
			CheckIfNoteIsNear(NoteType.Red);
		}
		if ( Input.GetButtonDown("P1_Blue") ) {
			CheckIfNoteIsNear(NoteType.Blue);
		}
		if ( Input.GetButtonDown("P1_Yellow") ) {
			CheckIfNoteIsNear(NoteType.Yellow);
		}
	}

	void CheckIfNoteIsNear(NoteType Color) {
		Debug.Log("Checking if note of " + Color.ToString() + " is near.");
		//see if a gameobject of tag is closer than activation radius
		//if the gameobject is close is of the correct color that was pressed.
		//if both are true, call the Activate action on the note.
	}

	//The wave are individual sprites the player follows. This function places you on the next tile.
	void FollowSprites() {

	}
}
