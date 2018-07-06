using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    [SerializeField]
    private AudioMixer mainMixer;
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


    //bool inColorMode;
    //bool playGlitch;


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
        bellscuDB = GetDB(bellscu);
        introDB = GetDB(intro);
        susbellDB = GetDB(susbell);

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
