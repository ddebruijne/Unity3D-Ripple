using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	int players = 4;                    //Max 4.
	public GameObject PlayerBallPrefab; //These are mostly empty but with scripts attached.
	public GameObject GoalPrefab;       //Standard orientation bottomleft.
    public GameObject BallPrefab;
    public Transform BallSpawnPos;
	public GameObject Level;

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
        
	}

	void Update() {
		if(XCI.GetButtonDown(XboxButton.A, XboxController.First)  && !GameStarted) {
			StartCoroutine(StartLevelSequence());
			GameStarted = true;

		}
	}

	IEnumerator StartLevelSequence() {
		Splash.GetComponent<Animator>().speed = 1;

		yield return new WaitForSeconds(1);
		Splash.SetActive(false);
		CameraAnimator.speed = 1;
		Level.SetActive(true);

		yield return new WaitForSeconds(2);
		HUD.SetActive(true);
		StartCoroutine(SpawnBallRoutine());
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

			UseGoal.SetActive(false);
			NotUseGoal.SetActive(true);

			if (i < players) {
				UseGoal.SetActive(true);
				NotUseGoal.SetActive(false);
				GoalGO.GetComponentInChildren<Goal>().SetupGoal(PlayerObjects[i].GetComponent<PlayerBall>());
				Goals.Add(GoalGO.GetComponentInChildren<Goal>());

			}
		}
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

        Balls.Add(b);
    }
}
