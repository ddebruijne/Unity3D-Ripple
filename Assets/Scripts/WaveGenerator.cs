using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaveGenerator : MonoBehaviour {

    [Header("Objects")]
    public Camera cam;

    [Header("Wave Control")]
    public int segments = 1;
    public float waveWidth = 1;
    public float waveAmplitude = 4;
    public float currentWaveValue = 0;

    public AnimationCurve curveSquare;

    private int vertRows = 1;
    private int vertsPerSegment = 4;
    private int trisPerSegment = 6;

    private float screenWidth;
    private float screenHeight;

    [Header("Mesh Objects")]
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Material meshMaterial;

    private Mesh mesh;

    void Start() {
        vertRows = (segments + 1);

        CreateMesh();
    }

	void Update () {
        screenHeight = cam.orthographicSize * 2;
        screenWidth = ((float)Screen.width / (float)Screen.height) * screenHeight;

        /*if (waveType == 0)
            currentWaveValue = Mathf.Sin(Time.time * 2) * 4;
        else if (waveType == 1)
            currentWaveValue = Mathf.Tan(Time.time * 2) * 4;*/

        //currentWaveValue = curveSquare.Evaluate(Time.time) * waveAmplitude;
        currentWaveValue = Mathf.Sin(Time.time * 5);
        waveWidth = Mathf.Sin(Time.time * 2.5f) + 1.5f;

        UpdateMesh();
	}

    [ContextMenu("Create mesh")]
    public void CreateMesh() {
        if (mesh != null) return;

        screenHeight = cam.orthographicSize * 2;
        screenWidth = ((float)Screen.width / (float)Screen.height) * screenHeight;

        mesh = new Mesh();
        mesh.name = "Wave_Mesh";
        meshFilter.mesh = mesh;
        meshRenderer.material = meshMaterial;

        //Set defaults
        Vector3[] verts = new Vector3[vertRows * 2];
        int[] tris = new int[(vertRows - 1) * 2 * 3]; //(rows - 1) * 2 = quads * 3 = tri points

        int startIndexVerts = 0;
        int startIndexTris = 0;
        for (int i = 0; i < vertRows; i++) {
            Debug.Log((float)(i) / (vertRows - 1));
            float x = Mathf.Lerp(screenWidth / -2, screenWidth / 2, (float)(i) / (vertRows - 1));

            verts[startIndexVerts] = new Vector3(x, 1, 0);
            verts[startIndexVerts + 1] = new Vector3(x, -1, 0);

            if(i != 0) {
                tris[startIndexTris + 0] = startIndexVerts - 2;
                tris[startIndexTris + 1] = startIndexVerts;
                tris[startIndexTris + 2] = startIndexVerts - 1;

                tris[startIndexTris + 3] = startIndexVerts;
                tris[startIndexTris + 4] = startIndexVerts + 1;
                tris[startIndexTris + 5] = startIndexVerts - 1;
                startIndexTris += 6;
            }

            startIndexVerts += 2;
        }

        mesh.vertices = verts;
        mesh.triangles = tris;

        //UpdateMesh();
    }

    public void UpdateMesh() {
        float waveWidthHalf = waveWidth / 2;

        Vector3[] verts = new Vector3[mesh.vertices.Length];
        int startIndexVerts = 0;

        for (int i = 0; i < vertRows; i++) {
            if (i == vertRows - 1) {
                verts[startIndexVerts] = new Vector3(screenWidth / 2, waveWidthHalf + currentWaveValue, 0);          //Top right
                verts[startIndexVerts + 1] = new Vector3(screenWidth / 2, -waveWidthHalf + currentWaveValue, 0);          //Bottom right
            } else {
                Vector3 nextTop = mesh.vertices[startIndexVerts + 2];
                Vector3 nextBottom = mesh.vertices[startIndexVerts + 3];
                verts[startIndexVerts] = new Vector3(nextTop.x - (screenWidth / segments), nextTop.y);
                verts[startIndexVerts + 1] = new Vector3(nextBottom.x - (screenWidth / segments), nextBottom.y);
            }

            startIndexVerts += 2;
        }

        mesh.vertices = verts;
    }
}
