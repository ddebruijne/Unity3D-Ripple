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

public enum GoalState {
	Active,
	Inactive
}

public enum TextState {
	Active,
	Inactive
}

public class LevelPhase : MonoBehaviour {

	[Header("Four goals")]
	[ReadOnly] public GoalState[] ActiveGoalState;
	public GameObject[] useGoals;
	public GameObject[] notUseGoals;

	[Header("3D Text")]
	[ReadOnly]public TextState[] ActiveTextState;
	public GameObject[] text3D;

	[Header("Spawn points")]
	public List<GameObject> ballSpawnPoints;


	void Start() {
		SetAllGoals(GoalState.Active);
	}

	public void SetGoal(GoalState _stateToSet, int _playerID) {
		switch ( _stateToSet ) {
			case GoalState.Active:
				notUseGoals[_playerID].SetActive(false);
				useGoals[_playerID].SetActive(true);
				break;
			case GoalState.Inactive:
				notUseGoals[_playerID].SetActive(true);
				useGoals[_playerID].SetActive(false);
				break;
			default:
				break;

		}
		ActiveGoalState[_playerID] = _stateToSet;
	}

	public void SetAllGoals(GoalState _stateToSet) {
		for(int i = 0; i < 4; i++ ) {
			switch ( _stateToSet ) {
				case GoalState.Active:
					notUseGoals[i].SetActive(false);
					useGoals[i].SetActive(true);
					break;
				case GoalState.Inactive:
					notUseGoals[i].SetActive(true);
					useGoals[i].SetActive(false);
					break;
				default:
					break;

			}
			ActiveGoalState[i] = _stateToSet;
		}
	}

	public void SetText(TextState _stateToSet, int _playerID) {
		switch ( _stateToSet ) {
			case TextState.Active:
				text3D[_playerID].SetActive(true);
				break;
			case TextState.Inactive:
				text3D[_playerID].SetActive(false);
				break;
			default:
				break;

		}
		ActiveTextState[_playerID] = _stateToSet;
	}

	public void SetAllText(TextState _stateToSet) {
		for(int i = 0; i < 4; i++ ) {
			switch ( _stateToSet ) {
				case TextState.Active:
					text3D[i].SetActive(true);
					break;
				case TextState.Inactive:
					text3D[i].SetActive(false);
					break;
				default:
					break;

			}
			ActiveTextState[i] = _stateToSet;
		}
	}
}
