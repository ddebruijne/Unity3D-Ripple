using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *		NewPlayer Class
 *		Rework of PlayerBall class so it works with NewGameManager
 *		
 *		Created 08-02-2017 by Danny de Bruijne
 */

public enum PlayerStatus {
	Lobby,
	Ready,
	Game,
	GameOver,
	None
}

public class NewPlayer : MonoBehaviour {

	[ReadOnly]  public int playerIndex;
	[ReadOnly]	public int score;
	[ReadOnly]  public PlayerStatus playerStatus = PlayerStatus.None;
    [ReadOnly]	public Camera cam;
	[ReadOnly]	public Vector2 currentPos = Vector2.zero;
	[ReadOnly]	public Vector2 velocity = Vector2.zero;
	[ReadOnly]	public float goalColorOffset = 0;

	//Controller shake for ready
	float currentshakeammount;
	float shakeammount = 0;   //max 100;
	float shakefalloff = 5;
	float shakeholdmultiplier = 7;

    private CubeEffectCircle playerEffect;

	public void SetupPlayer(int _playerIndex) {
		playerIndex = _playerIndex;

		score = 0;
		playerStatus = PlayerStatus.Lobby;

        CubeEffectCircleSettings effectSettings = new CubeEffectCircleSettings(CubeEffectModes.ALL, Vector2.zero, CubeGrid.Instance.playerColors[playerIndex], 0, 5f, 3f);
        effectSettings.ColorOffset = 0.5f;
        playerEffect = new CubeEffectCircle(effectSettings);
        playerEffect.AddAnimator(new CubeEffectAnimatorPulse(CubeEffectModes.COLOR, 1, 1f, 2f));
    }

	void Awake () {
        cam = Camera.main;
	}

    void Start() {
        CubeGrid.Instance.AddEffect(playerEffect);
    }
	
	//private void OnDestroy() { XInputDotNetPure.GamePad.SetVibration(MappedControllerXinput, 0, 0); }

	void Update () {
		if ( !NewGameManager.Instance.GameStarted ) {
			return;
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
				NewGameManager.Instance.Ready(playerIndex);

			} else {
				shakeammount = 0;
				AddShake();
			}


		} else if (playerStatus == PlayerStatus.Ready ) {
			if(Input.GetButtonDown(playerIndex + "_B") ) {
				//Unready
				playerStatus = PlayerStatus.Lobby;
				NewGameManager.Instance.GetLevelPhase(NewGameManager.Instance.activeLevelPhase).SetDefaultText(playerIndex);
				NewGameManager.Instance.ReadyCheck();
			}
			if ( Input.GetButtonDown(playerIndex + "_A") ) {
				if ( playerIndex == 0 && NewGameManager.Instance.ReadyCheck()) NewGameManager.Instance.GameStart();
			}
		}

        if (Input.GetKeyDown(KeyCode.Space)) {
			NewGameManager.Instance.GameStart();
        }

        Vector3 moveDirection = new Vector3( Input.GetAxisRaw(playerIndex + "_LeftStick_Horizontal"), 0, Input.GetAxisRaw(playerIndex + "_LeftStick_Vertical"));
        moveDirection = cam.transform.TransformDirection(moveDirection);

        velocity += new Vector2(moveDirection.x, moveDirection.z) * 2;
        currentPos += velocity * Time.deltaTime;

        if (currentPos.x < CubeGrid.Instance.boundsX.x) currentPos.x = CubeGrid.Instance.boundsX.x;
        if (currentPos.x > CubeGrid.Instance.boundsX.y) currentPos.x = CubeGrid.Instance.boundsX.y;
        if (currentPos.y < CubeGrid.Instance.boundsY.x) currentPos.y = CubeGrid.Instance.boundsY.x;
        if (currentPos.y > CubeGrid.Instance.boundsY.y) currentPos.y = CubeGrid.Instance.boundsY.y;

        playerEffect.GetSettings().Position = currentPos;
        playerEffect.GetSettings().Power = (Input.GetAxisRaw(playerIndex + "_Trigger")) * 4;

		//Debug.Log(playerIndex + "TRIGGER: " + Input.GetAxisRaw(playerIndex + "_Trigger"));

       if (BallSpawner.Instance.balls != null) {
            foreach (GameObject o in BallSpawner.Instance.balls ) {
                if (o == null) continue;
                Vector3 oPos = o.transform.position;

                if (oPos.y > 2.5f) continue;

                Vector2 oPos2D = new Vector2(oPos.x, oPos.z);
                float distance = Vector2.Distance(oPos2D, currentPos);
                Vector3 direction = (oPos - new Vector3(currentPos.x, oPos.y, currentPos.y)) + new Vector3(velocity.x, 0, velocity.y);
                Vector3 force = direction * (Mathf.Clamp((CubeGrid.Instance.pushRange - distance), 0, float.MaxValue) * 0.75f * Input.GetAxisRaw(playerIndex + "_Trigger"));
                force.y = 0;
                o.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
            }
        }


        velocity /= 1.2f;

		//if ( XCI.GetButtonDown(XboxButton.Back, MappedController) && playerIndex == 0) SceneManager.LoadScene(0);

	}

	void AddShake() {
		//Adding
		shakeammount += (Input.GetAxisRaw(playerIndex + "_Trigger") * shakeholdmultiplier);
		if ( currentshakeammount != shakeammount / 100 ) {
			currentshakeammount = shakeammount / 100;
			//XInputDotNetPure.GamePad.SetVibration(MappedControllerXinput, currentshakeammount, currentshakeammount);

		}
	}

	public void AddScore(int ammount) {
		if ( playerStatus != PlayerStatus.GameOver )
			score += ammount;
	}
	
	public void AddScore() {
		if(playerStatus != PlayerStatus.GameOver)
			score++;
	}
}
