using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour {

    public static CubeGrid Instance;

    public List<GridCube> cubes = new List<GridCube>();
    public List<GlassPulse> glasses = new List<GlassPulse>();
    public List<GameObject> goalParents = new List<GameObject>();

    public float pushRange = 2.5f;

    public int playerCount = 2;
    public List<Color> playerColors = new List<Color>();

    public AnimationCurve coolCurve;

    public Vector2 boundsX = Vector2.zero;
    public Vector2 boundsY = Vector2.zero;

    private List<CubeEffect> cubeEffects = new List<CubeEffect>();

    void Awake() {
        Instance = this;
    }

    void Update() {
        UpdateEffects();
    }

    [ContextMenu("Setup Level")]
    private void SetupLevel() {
        cubes = new List<GridCube>(gameObject.GetComponentsInChildren<GridCube>(true));

        foreach(GridCube c in cubes) {
            c.SetToDefaultPosition();
        }

        ResetBounds();

        glasses = new List<GlassPulse>(gameObject.GetComponentsInChildren<GlassPulse>(true));

        foreach(GlassPulse glass in glasses) {
            glass.GetComponent<GridCube>().isMoveable = true;
        }
    }

    public void ResetBounds() {
        foreach (GridCube c in cubes) {
            Vector2 cPos = new Vector2(c.transform.position.x, c.transform.position.z);
            if (cPos.x < boundsX.x) boundsX.x = cPos.x;
            if (cPos.x > boundsX.y) boundsX.y = cPos.x;
            if (cPos.y < boundsY.x) boundsY.x = cPos.y;
            if (cPos.y > boundsY.y) boundsY.y = cPos.y;
        }
    }

    #region Cube Effects
    public void AddEffect(CubeEffect effect) {
        cubeEffects.Add(effect);
    }

    public void UpdateEffects() {
        foreach(GridCube cube in cubes) {
            GridCubeMod tMod = new GridCubeMod();
            foreach (CubeEffect effect in cubeEffects) {
                GridCubeMod mod = effect.Update(cube);
                tMod.Combine(mod);
            }

            cube.UpdateCube(tMod.Height, tMod.Color);
        }
    }
    #endregion
}
