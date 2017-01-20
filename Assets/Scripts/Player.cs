using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		Player Class
 *		Handles input from the controller and follows the appropriate tiles on the wave.
 *		Created 20-01-2017 by Danny de Bruijne
 */

public class Player : MonoBehaviour {

	public float NoteActivationRadius = 10;

	void Start () {
		//Maybe this can be HP?
		SetPlayerColor(Color.black);
	}

	void Update () {
        transform.position = WaveGenerator.Instance.GetPlayerPoint();

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

		GameObject[] NotesInScene = GameObject.FindGameObjectsWithTag("Note");
		GameObject closest = null;
		float distance = Mathf.Infinity;

		//Loop through all the notes in the scene, leaving with the one that's closest by.
		foreach (GameObject go in NotesInScene ) {
			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance ) {
				closest = go;
				distance = curDistance;
			}
		}
		Debug.Log("Note distance: " + distance);
		if(closest != null ) {

			if(distance < NoteActivationRadius ) {
				//Check the color
				if ( closest.GetComponent<Note>().ThisNotesType.Equals(Color) ) {
					closest.GetComponent<Note>().Activate();
				} else {
					Debug.Log("Closest note is not of correct color.");
				}
			} else {
				Debug.Log("Note is not close enough!");
			}
		} else {
			Debug.LogWarning("No note found.");
		}


	}

	//The wave are individual sprites the player follows. This function places you on the next tile.
	void FollowSprites() {

	}
}
