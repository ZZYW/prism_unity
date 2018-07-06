using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    //public audio
    public AudioMixer mainMixer;


    public AudioSource ambient01;
    public AudioSource ambient02;
    public AudioSource ambient03;
    public AudioSource scifiCommunication;
    public AudioSource colorAmbient;
    public AudioSource glitch;

    public float ambient01DB;
    public float ambient02DB;
    public float ambient03DB;
    public float scifiCommunicationDB;
    public float colorAmbientDB;
    public float glitchDB;




    bool inColorMode;
    bool playGlitch;


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //AudioMixerPlayable audioMixerPlayable = mixerGroups[0].
    }

    // Update is called once per frame
    void Update()
    {
        ambient01DB = GetDB(ambient01);
        ambient02DB = GetDB(ambient02);
        ambient03DB = GetDB(ambient03);
        scifiCommunicationDB = GetDB(scifiCommunication);
        glitchDB = GetDB(glitch);
        colorAmbientDB = GetDB(colorAmbient);


        //sum /= spectrum.Length;
        //float avg = for(int )
        //Debug.Log(sum);


        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    inColorMode = !inColorMode;
        //    EnterColorMode(inColorMode);
        //}

        ////if(Input.GetKeyDown)

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    if (glitch.isPlaying == false)
        //    {
        //        glitch.Play();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.M))
        //{
        //    if (glitch.isPlaying)
        //    {
        //        glitch.Pause();
        //    }
        //}
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


    public void EnterColorMode(bool state)
    {
        //PlayGlitch(0.3f);
        //if (state)
        //{
        //    if (colorTrack.isPlaying == false)
        //    {
        //        colorTrack.Play();
        //    }
        //    mainMixer.SetFloat("colorVol", 1);
        //    mainMixer.SetFloat("finalVol", 0);
        //}
        //else
        //{
        //    mainMixer.SetFloat("colorVol", 0);
        //    mainMixer.SetFloat("finalVol", 1);
        //}
        ////throw new System.NotImplementedException();
    }

    void PlayGlitch(float duration)
    {
        //glitchAS.Play();
        //Invoke("StopGlitch", duration);
    }

    void StopGlitch()
    {
        //  glitchAS.Pause();
    }

}
