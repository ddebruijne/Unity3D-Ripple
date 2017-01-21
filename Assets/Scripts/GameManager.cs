using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	int players = 2;                    //Max 4.
	public GameObject PlayerBallPrefab; //These are mostly empty but with scripts attached.
	public GameObject GoalPrefab;       //Standard orientation bottomleft.

	[ReadOnly] public List<GameObject> PlayerObjects;

	[Header("Goals")]
	[ReadOnly] public List<GameObject> Goals;
	public List<GameObject> ScoreText;
	public List<GameObject> GoalSpawnPoints;
	Vector3 GoalSpawnOffset = new Vector3(0, 0, 0);


	void Start() {
		instance = this;
		CreatePlayers();
		CreateGoals();
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
	}

	void CreateGoals() {
		for ( int i = 0; i < PlayerObjects.Count; i++ ) {
			Vector3 gopos = GoalSpawnPoints[i].transform.position + GoalSpawnOffset;

			GameObject go = Instantiate(GoalPrefab, gopos, Quaternion.identity);
			go.GetComponent<Goal>().SetupGoal(PlayerObjects[i].GetComponent<PlayerBall>());     //TODO: Set correct positions
			go.name = "Player " + i + " Goal";

			//Rotate and positionate based on player.
			switch ( i ) {
				case 0:
				go.transform.Rotate(Vector3.up, 180);   //Topleft
				break;
				case 1:
				go.transform.Rotate(Vector3.up, 0f);    //Bottomright
				break;
				case 2:
				go.transform.Rotate(Vector3.up, 90);    //Bottomleft
				break;
				case 3:
				go.transform.Rotate(Vector3.up, 270);   //Topright
				break;
				default:
				break;
			}

			Goals.Add(go);
		}

		UpdateScoreText();	//Call this in the last part of initialization.
	}

	//when a score event happens on the goal of the playerindex.
	public void Score(int _PlayerIndex) {
		for ( int i = 0; i < PlayerObjects.Count; i++ ) {
			if ( i != _PlayerIndex ) ;
			PlayerObjects[i].GetComponent<PlayerBall>().AddScore();
		}
	}

	public void UpdateScoreText() {
		for ( int i = 0; i < PlayerObjects.Count; i++ ) {
			ScoreText[i].GetComponent<Text>().text = PlayerObjects[i].GetComponent<PlayerBall>().score + "";
		}
	}
}
