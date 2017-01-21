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

	[ReadOnly] public NoteType ThisNotesType;
	[ReadOnly] public bool activated = false;

	bool move;

	void Start () {
		//TEMP DEFAULT NOTETYPE:	DELETE WHEN LINE SPAWNER IS READY
		SetNoteType(NoteType.Green);

		transform.position = WaveGenerator.Instance.GetCurrentPoint();
	}
	
	void Update () {
		if ( move )  transform.position = new Vector3(transform.position.x - WaveGenerator.Instance.waveSpeed, transform.position.y, 0);
	}

    [ContextMenu("Set to wave")]
    private void SetToWave() {
        transform.position = WaveGenerator.Instance.GetCurrentPoint();
    }

	void SetNoteType(NoteType type) {
		ThisNotesType = type;
		//Set texture color
		switch ( type ) {
			case NoteType.Green:
			GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			break;
			case NoteType.Red:
			GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			break;
			case NoteType.Blue:
			GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
			break;
			case NoteType.Yellow:
			GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
			break;
			default:
			break;
		
		}
		move = true;
	}

	//action to take when the correct button is pressed in the correct radius
	public void Activate() {
		if ( !activated ) {
			Debug.Log("A note activated!!");
			move = false;
			activated = true;
		}
	}
}
