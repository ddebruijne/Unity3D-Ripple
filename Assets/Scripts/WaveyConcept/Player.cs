using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		Player Class
 *		Handles input from the controller and follows the appropriate tiles on the wave.
 *		Created 20-01-2017 by Danny de Bruijne
 */

public class Player : MonoBehaviour {

	public float NoteActivationRadius = 2;
	public bool debug = false;

	void Start () {
		//Maybe this can be HP?
		SetPlayerColor(Color.black);

		//Set starting position at 1/3rd of the horizontal resolution and 1/2th of the vertical resolution
		Camera cam = GameManager.instance.cam;
		Vector3 Position = cam.ViewportToWorldPoint(new Vector3(0.333f, 0.5f, cam.nearClipPlane));
		Position.z = 0;
		this.transform.position = Position;
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

		if(closest != null ) {
			if(distance < NoteActivationRadius ) {
				//Check the color
				if ( closest.GetComponent<Note>().ThisNotesType.Equals(Color) ) {
					closest.GetComponent<Note>().Activate();
				} else {
					if ( debug ) Debug.Log("Closest note is not of correct color.");
				}
			} else {
				if ( debug ) Debug.Log("Note is not close enough!");
			}
		} else {
			if ( debug ) Debug.LogWarning("No note found.");
		}


	}

	//The wave are individual sprites the player follows. This function places you on the next tile.
	void FollowSprites() {

	}
}
