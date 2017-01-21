using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour {

    public List<GridCube> cubes = new List<GridCube>();
    public float raiseRange = 2.5f;
    public float maxRaiseHeight = 1;

    void Update() {
        ResetRaise();

        SetRaiseAmount(new Vector2(Mathf.Sin(Time.time) * 4, 0), 2);
        SetRaiseAmount(new Vector2(0, Mathf.Sin(Time.time) * 4), 2);
    }

    public void ResetRaise() {
        foreach (GridCube cube in cubes) {
            cube.SetRaiseAmount(0);
        }
    }

    public void SetRaiseAmount(Vector2 location, float a) {
        foreach(GridCube cube in cubes) {
            float distance = raiseRange / Vector2.Distance(location, new Vector2(cube.transform.position.x, cube.transform.position.z));
            float clampedDistance = Mathf.Clamp01(distance);
            float amount = clampedDistance * a;
            cube.AddRaiseAmount(amount);
        }
    }

    [ContextMenu("Find Cubes")]
    private void FindCubes() {
        cubes = new List<GridCube>(gameObject.GetComponentsInChildren<GridCube>());
    }
}
