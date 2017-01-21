using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	[ReadOnly] public PlayerBall player;

	public void SetupGoal(PlayerBall _player) {
		player = _player;
	}

	//Called when a ball collides with us.
	public void OnScoreEvent() {
		GameManager.instance.Score(player.playerIndex);
	}

}
