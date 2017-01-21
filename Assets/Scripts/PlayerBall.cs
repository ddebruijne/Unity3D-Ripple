using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerBall : MonoBehaviour {

    [ReadOnly]  public int playerIndex;
	[ReadOnly]	public XboxController MappedController;
	[ReadOnly]	public int score;

    public Camera cam;
    public Vector2 currentPos = Vector2.zero;
    public Vector2 velocity = Vector2.zero;

    void Start() {

    }

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

	void Awake () {
        cam = Camera.main;
	}
	
	void Update () {
		ControllerInput();		
	}

	void ControllerInput() {
        Vector3 moveDirection = new Vector3( XCI.GetAxis(XboxAxis.LeftStickX, MappedController), 0, XCI.GetAxis(XboxAxis.LeftStickY, MappedController));
        moveDirection = cam.transform.TransformDirection(moveDirection);

        velocity += new Vector2(moveDirection.x, moveDirection.z) * 2;
        currentPos += velocity * Time.deltaTime;

        CubeGrid.Instance.SetRaiseAmount(currentPos, XCI.GetButton(XboxButton.A, MappedController) ? 1 : 0.1f, playerIndex);

        velocity /= 1.2f;
	}

	public void AddScore(int ammount) { score += ammount; }
	
	public void AddScore() { score++; }
}
