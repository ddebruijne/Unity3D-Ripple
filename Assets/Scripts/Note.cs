using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		Note Class
 *		To be attached to one of the wave tiles. These are the objects the player has to click.
 *		Created 20-01-2017 by Danny de Bruijne
 */

public enum NoteType {
	Green,
	Red,
	Blue,
	Yellow
}

public class Note : MonoBehaviour {

	public NoteType ThisNotesType;

	void Start () {
		//TEMP DEFAULT NOTETYPE:	DELETE WHEN LINE SPAWNER IS READY
		SetNoteType(NoteType.Green);
	}
	
	void Update () {
		
	}

	void SetNoteType(NoteType type) {
		ThisNotesType = type;
		//Set texture color
	}

	//action to take when the correct button is pressed in the correct radius
	void Activate() {

	}
}
