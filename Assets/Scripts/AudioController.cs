using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;


    public TimelineAsset timeline;
    public TrackAsset trackAmbient01;

    //[SerializeField]
    //private AudioMixer mainMixer;
    [SerializeField]
    private AudioSource ambient01;
    [SerializeField]
    private AudioSource ambient02;
    [SerializeField]
    private AudioSource ambient03;
    [SerializeField]
    private AudioSource intro;
    [SerializeField]
    private AudioSource susbell;
    [SerializeField]
    private AudioSource bellscu;
    [SerializeField]
    private AudioSource scifiCommunication;
    [SerializeField]
    private AudioSource colorAmbient;
    [SerializeField]
    private AudioSource glitch;

    public float ambient01DB { get; private set; }
    public float ambient02DB { get; private set; }
    public float ambient03DB { get; private set; }
    public float scifiCommunicationDB { get; private set; }
    public float colorAmbientDB { get; private set; }
    public float glitchDB { get; private set; }
    public float susbellDB { get; private set; }
    public float introDB { get; private set; }
    public float bellscuDB { get; private set; }

    private AudioSource[] allSources;
    public static float LENGTH = 0;

    void Awake()
    {
        instance = this;
        LENGTH = ambient01.clip.length;
    }

    // Use this for initialization
    void Start()
    {
        allSources = new[] { ambient01, ambient02, ambient03, intro, susbell, bellscu, scifiCommunication, colorAmbient, glitch };
    }

    // Update is called once per frame
    void Update()
    {

        //timeline.

        ambient01DB = GetDB(ambient01);
        ambient02DB = GetDB(ambient02);
        ambient03DB = GetDB(ambient03);
        scifiCommunicationDB = GetDB(scifiCommunication);
        glitchDB = GetDB(glitch);
        colorAmbientDB = GetDB(colorAmbient);
        bellscuDB = GetDB(bellscu);
        introDB = GetDB(intro);
        susbellDB = GetDB(susbell);
    }


    public void SetProgress(float p)
    {
        foreach (AudioSource source in allSources)
        {
            source.time = p * LENGTH;
        }
    }

    private float GetDB(AudioSource source)
    {
        //timeline.

        float[] spectrum = new float[64];
        source.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        float sum = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            sum += spectrum[i];
        }
        return sum;
    }
    public float GetProgress()
    {
        return ambient01.time / LENGTH;
    }

    public void muteNonColorTracks()
    {
        intro.mute = true;
        ambient01.mute = true;
        ambient02.mute = true;
        ambient03.mute = true;
        susbell.mute = true;
        bellscu.mute = true;
        glitch.mute = true;
        scifiCommunication.mute = true;
    }

    public void unmuteNonColorTracks()
    {
        intro.mute = false;
        ambient01.mute = false;
        ambient02.mute = false;
        ambient03.mute = false;
        susbell.mute = false;
        bellscu.mute = false;
        glitch.mute = false;
        scifiCommunication.mute = false;
    }
}
