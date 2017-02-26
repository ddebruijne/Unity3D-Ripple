using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public int levelToLoad;

	// Use this for initialization
	void Start () {
		StartCoroutine(CallLoad());
	}

	IEnumerator CallLoad() {
		AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
		yield return async;
	}
	

}
