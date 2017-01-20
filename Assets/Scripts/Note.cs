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

	void Start () {
		//TEMP DEFAULT NOTETYPE:	DELETE WHEN LINE SPAWNER IS READY
		SetNoteType(NoteType.Green);
	}
	
	void Update () {
        transform.position += new Vector3(-WaveGenerator.Instance.waveSpeed, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            SetToWave();
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
	}

	//action to take when the correct button is pressed in the correct radius
	public void Activate() {
		Debug.Log("A note activated!!");
	}
}
