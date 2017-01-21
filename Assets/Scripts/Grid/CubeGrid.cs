using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour {

    public static CubeGrid Instance;

    public List<GridCube> cubes = new List<GridCube>();
    public float raiseRange = 2.5f;
    public float maxRaiseHeight = 1;

    public int playerCount = 2;
    public List<Color> playerColors = new List<Color>();

    void Awake() {
        Instance = this;
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
            float amount = (clampedDistance * a * 6) + (a >= 0.8f ? + 0.5f : 0);
            cube.SetRaiseAmount(amount, player);
        }
    }

    [ContextMenu("Find Cubes")]
    private void FindCubes() {
        cubes = new List<GridCube>(gameObject.GetComponentsInChildren<GridCube>(true));

        foreach(GridCube c in cubes) {
            c.playerCount = playerCount;
            c.colors = playerColors;

            c.SetToDefaultPosition();
        }
    }
}
