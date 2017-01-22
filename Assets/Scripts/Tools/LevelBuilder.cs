using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public static LevelBuilder Instance;

    public int currentPhase = -1;

    public List<GameObject> phaseObjects;

    void Awake() {
        Instance = this;
    }

    void Start() {
        foreach (GameObject obj in phaseObjects) {
            foreach(GridCube c in GetCubes(obj)) {
                c.SetRaiseAmount(-100, 0, 4);
            }
            obj.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) GoToPhase(2, 1);
    }

    public void NextPhase() {
        GoToPhase(currentPhase + 1, 1);
    }

    public void GoToPhase(int phase, int waitTime) {
        if(phase >= GameManager.instance.Goals.Count) {
            Hide(GetCubes(phaseObjects[currentPhase]), currentPhase);
            //GameManager.instance.g
            return;
        }

        StartCoroutine(GoToPhaseSequence(phase, 1));

        if(phase == 2) {
            CameraController.Instance.SetOrtographicSize(20);
        }else {
            CameraController.Instance.SetOrtographicSize(17);
        }

        if(phase >= 2) {
            GameManager.instance.SpawnBalls();
        }
    }

    private IEnumerator GoToPhaseSequence(int phase, int waitTime) {
        HidePreviousPhase();
        yield return new WaitForSeconds(waitTime);
        Show(GetCubes(phaseObjects[phase]), phase);

        if (phase != 0) currentPhase = phase;
    }

    private IEnumerable<GridCube> GetCubes(GameObject parent) {
        return parent.GetComponentsInChildren<GridCube>(true);
    }

    private void HidePreviousPhase() {
        if (currentPhase >= 1) Hide(GetCubes(phaseObjects[currentPhase]), currentPhase);
    }

    public void Hide(IEnumerable<GridCube> cubes, int phase) {
        foreach (GridCube c in cubes) {
            StartCoroutine(AnimateCube(c, 0, -50));
        }

        foreach (Goal g in phaseObjects[phase].GetComponentsInChildren<Goal>(true)) {
            g.gameObject.SetActive(false);
        }

        StartCoroutine(HideDelay(phaseObjects[phase]));
    }

    private IEnumerator HideDelay(GameObject obj) {
        yield return new WaitForSeconds(5);
        obj.SetActive(false);
    }

    public void Show(IEnumerable<GridCube> cubes, int phase) {
        phaseObjects[phase].SetActive(true);
        foreach (GridCube c in cubes) {
            StartCoroutine(AnimateCube(c, -50, 0));
        }

        foreach(Goal g in phaseObjects[phase].GetComponentsInChildren<Goal>(true)) {
            g.gameObject.SetActive(true);
        }
    }

    private IEnumerator AnimateCube(GridCube cube, float start, float end) {
        Vector3 originalScale = cube.originalScale;
        Vector3 startScale = (start == 0 ? originalScale : Vector3.zero);
        Vector3 endScale = (start == 0 ? Vector3.zero : originalScale);

        cube.SetRaiseAmount(start * 2, 1, 4);

        cube.transform.localScale = startScale;

        yield return new WaitForSeconds(Random.Range(0, 1f));

        float value = start;
        float animationSpeed = Random.Range(0.1f, 0.15f) * (end != 0 ? 0.75f: 1);
        while (Mathf.Abs(end - value) >= 0.01f) {
            cube.SetRaiseAmount(value, value, 4);
            value = Mathf.Lerp(value, end, animationSpeed);
            cube.transform.localScale = Vector3.Lerp(startScale, endScale, Mathf.Abs(value / end));
            yield return null;
        }

        cube.SetRaiseAmount(end, 0, 4);
        cube.transform.localScale = endScale;
    }
}
