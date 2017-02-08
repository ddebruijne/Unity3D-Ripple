using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

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
	public List<GameObject> levelPhases;
	public bool showSplash;

	[Header("Player")]
	public GameObject playerPrefab;
	public GameObject ballPrefab;

	[Header("Readonly")]
	[ReadOnly] public int maxPlayers = 4;
	[ReadOnly] public List<GameObject> players;
	[ReadOnly] public List<GameObject> balls = new List<GameObject>();
	[ReadOnly] public float time;				//lifetime
	[ReadOnly] public int activeLevelPhase;
	[ReadOnly] public Animator CameraAnimator;
	[ReadOnly] public bool GameStarted = false;

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
		if ( (XCI.GetButtonDown(XboxButton.A, XboxController.First) || Input.GetKeyDown(KeyCode.Space)) && !GameStarted ) {
			SoundManager.Instance.PlaySFX(SFX.MenuConfirm);
			GameStarted = true;
			UIManager.Instance.SplashAnimation();
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
	}


	#region Support Functions
	LevelPhase GetLevelPhase(GameObject _phasego) { return _phasego.GetComponent<LevelPhase>(); }

	public LevelPhase GetLevelPhase(int _phaseID) { return levelPhases[_phaseID].GetComponent<LevelPhase>(); }

	void SetActiveLevelPhase(int _phaseID) {
		for(int i = 0; i < levelPhases.Count; i++ ) {
			if(i == _phaseID ) {
				levelPhases[i].SetActive(true);
				activeLevelPhase = i;
			} else {
				levelPhases[i].SetActive(false);

			}
		}
	}

	void CreatePlayers() {
		//Create ALL 4 players. Later we remove some.
		for (int i = 0; i < maxPlayers; i++ ) {
			GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<NewPlayer>().SetupPlayer(i);
			go.name = "Player " + i;
			UIManager.Instance.SetScoreTextActive(State.Active, i);
			UIManager.Instance.SetScoreTextColor(CubeGrid.Instance.playerColors[i], i);
			players.Add(go);
		}
	}

	void SetPlayerObjects(int _playerID) {
		//turn on/off player hud
		//turn on/off goals
		//turn on/off 3D text
	}
	#endregion


	#region Lobby
	public void Ready(int _playerindex) {
		Debug.Log("P" + _playerindex + " Ready!");
		XInputDotNetPure.GamePad.SetVibration(players[_playerindex].GetComponent<NewPlayer>().MappedControllerXinput, 100, 100);
		players[_playerindex].GetComponent<NewPlayer>().playerStatus = PlayerStatus.Ready;
		GetLevelPhase(activeLevelPhase).SetText(_playerindex, "READY");
		StartCoroutine(PlayerReadySequence(_playerindex));
		ReadyCheck();
	}

	IEnumerator PlayerReadySequence(int _playerIndex) {
		yield return new WaitForSeconds(1);
		XInputDotNetPure.GamePad.SetVibration(players[_playerIndex].GetComponent<NewPlayer>().MappedControllerXinput, 0, 0);
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

	public void GameStart() {

	}
	#endregion
}
