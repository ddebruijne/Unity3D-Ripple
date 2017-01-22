using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX {
	Goal,
	MenuConfirm
}

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	[Header("Audio Clips")]
	public AudioClip[] BGM;
	public AudioClip goal;
	public AudioClip menuConfirm;

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
		AudioSource_SFX.Stop();
		switch ( SFXToPlay ) {
			case SFX.Goal:
				if ( AudioSource_SFX.clip != goal ) AudioSource_SFX.clip = goal;
				break;
			case SFX.MenuConfirm:
				if ( AudioSource_SFX.clip != menuConfirm ) AudioSource_SFX.clip = menuConfirm;
				break;
			default:
				break;

		}
		AudioSource_SFX.Play();
	}
}
