using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{

    public static VariableController instance;


    [Header(">>>>> Link Mat Here")]
    public Shader[] mirrorMatShaders;
    public Material wireMat;
    public Material mainPrismMat;
    public Material planeMat;
    public Material mirrorMat;


    [Header(">>>> Shader Variables")]
    [Range(0, 100)]
    public float vertexOffsetIntense;
    [Range(0, 3)]
    public float vertexOffsetFreq;
    [Range(0, 1)]
    public float vertexRandomMult;
    public bool useRMStyle;
    public bool useRainbowStyle;

    [Range(0f, 1f)]
    public float noisePara01;
    [Range(0.1f, 5f)]
    public float noiseScale = 0.3f;
    [Range(0f, 20f)]
    public float randomNoiseScale;

    public bool UseAlbedoOnRM;


    [Header(">>>> Glitch")]
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




    Kino.AnalogGlitch analogGlitch;
    Kino.DigitalGlitch digitalGlitch;



    //boolean 


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        analogGlitch = Camera.main.GetComponent<Kino.AnalogGlitch>();
        digitalGlitch = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }

    // Update is called once per frame
    void Update()
    {


        Material[] myMats = new Material[] { mirrorMat, mainPrismMat };


        analogGlitch.scanLineJitter = lineJitter;
        analogGlitch.verticalJump = verticalJump;
        analogGlitch.colorDrift = colorDrift;
        analogGlitch.horizontalShake = horiShakel;
        digitalGlitch.intensity = digitalGlitchIntensity;


        foreach (var s in myMats)
        {
            s.SetFloat("_Shake", vertexOffsetIntense);
            s.SetFloat("_ShakeFreq", vertexOffsetFreq);
            s.SetFloat("_RandomMult", vertexRandomMult);
            s.SetFloat("_NoisePara01", noisePara01);
            s.SetFloat("_NoisePara02", noiseScale);
            s.SetFloat("_NoisePara03", randomNoiseScale);
            s.SetInt("_RMStyle", useRMStyle ? 1 : 0);
            s.SetInt("_NormalRainbow", useRainbowStyle ? 1 : 0);
            s.SetInt("_UseAlbedo", UseAlbedoOnRM ? 1 : 0);
        }



        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            print("Reloading");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StageController.instance.SwtichStage(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StageController.instance.SwtichStage(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StageController.instance.SwtichStage(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StageController.instance.SwtichStage(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StageController.instance.SwtichStage(4);
        }




        if (Input.GetKeyDown(KeyCode.C))
        {
            mirrorMat.shader = mirrorMatShaders[0];
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            mirrorMat.shader = mirrorMatShaders[1];
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            useRMStyle = !useRMStyle;
            print("rosa style: " + useRMStyle);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            useRainbowStyle = !useRainbowStyle;
            print("rainbow shader: " + useRainbowStyle);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseAlbedoOnRM = !UseAlbedoOnRM;
            print("use albedo on rm style: " + UseAlbedoOnRM);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            MirrorManager.instance.matrixRotate = !MirrorManager.instance.matrixRotate;
        }



    }




}
