using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBloomEffect : MonoBehaviour {

    public Material bloomMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, bloomMaterial);
    }
}
