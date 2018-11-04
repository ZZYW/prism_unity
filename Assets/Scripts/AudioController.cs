using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    public AudioSource ambient01;
	public AudioSource ambient02;
	public AudioSource ambient03;
	public AudioSource intro;
	public AudioSource susbell;
	public AudioSource bellscu;
	public AudioSource scifiCommunication;
	public AudioSource colorAmbient;
	public AudioSource glitch;

    public float overallDB;
    public float ambient01DB;
    public float ambient02DB;
    public float ambient03DB;
    public float scifiCommunicationDB;
    public float colorAmbientDB;
    public float glitchDB;
    public float susbellDB;
    public float introDB;
    public float bellscuDB;

    public AudioSource[] allSources;
    public static float LENGTH = 0;

    void Awake()
    {
        instance = this;
        LENGTH = ambient01.clip.length;
    }

    void Start()
    {
        allSources = new[] { ambient01, ambient02, ambient03, intro, susbell, bellscu, scifiCommunication, colorAmbient, glitch };
    }

    void Update()
    {
        ambient01DB = GetDB(ambient01);
        ambient02DB = GetDB(ambient02);
        ambient03DB = GetDB(ambient03);
        scifiCommunicationDB = GetDB(scifiCommunication);
        glitchDB = GetDB(glitch);
        colorAmbientDB = GetDB(colorAmbient);
        bellscuDB = GetDB(bellscu);
        introDB = GetDB(intro);
        susbellDB = GetDB(susbell);
        //overallDB = GetDB()


        float[] spectrum = new float[64];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        float sum = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            sum += spectrum[i];
        }
        overallDB = sum;


       // playableDirector.timeUpdateMode = DirectorUpdateMode.GameTime;
      
    }

    public void SetProgress(float time)
    {
        foreach (AudioSource source in allSources)
        {
            source.time = time;
        }
    }

    private float GetDB(AudioSource source)
    {
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
