using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour {

    public static CubeGrid Instance;

    public List<GridCube> cubes = new List<GridCube>();
    public List<GlassPulse> glasses = new List<GlassPulse>();

    public float raiseRange = 2.5f;
    public float maxRaiseHeight = 1;

    public int playerCount = 2;
    public List<Color> playerColors = new List<Color>();

    public AnimationCurve coolCurve;

    void Awake() {
        Instance = this;
    }

    void Start() {
        BuildLevelSequence();
    }

    void Update() {
        //SetRaiseAmount(new Vector2(Mathf.Sin(Time.time / 2) * 10, Mathf.PerlinNoise(Time.time, Time.time) + Mathf.Sin(Time.time / 4) * 5) - new Vector2(5,5), 2, 0);
        //SetRaiseAmount(new Vector2(Mathf.PerlinNoise(Time.time, Time.time) + Mathf.Sin(Time.time / 5) * 10, Mathf.Sin(Time.time / 2) * 10) - new Vector2(5, 5), 1, 1);
    }

    public void SetRaiseAmount(Vector2 location, float a, int player) {
        a = Mathf.Clamp01(a);
        foreach(GridCube cube in cubes) {
            float distance = raiseRange / Vector2.Distance(location, new Vector2(cube.transform.position.x, cube.transform.position.z));
            float clampedDistance = Mathf.Clamp01(distance);
            float amount = (clampedDistance * a * 6) - 0.1f;
                //+ (a >= 0.8f ? + 0.5f : 0);
            cube.SetRaiseAmount(amount, player);
        }
    }

    private void BuildLevelSequence() {
        List<GridCube> cubestoLoad = new List<GridCube>(cubes);

        Debug.Log("LUL");
        foreach(GridCube c in cubestoLoad) {
            StartCoroutine(AnimateCube(c));
        }
    }

    private IEnumerator AnimateCube(GridCube cube) {
        Vector3 originalScale = cube.transform.localScale;

        cube.SetRaiseAmount(-100, 4);
        cube.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(Random.Range(0, 1f));

        float value = -100;
        float animationSpeed = Random.Range(0.05f, 0.1f);
        while (value <= -0.01f) {
            cube.SetRaiseAmount(value, 4);
            value = Mathf.Lerp(value, 0, animationSpeed);
            cube.transform.localScale = Vector3.Lerp(cube.transform.localScale, originalScale, animationSpeed);
            yield return null;
        }

        cube.SetRaiseAmount(0, 4);
    }

    [ContextMenu("Find Cubes")]
    private void FindCubes() {
        cubes = new List<GridCube>(gameObject.GetComponentsInChildren<GridCube>(true));

        foreach(GridCube c in cubes) {
            //c.playerCount = playerCount;
            c.colors = playerColors;

            c.SetToDefaultPosition();
        }

        glasses = new List<GlassPulse>(gameObject.GetComponentsInChildren<GlassPulse>(true));
    }
}
