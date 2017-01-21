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

    public float goalColorOffset = 0;

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
		if ( !GameManager.instance.GameStarted ) {
			return;
		}
		ControllerInput();		
	}

	void ControllerInput() {
        Vector3 moveDirection = new Vector3( XCI.GetAxis(XboxAxis.LeftStickX, MappedController), 0, XCI.GetAxis(XboxAxis.LeftStickY, MappedController));
        moveDirection = cam.transform.TransformDirection(moveDirection);

        velocity += new Vector2(moveDirection.x, moveDirection.z) * 2;
        currentPos += velocity * Time.deltaTime;

        CubeGrid.Instance.SetRaiseAmount(currentPos, XCI.GetAxis(XboxAxis.RightTrigger, MappedController), XCI.GetAxis(XboxAxis.RightTrigger, MappedController), goalColorOffset, playerIndex);
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

	public void AddScore(int ammount) { score += ammount; }
	
	public void AddScore() { score++; }
}
