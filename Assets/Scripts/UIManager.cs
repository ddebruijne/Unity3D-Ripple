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

	void Awake() { Instance = this; }

	public void Setup(bool splash) {
		Splash.SetActive(splash);
		HUD.SetActive(false);
	}

	public void SplashAnimation() { StartCoroutine(SplashAnimationSequence()); }

	public IEnumerator SplashAnimationSequence() {
		Splash.GetComponent<Animator>().speed = 1;
		yield return new WaitForSeconds(1);
		Splash.SetActive(false);
		NewGameManager.Instance.CameraAnimator.speed = 1;
	}

	public void GameDoneAnimation() {
		StartCoroutine(GameDoneSequence());
	}

	IEnumerator GameDoneSequence() {
		float aTime = 1;
		float aValue = 1;
		float alpha = GameDone.GetComponent<Image>().color.a;

		for ( float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime ) {
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			GameDone.GetComponent<Image>().color = newColor;
			yield return null;
		}
	}

	public void ActivateHUD() {
		HUD.SetActive(true);
		UpdateScoreText();
	}

	public void UpdateScoreText() {
		for ( int i = 0; i < NewGameManager.Instance.players.Count; i++ ) {
			ScoreText[i].GetComponentInChildren<Text>().text = NewGameManager.Instance.players[i].GetComponent<NewPlayer>().score + "";
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
