using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[ReadOnly] public int score;

	public void AddScore(int i) {
		score += i;
	}

	public void AddScore() {
		score++;
	}
}
