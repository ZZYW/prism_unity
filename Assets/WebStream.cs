using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class WebStream : MonoBehaviour
{

    public static WebStream main;

    public string url = "http://192.168.1.12:8080/video";

    public Texture latestFrame { get; private set; }


    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        StartCoroutine(CheckFrame());
    }

    IEnumerator CheckFrame()
    {
        while (true)
        {
            using (WWW www = new WWW(url))
            {
                yield return www;
                latestFrame = www.texture;
            }
        }

    }






}