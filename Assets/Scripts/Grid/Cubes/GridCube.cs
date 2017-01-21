using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCube : MonoBehaviour {

    private int playerCount = 5;

    private Vector3 defaultPos;
    private Material material;

    public List<float> raiseAmount = new List<float>();
    private List<float> actualRaiseAmount = new List<float>();

    public List<Color> colors = new List<Color>();
    private List<Color> actualColors = new List<Color>();

    public float heightOffset = 0;
    public bool isMoveable = true;

    void Awake() {
        material = GetComponent<MeshRenderer>().material;

        for(int i = 0; i < playerCount; i++) {
            raiseAmount.Add(0);
            actualRaiseAmount.Add(0);
            actualColors.Add(Color.black);
        }
    }

    void Start() {
        defaultPos = GetDefaultPosition();
    }

    public void SetToDefaultPosition() {
        transform.position = GetDefaultPosition();
    }

    public Vector3 GetDefaultPosition() {
        return new Vector3(transform.position.x, -4 + heightOffset, transform.position.z);
    }

    public void SetRaiseAmount(float a, int player) {
        raiseAmount[player] = a;
    }

	void Update () {
        Color c = Color.black;
        float raise = 0;

        for(int i = 0; i < playerCount; i++) {
            actualRaiseAmount[i] = Mathf.Lerp(actualRaiseAmount[i], raiseAmount[i], 0.25f);
            raise += (actualRaiseAmount[i]);

            c += (colors[i] * actualRaiseAmount[i] * 0.5f);
        }

        if(isMoveable) transform.position = defaultPos + new Vector3(0, raise, 0);
        material.SetColor("_Color", c);

    }
}
