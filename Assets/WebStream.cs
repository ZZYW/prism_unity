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

    //public Texture[] frames { get; private set; }

    int frameNumber;

    float freq = 0.05f;

    public static List<Texture> Frames;



    private void Awake()
    {
        frameNumber = MirrorManager.Dimension.n;
        main = this;
        Frames = new List<Texture>();
        InvokeRepeating("AddFrame", 0, freq);

    }

    private void Start()
    {
        StartCoroutine(CheckFrame());
    }


    private void Update()
    {

    }


    void AddFrame()
    {
        Frames.Add(latestFrame);
        if (Frames.Count > frameNumber)
        {
            Frames.RemoveAt(0);
        }
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