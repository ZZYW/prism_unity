using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{



    public int useShaderIndex;

    public Shader[] mirrorMatShaders;

    public Material mirrorMat;
    public float vertexOffsetIntense;
    public float vertexOffsetFreq;
    public Color mirrorColor;

    public Material wireMat;
    public Color wireColor;
    [Range(0.1f, 1f)]
    public float wireSize;

    [Header("glitch")]
    [SerializeField, Range(0, 1)]
    public float lineJitter;
    [SerializeField, Range(0, 1)]
    public float verticalJump;
    [SerializeField, Range(0, 1)]
    public float horiShakel;
    [SerializeField, Range(0, 1)]
    public float colorDrift;
    [SerializeField, Range(0, 1)]
    public float digitalGlitchIntensity;


    public float[] spectrum = new float[256];

    Kino.AnalogGlitch analogGlitch;
    Kino.DigitalGlitch digitalGlitch;


    public int controlStage = 0;


    // Use this for initialization
    void Start()
    {
        analogGlitch = Camera.main.GetComponent<Kino.AnalogGlitch>();
        digitalGlitch = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }

    // Update is called once per frame
    void Update()
    {

        useShaderIndex = Mathf.Clamp(useShaderIndex, 0, mirrorMatShaders.Length - 1);

        mirrorMat.shader = mirrorMatShaders[useShaderIndex];

        mirrorMat.color = mirrorColor;
        mirrorMat.SetFloat("_Shake", vertexOffsetIntense);
        mirrorMat.SetFloat("_ShakeFreq", vertexOffsetFreq);

        if (useShaderIndex == 1)
        {
            mirrorMat.SetColor("_Color", wireColor);
            mirrorMat.SetFloat("_V_WIRE_Size", wireSize);
        }


        analogGlitch.scanLineJitter = lineJitter;
        analogGlitch.verticalJump = verticalJump;
        analogGlitch.colorDrift = colorDrift;
        analogGlitch.horizontalShake = horiShakel;
        digitalGlitch.intensity = digitalGlitchIntensity;


        spectrum = new float[256];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);



        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrum[i] *= 10000f;
            spectrum[i] = (int)spectrum[i];
        }





        if (controlStage == 0)
        {
            lineJitter = spectrum[10] / 100;
            verticalJump = spectrum[12] / 200;
            vertexOffsetIntense = 0;
            vertexOffsetFreq = 0;
        }
        else if (controlStage == 1)
        {
            lineJitter = 0;
            verticalJump = 0;
            vertexOffsetIntense = spectrum[10] / 150;
            vertexOffsetFreq = spectrum[12] / 200;
        }


        if (Input.GetKey(KeyCode.B))
        {
            controlStage++;
            if (controlStage > 1)
            {
                controlStage = 0;
            }
        }
    }




}
