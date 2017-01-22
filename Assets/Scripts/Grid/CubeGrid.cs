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

    public Vector2 boundsX = Vector2.zero;
    public Vector2 boundsY = Vector2.zero;

    void Awake() {
        Instance = this;
    }

    void Start() {
        BuildLevelSequence();
    }

    public void SetRaiseAmount(Vector2 location, float height, float color, float overallColor, int player) {
        overallColor *= 2.5f;
        height = Mathf.Clamp01(height);
        color = Mathf.Clamp01(color);
        foreach(GridCube cube in cubes) {
            float distance = raiseRange / Vector2.Distance(location, new Vector2(cube.transform.position.x, cube.transform.position.z));
            float clampedDistance = Mathf.Clamp01(distance);
            float amount = (clampedDistance * height * 2.5f) - 0.1f;
            cube.SetRaiseAmount(amount + (overallColor / 5), (color * (amount - 0.25f) + (clampedDistance * 2)) + overallColor, player);
        }
    }

    public void BuildLevelSequence() {
        List<GridCube> cubestoLoad = new List<GridCube>(cubes);

        Debug.Log("LUL");
        foreach(GridCube c in cubestoLoad) {
            StartCoroutine(AnimateCube(c));
        }
    }

    private IEnumerator AnimateCube(GridCube cube) {
        Vector3 originalScale = cube.transform.localScale;

        cube.SetRaiseAmount(-100, 1, 4);
        cube.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(Random.Range(0, 1f));

        float value = -100;
        float animationSpeed = Random.Range(0.05f, 0.1f);
        while (value <= -0.01f) {
            cube.SetRaiseAmount(value, value, 4);
            value = Mathf.Lerp(value, 0, animationSpeed);
            cube.transform.localScale = Vector3.Lerp(cube.transform.localScale, originalScale, animationSpeed);
            yield return null;
        }

        cube.SetRaiseAmount(0, 0, 4);
    }

    [ContextMenu("Setup Level")]
    private void FindCubes() {
        cubes = new List<GridCube>(gameObject.GetComponentsInChildren<GridCube>(true));

        foreach(GridCube c in cubes) {
            //c.playerCount = playerCount;
            c.colors = playerColors;

            c.SetToDefaultPosition();

            Vector2 cPos = new Vector2(c.transform.position.x, c.transform.position.z);
            if (cPos.x < boundsX.x) boundsX.x = cPos.x;
            if (cPos.x > boundsX.y) boundsX.y = cPos.x;
            if (cPos.y < boundsY.x) boundsY.x = cPos.y;
            if (cPos.y > boundsY.y) boundsY.y = cPos.y;
        }

        glasses = new List<GlassPulse>(gameObject.GetComponentsInChildren<GlassPulse>(true));
    }
}
