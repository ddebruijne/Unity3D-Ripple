using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	int players = 2;
	public GameObject PlayerBallPrefab;

	public List<Vector3> PlayerStartPositions;
	public List<GameObject> PlayerObjects;



	void Start() {
		for(int i = 0; i < players; i++ ) {
			if ( i >= PlayerStartPositions.Count ) break;	//Safety measure
			GameObject go = Instantiate(PlayerBallPrefab, PlayerStartPositions[i], Quaternion.identity);
			go.GetComponent<PlayerBall>().SetupPlayer(i);
			PlayerObjects.Add(go);

		}
	}
}
