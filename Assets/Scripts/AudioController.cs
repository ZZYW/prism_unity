using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    //public audio
    public AudioMixer mainMixer;
    public AudioSource glitchAS;
    //AudioMixerGroup[] mixerGroups;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            inColorMode = !inColorMode;
            EnterColorMode(inColorMode);
        }

        //if(Input.GetKeyDown)

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (glitchAS.isPlaying == false)
            {
                glitchAS.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (glitchAS.isPlaying)
            {
                glitchAS.Pause();
            }
        }
    }


    public void EnterColorMode(bool state)
    {
        PlayGlitch(0.3f);
        if (state)
        {
            mainMixer.SetFloat("colorVol", 1);
            mainMixer.SetFloat("finalVol", 0);
        }
        else
        {
            mainMixer.SetFloat("colorVol", 0);
            mainMixer.SetFloat("finalVol", 1);
        }
        //throw new System.NotImplementedException();
    }

    void PlayGlitch(float duration)
    {
        glitchAS.Play();
        Invoke("StopGlitch", duration);
    }

    void StopGlitch()
    {
        glitchAS.Pause();
    }

}
