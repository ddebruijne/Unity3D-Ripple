using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *		CameraBloomEffect Class
 *		...
 *		
 *		Created 28-01-2017 by Randy Schouten
 */

public class CameraBloomEffect : MonoBehaviour {

    public Material bloomMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, bloomMaterial);
    }
}
