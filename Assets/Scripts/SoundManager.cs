using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		SoundManager Class
 *		Used to play sounds.
 *		
 *		Created 28-01-2017 by Danny de Bruijne
 */

public enum SFX {
	Goal,
	MenuConfirm,
    BallBoop
}

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	[Header("Audio Clips")]
	public AudioClip[] BGM;
	public AudioClip goal;
	public AudioClip menuConfirm;
    public AudioClip ballBoop;

	[Header("Audio Sources")]
	public AudioSource AudioSource_BGM;
	public AudioSource AudioSource_SFX;

	// Use this for initialization
	void Start () {
		Instance = this;

		if ( BGM.Length > 0 ) {
			AudioSource_BGM.clip = BGM[0];
			AudioSource_BGM.Play();
			AudioSource_BGM.loop = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Next bgm logic
	}

	public void PlaySFX(SFX SFXToPlay) {
		switch ( SFXToPlay ) {
			case SFX.Goal:
                AudioSource_SFX.PlayOneShot(goal);
				break;
			case SFX.MenuConfirm:
                AudioSource_SFX.PlayOneShot(menuConfirm);
                break;

            case SFX.BallBoop:
                AudioSource_SFX.PlayOneShot(ballBoop, 0.5f);
                break;

			default:
				break;

		}
		AudioSource_SFX.Play();
	}
}
