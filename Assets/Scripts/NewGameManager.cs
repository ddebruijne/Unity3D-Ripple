using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		GameManager Class
 *		Main gameplay logic class
 *		
 *		Created 01-02-2017 by Danny de Bruijne
 */

public class NewGameManager : MonoBehaviour {

	public static NewGameManager Instance;

	[Header("Level")]
	public GameObject persistentLevel;
	public List<LevelPhase> levelPhases;
	public bool showSplash;

	[Header("Player")]
	[ReadOnly] public int maxPlayers = 4;
	[ReadOnly] public List<GameObject> players;
	public GameObject playerPrefab;

	[Header("Misc")]
	[ReadOnly] public float time;				//lifetime
	[ReadOnly] public int activeLevelPhase;
	[ReadOnly] public Animator CameraAnimator;
	[ReadOnly] public bool GameStarted = false;

    private float levelStartTime = 1000000;

    // Use this for initialization
    void Start () {
		Instance = this;
		time = 0;
		CameraAnimator = GetComponentInChildren<Animator>();

		Setup();
	}
	
	void Update () {
		time += Time.deltaTime;

		//Splash confirm with A
		if ( (Input.GetButtonDown("0_A") || Input.GetKeyDown(KeyCode.Space)) && !GameStarted ) {
			SoundManager.Instance.PlaySFX(SFX.MenuConfirm);
			GameStarted = true;
			UIManager.Instance.SplashAnimation();
		}

        if (BallSpawner.Instance.doneSpawning &&
            ((Time.time - levelStartTime) >= 60 ||
            BallSpawner.Instance.balls.Count == 0)) {
                    BallSpawner.Instance.doneSpawning = false;
            NextSequence();
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            NextSequence();
        }
    }

	void Setup(){
		//Show the splash screen?
		if ( showSplash ) {
			CameraAnimator.speed = 0;
			GameStarted = false;
			UIManager.Instance.Splash.GetComponent<Animator>().speed = 0;
		} else { GameStarted = true; }
		UIManager.Instance.Setup(showSplash);

		//Prepare all players
		int startlevelphase = 0;
		SetActiveLevelPhase(startlevelphase);
		CreatePlayers();
		UIManager.Instance.UpdateScoreText();
		GetLevelPhase(startlevelphase).SetAllGoalsActive(State.Active);
		GetLevelPhase(startlevelphase).SetAllTextActive(State.Active);

        for(int i = 0; i < levelPhases.Count; i++) {
            levelPhases[i].SetActiveState(i == 0);
        }
    }


	#region Support Functions
	LevelPhase GetLevelPhase(GameObject _phasego) { return _phasego.GetComponent<LevelPhase>(); }

	public LevelPhase GetLevelPhase(int _phaseID) { return levelPhases[_phaseID].GetComponent<LevelPhase>(); }

	void SetActiveLevelPhase(int _phaseID) {

        LevelPhase toHide = GetLevelPhase(activeLevelPhase);
        LevelPhase toShow = null;

        for (int i = 0; i < levelPhases.Count; i++) {
            if (i == _phaseID) {
                activeLevelPhase = i;

                toShow = levelPhases[i];
            }
        }

        if (_phaseID == 0) return;
        LevelBuilder.Instance.GoToPhase(toHide, toShow);
	}

	void CreatePlayers() {
		//Create ALL 4 players. Later we remove some.
		for (int i = 0; i < maxPlayers; i++ ) {
			GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<NewPlayer>().SetupPlayer(i);
			go.name = "Player " + i;
			UIManager.Instance.SetScoreTextActive(State.Active, i);
			UIManager.Instance.SetScoreTextColor(CubeGrid.Instance.playerColors[i], i);
			GetLevelPhase(activeLevelPhase).AssignPlayerToGoal(go.GetComponent<NewPlayer>());
			players.Add(go);
		}
	}

	void DisablePlayer(int _playerID) {
		UIManager.Instance.SetScoreTextActive(State.Inactive, _playerID);			//HUD Score
		GetLevelPhase(activeLevelPhase).SetGoalActive(State.Inactive, _playerID);	//Goal
		GetLevelPhase(activeLevelPhase).SetTextActive(State.Inactive, _playerID);	//TextMesh
	}

	void NextSequence() {
        SetActiveLevelPhase(activeLevelPhase + 1);
        levelStartTime = Time.time;
    }

	public void FinishLevel() {
		foreach (GameObject go in players ) {
			go.GetComponent<NewPlayer>().playerStatus = PlayerStatus.GameOver;
		}

        UIManager.Instance.GameDoneAnimation();
	}
	
	public void Score(int _playerID) {
		players[_playerID].GetComponent<NewPlayer>().AddScore();

		foreach ( GlassPulse glass in CubeGrid.Instance.glasses ) {
			glass.FlashColor(CubeGrid.Instance.playerColors[_playerID]);
		}

		Camera.main.gameObject.GetComponent<CameraController>().AddCameraShake(0.5f);
		UIManager.Instance.UpdateScoreText();
	}
	#endregion


	#region Lobby
	public void Ready(int _playerindex) {
		Debug.Log("P" + _playerindex + " Ready!");
		//XInputDotNetPure.GamePad.SetVibration(players[_playerindex].GetComponent<NewPlayer>().MappedControllerXinput, 100, 100);
		players[_playerindex].GetComponent<NewPlayer>().playerStatus = PlayerStatus.Ready;
		GetLevelPhase(activeLevelPhase).SetText(_playerindex, "READY");
		StartCoroutine(PlayerReadySequence(_playerindex));
		ReadyCheck();
	}

	IEnumerator PlayerReadySequence(int _playerIndex) {
		yield return new WaitForSeconds(1);
		//XInputDotNetPure.GamePad.SetVibration(players[_playerIndex].GetComponent<NewPlayer>().MappedControllerXinput, 0, 0);
	}

	public bool ReadyCheck() {
		if ( players[0].GetComponent<NewPlayer>().playerStatus == PlayerStatus.Ready && players[1].GetComponent<NewPlayer>().playerStatus == PlayerStatus.Ready ) {
			GetLevelPhase(activeLevelPhase).SetText(0, "P1 A TO START");
			return true;
		} else {
			if ( players[0].GetComponent<NewPlayer>().playerStatus == PlayerStatus.Ready ) {
				GetLevelPhase(activeLevelPhase).SetText(0, "READY");
			} else if ( players[0].GetComponent<NewPlayer>().playerStatus == PlayerStatus.Lobby ) {
				GetLevelPhase(activeLevelPhase).SetText(0, "P1 R2");
			}
			return false;
		}
	}

	List<GameObject> playersToRemove = new List<GameObject>();
	public void GameStart() {
		for(int i = 0; i < maxPlayers; i++ ) {
			if ( players[i].GetComponent<NewPlayer>().playerStatus != PlayerStatus.Ready ) {
				playersToRemove.Add(players[i]);
				DisablePlayer(i);
			} else {
				players[i].GetComponent<NewPlayer>().playerStatus = PlayerStatus.Game;
			}
		}

		foreach(GameObject go in playersToRemove ) {
			players.Remove(go);
			Destroy(go);
		}

		StartCoroutine(GameStartSequence());
	}

	IEnumerator GameStartSequence() {
		yield return new WaitForSeconds(1);
		UIManager.Instance.ActivateHUD();
		GetLevelPhase(activeLevelPhase).AnimateText();
		BallSpawner.Instance.SpawnBalls();

        yield return new WaitForSeconds(1);
		//Enable camera rotation
	}
	#endregion
}
