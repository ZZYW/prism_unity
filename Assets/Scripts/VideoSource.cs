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

        webcamTexture = new WebCamTexture (devices[1].name);
    }

    private void Start () {
        webcamTexture.Play ();
    }

}