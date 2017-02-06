using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *		UIManager Class
 *		Whenever you need to do something with UI, look no further!
 *		
 *		Created 01-02-2017 by Danny de Bruijne
 */

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	[Header("UI Elements")]
	public GameObject Splash;
	public GameObject HUD;
	public List<GameObject> ScoreText;
	public GameObject GameDone;

	// Use this for initialization
	void Start() {
		Instance = this;
	}

	// Update is called once per frame
	void Update() {

	}

	public void UpdateScoreText() {
		for ( int i = 0; i < NewGameManager.Instance.Players.Count; i++ ) {
			ScoreText[i].GetComponentInChildren<Text>().text = NewGameManager.Instance.Players[i].GetComponent<PlayerBall>().score + "";
		}
	}

	public void SetScoreTextColor(Color col, int _playerID) {
		ScoreText[_playerID].GetComponentInChildren<Text>().color = col;
	}

	public void SetScoreTextActive(State _stateToSet, int _playerID) {
		switch ( _stateToSet ) {
			case State.Active:
				ScoreText[_playerID].SetActive(true);
				break;
			case State.Inactive:
				ScoreText[_playerID].SetActive(false);
				break;
			default:
				break;

		}
	}

	public void SetAllScoreTextActive(State _stateToSet) {
		for ( int i = 0; i < NewGameManager.Instance.maxPlayers; i++ ) {
			switch ( _stateToSet ) {
				case State.Active:
					ScoreText[i].SetActive(true);
					break;
				case State.Inactive:
					ScoreText[i].SetActive(false);
					break;
				default:
					break;

			}
		}
	}
}
