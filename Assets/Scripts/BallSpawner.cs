using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		BallSpawner Class
 *		This class spawns balls in waves when told to do so.
 *		
 *		Created 08-02-2017 by Danny de Bruijne
 */

public class BallSpawner : MonoBehaviour {

	public static BallSpawner Instance;

	public GameObject ballPrefab;
	public Transform ballSpawnPos;

	public bool doneSpawning = false;
	private float levelStartTime = 0;

	[ReadOnly] public List<GameObject> balls = new List<GameObject>();


	void Awake() { Instance = this; }

	private IEnumerator SpawnBallRoutine() {
		int currentWave = 0;
		doneSpawning = false;
		levelStartTime = Time.time;

		yield return new WaitForSeconds(2);
		while ( true ) {
			if ( currentWave < 6 ) {
				for ( int i = 0; i < (currentWave + 1) * 5; i++ ) {
					SpawnBall();
					yield return new WaitForSeconds(0.1f);
				}
				currentWave++;
				yield return new WaitForSeconds(5);
			} else {
				break;
			}
		}

		doneSpawning = true;
        Debug.Log("I'm done. I'm fucking done.");
		yield return null;
	}

	public void SpawnBalls() {
		StartCoroutine(SpawnBallRoutine());
	}

	public void SpawnBall() {
		GameObject b = Instantiate(ballPrefab);
		b.transform.position = ballSpawnPos.position + new Vector3(
			Random.Range(-0.5f, 0.5f),
			Random.Range(-0.5f, 0.5f),
			Random.Range(-0.5f, 0.5f));
		b.GetComponent<Rigidbody>().AddForce(new Vector3(
			Random.Range(-100, 100),
			Random.Range(-100, 100),
			Random.Range(-100, 100)), ForceMode.Acceleration);

		balls.Add(b);
	}
}
