using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public enum PlayerStatus {
	Lobby,
	Ready,
	Game,
	None
}

public class PlayerBall : MonoBehaviour {

	[ReadOnly]  public int playerIndex;
	[ReadOnly]  public XboxController MappedController;
	[ReadOnly]	public XInputDotNetPure.PlayerIndex MappedControllerXinput;
	[ReadOnly]	public int score;
	[ReadOnly]  public PlayerStatus playerStatus = PlayerStatus.None;
    [ReadOnly]	public Camera cam;
	[ReadOnly]	public Vector2 currentPos = Vector2.zero;
	[ReadOnly]	public Vector2 velocity = Vector2.zero;
	[ReadOnly]	public float goalColorOffset = 0;
	[ReadOnly]	public GameObject Player3DText;

	//Controller shake for ready
	float currentshakeammount;
	float shakeammount = 0;   //max 100;
	float shakefalloff = 5;
	float shakeholdmultiplier = 7;

	public void SetupPlayer(int _playerIndex) {
		playerIndex = _playerIndex;

		switch ( playerIndex ) {
			case 0:
				MappedController = XboxController.First;
				MappedControllerXinput = XInputDotNetPure.PlayerIndex.One;
				break;
			case 1:
				MappedController = XboxController.Second;
				MappedControllerXinput = XInputDotNetPure.PlayerIndex.Two;
				break;
			case 2:
				MappedController = XboxController.Third;
				MappedControllerXinput = XInputDotNetPure.PlayerIndex.Three;
				break;
			case 3:
				MappedController = XboxController.Third;
				MappedControllerXinput = XInputDotNetPure.PlayerIndex.Four;
				break;
			default:
				MappedController = XboxController.All;
				MappedControllerXinput = XInputDotNetPure.PlayerIndex.One;
				break;
		}

		score = 0;
		playerStatus = PlayerStatus.Lobby;
	}

	void Awake () {
        cam = GameManager.instance.cam;
	}
	
	private void OnDestroy() { XInputDotNetPure.GamePad.SetVibration(MappedControllerXinput, 0, 0); }

	void Update () {
		if ( !GameManager.instance.GameStarted ) {
			return;
		}
		if ( playerStatus == PlayerStatus.Lobby ) {

		}
		ControllerInput();		
	}

	void ControllerInput() {
		if(playerStatus == PlayerStatus.Lobby ) {
			//Resetting and confirmation
			if ( shakeammount > 0 && shakeammount < 100 ) {
				shakeammount -= shakefalloff;
				AddShake();

			} else if ( shakeammount >= 100 ) {
				//DONE WE READY
				shakeammount = 0;
				currentshakeammount = 0;
				GameManager.instance.Ready(playerIndex);

			} else {
				shakeammount = 0;
				AddShake();
			}


		} else if (playerStatus == PlayerStatus.Ready ) {
			if(XCI.GetButtonDown(XboxButton.B, MappedController) ) {
				//Unready
				playerStatus = PlayerStatus.Lobby;
				Player3DText.GetComponent<TextMesh>().text = "P" + (playerIndex+1) + " L2";
			}
			if ( XCI.GetButtonDown(XboxButton.A, MappedController) ) {
				if ( playerIndex == 0 /*&& GameManager.instance.AreTwoReady()*/ ) GameManager.instance.GameStartCall();
			}
		}


        Vector3 moveDirection = new Vector3( XCI.GetAxisRaw(XboxAxis.LeftStickX, MappedController), 0, XCI.GetAxisRaw(XboxAxis.LeftStickY, MappedController));
        moveDirection = cam.transform.TransformDirection(moveDirection);

        velocity += new Vector2(moveDirection.x, moveDirection.z) * 2;
        currentPos += velocity * Time.deltaTime;

        if (currentPos.x < CubeGrid.Instance.boundsX.x) currentPos.x = CubeGrid.Instance.boundsX.x;
        if (currentPos.x > CubeGrid.Instance.boundsX.y) currentPos.x = CubeGrid.Instance.boundsX.y;
        if (currentPos.y < CubeGrid.Instance.boundsY.x) currentPos.y = CubeGrid.Instance.boundsY.x;
        if (currentPos.y > CubeGrid.Instance.boundsY.y) currentPos.y = CubeGrid.Instance.boundsY.y;

        CubeGrid.Instance.SetRaiseAmount(currentPos, XCI.GetAxisRaw(XboxAxis.RightTrigger, MappedController), XCI.GetAxisRaw(XboxAxis.RightTrigger, MappedController) * 2f, goalColorOffset, playerIndex);

        if (GameManager.instance.Balls != null) {
            foreach (GameObject o in GameManager.instance.Balls) {
                if (o == null) continue;
                Vector3 oPos = o.transform.position;
                Vector2 oPos2D = new Vector2(oPos.x, oPos.z);
                float distance = Vector2.Distance(oPos2D, currentPos);
                Vector3 direction = (oPos - new Vector3(currentPos.x, oPos.y, currentPos.y));
                Vector3 force = direction * (Mathf.Clamp(((CubeGrid.Instance.raiseRange * 8) - distance), 0, float.MaxValue) * 1.2f * XCI.GetAxis(XboxAxis.RightTrigger, MappedController));
                force.y = 0;
                o.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
            }
        }


        velocity /= 1.2f;
	}

	void AddShake() {
		//Adding
		shakeammount += (XCI.GetAxis(XboxAxis.RightTrigger, MappedController) * shakeholdmultiplier);
		if ( currentshakeammount != shakeammount / 100 ) {
			currentshakeammount = shakeammount / 100;
			XInputDotNetPure.GamePad.SetVibration(MappedControllerXinput, currentshakeammount, currentshakeammount);

		}
	}

	public void AddScore(int ammount) { score += ammount; }
	
	public void AddScore() { score++; }
}
