using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -100) {
            DestroyBall();
        }
	}

	void OnCollisionEnter (Collision col) {
		if(col.gameObject.tag == "Goal" ) {
			Debug.Log("Ayy lmao this is a goal!");
            col.gameObject.GetComponent<Goal>().OnScoreEvent();
            DestroyBall();
		}
	}

	void DestroyBall() {
        GameManager.instance.Balls.Remove(gameObject);
		Destroy(this.gameObject);
	}
}
