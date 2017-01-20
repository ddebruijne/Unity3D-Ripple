using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[ReadOnly] public int score;
	public Text ScoreText;


	public void AddScore(int i) {
		score += i;
		ScoreText.text = "Score: " + score;
	}

	public void AddScore() {
		score++;
		ScoreText.text = "Score: " + score;
	}
}
