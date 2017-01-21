using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerBall : MonoBehaviour {

	[ReadOnly]	public int playerIndex;
	[ReadOnly]	public XboxController MappedController;
	[ReadOnly]	public int score;

	public void SetupPlayer(int _playerIndex) {
		playerIndex = _playerIndex;

		switch ( playerIndex ) {
			case 0:
				MappedController = XboxController.First;
				break;
			case 1:
				MappedController = XboxController.Second;
				break;
			default:
				MappedController = XboxController.All;
				break;
		}

		score = 0;
	}

	void Start () {
		
	}
	
	void Update () {
		ControllerInput();		
	}

	void ControllerInput() {
		//Debug.Log("P" + playerIndex + ": " + XCI.GetAxis(XboxAxis.LeftStickX, MappedController));
		
	}

	public void AddScore(int ammount) { score += ammount; }
	
	public void AddScore() { score++; }
}
