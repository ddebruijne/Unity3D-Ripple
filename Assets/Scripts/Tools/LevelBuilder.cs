using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		LevelBuilder Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class LevelBuilder : MonoBehaviour {

    public static LevelBuilder Instance;
    public AnimationCurve animationCurve;

    void Awake() {
        Instance = this;
    }

    public void GoToPhase(LevelPhase thisPhase, LevelPhase nextPhase) {
        Hide(thisPhase);
        CameraController.Instance.SetOrtographicSize(nextPhase.cameraOrtographicSize);
        UnityUtilities.ExecuteAfterDelay(this, 2, () => { Show(nextPhase); });
    }

    public void Show(LevelPhase phase) {

    }

    public void Hide(LevelPhase phase) {
        StartCoroutine(HideDelay(phase));
    }

    private IEnumerator HideDelay(LevelPhase obj) {
        yield return new WaitForSeconds(5);
        obj.gameObject.SetActive(false);
        obj.SetActiveState(false);
    }
}
