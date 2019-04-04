using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class VideoSource : MonoBehaviour {

    public static VideoSource main;

    public WebCamTexture webcamTexture;

    private void Awake () {
        main = this;
        WebCamDevice[] devices = WebCamTexture.devices;

        int webCamIndex = 0;
        if (devices.Length > 1) {
            webCamIndex = 1;
        }
        
        webcamTexture = new WebCamTexture (devices[webCamIndex].name);
    }

    private void Start () {
        webcamTexture.Play ();
    }

}