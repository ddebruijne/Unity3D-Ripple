using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	[ReadOnly] public PlayerBall player;
    public GoalLight goalLight;
    public GridCube goalPost;
	public Color gridColor;

	public void SetupGoal(PlayerBall _player) {
		player = _player;
		gridColor = CubeGrid.Instance.playerColors[player.playerIndex];
	}

    void Update() {
        CubeGrid.Instance.SetRaiseAmount(new Vector2(goalPost.transform.position.x, goalPost.transform.position.z), Mathf.Sin(Time.time) + 1.5f, 1, 0, player.playerIndex + 6);
    }

	//Called when a ball collides with us.
	public void OnScoreEvent() {
		GameManager.instance.Score(player.playerIndex);
        goalLight.FlashColor(CubeGrid.Instance.playerColors[player.playerIndex], player.playerIndex);
		SoundManager.Instance.PlaySFX(SFX.Goal);
    }

}
