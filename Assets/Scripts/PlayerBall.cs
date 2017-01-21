using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour {

	[ReadOnly] public int playerIndex;

	public void SetupPlayer(int _playerIndex) {
		playerIndex = _playerIndex;
	}

	void Start () {
		
	}
	
	void Update () {
		ControllerInput();		
	}

	void ControllerInput() {
		Debug.Log("P" + playerIndex + ": " + Input.GetAxis("0_Horizontal"));
	}
}
