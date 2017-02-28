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
    public AnimationCurve animationCurveInverse;

    void Awake() {
        Instance = this;
    }

    public void GoToPhase(LevelPhase thisPhase, LevelPhase nextPhase) {
        Hide(thisPhase);
        CameraController.Instance.SetOrtographicSize(nextPhase.cameraOrtographicSize);
        UnityUtilities.ExecuteAfterDelay(this, 2, () => { Show(nextPhase); });
    }

    public void Show(LevelPhase phase) {

        CubeEffectNoise effect = CubeGrid.Instance.noiseEffect;
        Transform phaseTransform = phase.transform;
        Vector3 phaseStartPos = phaseTransform.localPosition - new Vector3(0, 50, 0);
        Vector3 phaseEndPos = phaseTransform.localPosition;

        phaseTransform.gameObject.SetActive(true);
        phase.SetActiveState(true);

        UnityUtilities.StartTimedRoutine(this, 2, (progress) => {
            effect.GetSettings().Power = Mathf.Lerp(-20, 0, animationCurveInverse.Evaluate(progress));
            phaseTransform.localPosition = Vector3.Lerp(phaseStartPos, phaseEndPos, animationCurve.Evaluate(progress));

            if(progress == 1) {
                BallSpawner.Instance.SpawnBalls();
            }
        });
    }

    public void Hide(LevelPhase phase) {
        //StartCoroutine(HideDelay(phase));

        CubeEffectNoise effect = CubeGrid.Instance.noiseEffect;
        Transform phaseTransform = phase.transform;
        Vector3 phaseStartPos = phaseTransform.localPosition;
        Vector3 phaseEndPos = phaseTransform.localPosition - new Vector3(0, 50, 0);

        UnityUtilities.StartTimedRoutine(this, 2, (progress) => {
            effect.GetSettings().Power = Mathf.Lerp(0, -20, animationCurve.Evaluate(progress));
            phaseTransform.localPosition = Vector3.Lerp(phaseStartPos, phaseEndPos, animationCurveInverse.Evaluate(progress));

            if (progress == 1) {
                phaseTransform.gameObject.SetActive(false);
                phase.SetActiveState(false);
            }
        });
    }

    private IEnumerator HideDelay(LevelPhase obj) {
        yield return new WaitForSeconds(5);
        obj.gameObject.SetActive(false);
        obj.SetActiveState(false);
    }
}
