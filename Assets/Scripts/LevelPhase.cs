using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		LevelPhase Class
 *		Levels consist of goals, notgoals, 3dtext and spawn points. This class keeps track of them and allows you to turn them on/off
 *		
 *		Todo:
 *		- Include nullchecks
 *		
 *		Created 01-02-2017 by Danny de Bruijne
 */

public enum State {
	Active,
	Inactive
}

public class LevelPhase : MonoBehaviour {

	[Header("Four goals")]
	[ReadOnly] public State[] ActiveGoalState;
	public GameObject[] useGoals;
	public GameObject[] notUseGoals;

	[Header("3D Text")]
	[ReadOnly]public State[] ActiveTextState;
	public GameObject[] text3D;

	[Header("Spawn points")]
	public List<GameObject> ballSpawnPoints;


	void Start() {
		SetAllGoals(State.Active);
	}

	public void SetGoal(State _stateToSet, int _playerID) {
		switch ( _stateToSet ) {
			case State.Active:
				notUseGoals[_playerID].SetActive(false);
				useGoals[_playerID].SetActive(true);
				break;
			case State.Inactive:
				notUseGoals[_playerID].SetActive(true);
				useGoals[_playerID].SetActive(false);
				break;
			default:
				break;

		}
		ActiveGoalState[_playerID] = _stateToSet;
	}

	public void SetAllGoals(State _stateToSet) {
		for(int i = 0; i < NewGameManager.Instance.maxPlayers; i++ ) {
			switch ( _stateToSet ) {
				case State.Active:
					notUseGoals[i].SetActive(false);
					useGoals[i].SetActive(true);
					break;
				case State.Inactive:
					notUseGoals[i].SetActive(true);
					useGoals[i].SetActive(false);
					break;
				default:
					break;

			}
			ActiveGoalState[i] = _stateToSet;
		}
	}

	public void SetText(State _stateToSet, int _playerID) {
		switch ( _stateToSet ) {
			case State.Active:
				text3D[_playerID].SetActive(true);
				break;
			case State.Inactive:
				text3D[_playerID].SetActive(false);
				break;
			default:
				break;

		}
		ActiveTextState[_playerID] = _stateToSet;
	}

	public void SetAllText(State _stateToSet) {
		for(int i = 0; i < NewGameManager.Instance.maxPlayers; i++ ) {
			switch ( _stateToSet ) {
				case State.Active:
					text3D[i].SetActive(true);
					break;
				case State.Inactive:
					text3D[i].SetActive(false);
					break;
				default:
					break;

			}
			ActiveTextState[i] = _stateToSet;
		}
	}
}
