using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateScoreText() {
		//for ( int i = 0; i < PlayerObjects.Count; i++ ) {
			//ScoreText[i].GetComponentInChildren<Text>().text = PlayerObjects[i].GetComponent<PlayerBall>().score + "";
		//}
	}
}
