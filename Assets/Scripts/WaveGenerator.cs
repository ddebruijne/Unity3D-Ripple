using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {

    public int segments = 1;
    private int vertsPerSegment = 4;


    public Mesh mesh;

    void Start() {
        CreateMesh();
    }

	void Update () {
        UpdateMesh();
	}

    public void CreateMesh() {
        if (mesh != null) return;

        mesh = new Mesh();
        mesh.name = "Wave_Mesh";

        UpdateMesh();
    }

    public void UpdateMesh() {
        mesh.;


    }
}
