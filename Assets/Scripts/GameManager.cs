using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	int players = 2;                    //Max 4.
	public GameObject PlayerBallPrefab; //These are mostly empty but with scripts attached.
	public GameObject GoalPrefab;       //Standard orientation bottomleft.
    public GameObject BallPrefab;
    public Transform BallSpawnPos;
	public GameObject Level;
	public CameraRotate CameraPivotRotate;
    public List<GameObject> ScoreText;

	[Header("Read Only Objects")]
	[ReadOnly]	public List<GameObject> PlayerObjects;
	[ReadOnly]	public List<Goal> Goals;
    [ReadOnly]	public List<GameObject> Balls = new List<GameObject>();
	[ReadOnly]	public Animator CameraAnimator;
	[ReadOnly]	public bool GameStarted = false;

	[Header("UI Elements")]
	public GameObject HUD;
	public GameObject Splash;

	void Start() {
		instance = this;
		CreatePlayers();
		SetupGoals();
		CameraAnimator = GetComponent<Animator>();

		//Pause it all.
		CameraAnimator.speed = 0 ;
		Splash.GetComponent<Animator>().speed = 0;
		Level.SetActive(false);
		Splash.SetActive(true);
		HUD.SetActive(false);
        
	}

	void Update() {
		if(XCI.GetButtonDown(XboxButton.A, XboxController.First)  && !GameStarted) {
			StartCoroutine(StartLevelSequence());
			GameStarted = true;
		}

        CubeGrid.Instance.SetRaiseAmount(Vector2.zero, 0, 0, Mathf.Sin(Time.time) / 5, 5);
    }

	IEnumerator StartLevelSequence() {
		Splash.GetComponent<Animator>().speed = 1;
		yield return new WaitForSeconds(1);
		Splash.SetActive(false);
		CameraAnimator.speed = 1;
		Level.SetActive(true);
	}

	void CreatePlayers() {
		for ( int i = 0; i < players; i++ ) {
			if ( i > 3 ) break;
			GameObject go = Instantiate(PlayerBallPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<PlayerBall>().SetupPlayer(i);
			go.name = "Player " + i;
			ScoreText[i].SetActive(true);


			PlayerObjects.Add(go);
		}
		UpdateScoreText();  //Call this in the last part of initialization.

	}

	void SetupGoals() {
		for(int i = 0; i < 4; i++ ) {
			GameObject GoalGO = GameObject.Find("Goal" + i);
			GameObject UseGoal = GoalGO.transform.FindChild("UseGoal").gameObject;
			GameObject NotUseGoal = GoalGO.transform.FindChild("NotUseGoal").gameObject;
			GameObject Text3D = GoalGO.transform.FindChild("Player3DText").gameObject;

			UseGoal.SetActive(false);
			NotUseGoal.SetActive(true);
			Text3D.SetActive(false);

			if (i < players) {
				UseGoal.SetActive(true);
				NotUseGoal.SetActive(false);
				Text3D.SetActive(true);

				GoalGO.GetComponentInChildren<Goal>().SetupGoal(PlayerObjects[i].GetComponent<PlayerBall>());
				Goals.Add(GoalGO.GetComponentInChildren<Goal>());
				Text3D.GetComponent<TextMesh>().color = GoalGO.GetComponentInChildren<Goal>().gridColor;
				PlayerObjects[i].GetComponent<PlayerBall>().Player3DText = Text3D;
			}
		}
	}

	public void Ready(int _playerindex) {
		XInputDotNetPure.GamePad.SetVibration(PlayerObjects[_playerindex].GetComponent<PlayerBall>().MappedControllerXinput, 100, 100);
		PlayerObjects[_playerindex].GetComponent<PlayerBall>().playerStatus = PlayerStatus.Ready;
		PlayerObjects[_playerindex].GetComponent<PlayerBall>().Player3DText.GetComponent<TextMesh>().text = "READY";
		StartCoroutine(PlayerReadySequence(_playerindex));
	}

	public IEnumerator PlayerReadySequence(int _playerIndex) {
		yield return new WaitForSeconds(1);
		XInputDotNetPure.GamePad.SetVibration(PlayerObjects[_playerIndex].GetComponent<PlayerBall>().MappedControllerXinput, 0, 0);
	}

	public void GameStartCall() {
		//TODO: foreach player set playerstatus game
		//TODO: foreach player set 3D text inactive.
		StartCoroutine(StartGameSequence());
	}

	IEnumerator StartGameSequence() {
		yield return new WaitForSeconds(2);
		HUD.SetActive(true);
		StartCoroutine(SpawnBallRoutine());

		yield return new WaitForSeconds(1);
		CameraPivotRotate.enabled = true;
	}

	public bool AreTwoReady() {
		return false;
	}


	//when a score event happens on the goal of the playerindex.
	public void Score(int _PlayerIndex) {
		for ( int i = 0; i < PlayerObjects.Count; i++ ) {
            if (i != _PlayerIndex) {
                PlayerObjects[i].GetComponent<PlayerBall>().AddScore();
            }
		}

        foreach (GlassPulse glass in CubeGrid.Instance.glasses) {
            glass.FlashColor(CubeGrid.Instance.playerColors[_PlayerIndex]);
        }

        Camera.main.gameObject.GetComponent<CameraController>().AddCameraShake(0.5f);

        UpdateScoreText();
	}

	public void UpdateScoreText() {
		for ( int i = 0; i < PlayerObjects.Count; i++ ) {
			ScoreText[i].GetComponent<Text>().text = PlayerObjects[i].GetComponent<PlayerBall>().score + "";
		}
	}

    private IEnumerator SpawnBallRoutine() {

        while (true) {
            SpawnBall();
            yield return new WaitForSeconds(2);
        }
    }

    public void SpawnBall() {
        GameObject b = Instantiate(BallPrefab);
        b.transform.position = BallSpawnPos.position;
        b.GetComponent<Rigidbody>().AddForce(new Vector3(
            Random.Range(-100, 100),
            Random.Range(-100, 100),
            Random.Range(-100, 100)), ForceMode.Acceleration);

        Balls.Add(b);
    }
}
