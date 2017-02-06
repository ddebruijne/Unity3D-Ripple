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
	public GameObject PersistentLevel;
	public List<GameObject> LevelPhases;

	[Header("Player")]
	public GameObject PlayerPrefab;
	public GameObject BallPrefab;

	[Header("Readonly")]
	[ReadOnly] public int maxPlayers = 4;
	[ReadOnly] public List<GameObject> Players;
	[ReadOnly] public List<GameObject> Balls = new List<GameObject>();
	[ReadOnly] public float time;				//lifetime
	[ReadOnly] public int activeLevelPhase;


	// Use this for initialization
	void Start () {
		Instance = this;
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}

	void Setup(){
		//Set Phase 1 active 
		//Create 4 Players

	}


	#region Support Functions
	LevelPhase GetLevelPhase(GameObject _phasego) { return _phasego.GetComponent<LevelPhase>(); }

	void SetActiveLevelPhase(int _phaseID) {
		for(int i = 0; i < LevelPhases.Count; i++ ) {
			if(i == _phaseID ) {
				LevelPhases[i].SetActive(true);
				activeLevelPhase = i;
			} else {
				LevelPhases[i].SetActive(false);

			}
		}
	}

	void CreatePlayers() {
		//Create ALL 4 players. Later we remove some.
		for (int i = 0; i < maxPlayers; i++ ) {
			GameObject go = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<PlayerBall>().SetupPlayer(i);
			go.name = "Player " + i;
			UIManager.Instance.SetScoreTextActive(State.Active, i);
			UIManager.Instance.SetScoreTextColor(CubeGrid.Instance.playerColors[i], i);
			Players.Add(go);
		}
	}

	void SetPlayerObjects(int _playerID) {
		//turn on/off player hud
		//turn on/off goals
		//turn on/off 3D text
	}
	#endregion
}
