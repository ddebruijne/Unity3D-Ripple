using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		GameManager Class
 *		Main gameplay logic class
 *		
 *		Created 01-02-2017 by Danny de Bruijne
 */

public class NewGameManager : MonoBehaviour {

	public static NewGameManager Instance;

	[Header("Level")]
	public GameObject PersistentLevel;
	public List<GameObject> LevelPhases;

	[Header("Player")]
	public GameObject PlayerPrefab;
	public GameObject BallPrefab;

	[Header("Readonly")]
	[ReadOnly] public float time;		//Gametime 

	// Use this for initialization
	void Start () {
		Instance = this;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Setup(){
		//Create 4 Players
		//Set Phase 1 active 
		//
	}


	/*
	 *	Support Functions
	 */

	LevelPhase GetLevelPhase(GameObject phasego) { return phasego.GetComponent<LevelPhase>(); }
}
