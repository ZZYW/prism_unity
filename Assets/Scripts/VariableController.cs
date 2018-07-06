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


    [System.Serializable]
    public class MatVariables
    {
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
    }


    MatVariables[] matVariables;
    [Header(">>>>> Materials")]
    [SerializeField]
    MatVariables bigPrismMat = new MatVariables();
    [SerializeField]
    MatVariables mirrorsMat = new MatVariables();


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
    public bool enterColorMode;

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

        matVariables = new[] { bigPrismMat, mirrorsMat };

    }

    // Update is called once per frame
    void Update()
    {

        lineJitter = Map(AudioController.instance.glitchDB, 0f, 0.2f, 0, 1f);
        //print(AudioController.instance.glitchDB);



        KeyControls();
        ApplyVariables();
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }


    void KeyControls()
    {
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



        if (Input.GetKeyDown(KeyCode.M))
        {
            lineJitter = Random.Range(0f, 1f);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            lineJitter = 0f;
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
            foreach (var s in matVariables)
            {
                s.useRMStyle = !s.useRMStyle;
                print("rosa style: " + s.useRMStyle);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var s in matVariables)
            {
                s.useRainbowStyle = !s.useRainbowStyle;
                print("rainbow shader: " + s.useRainbowStyle);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var s in matVariables)
            {
                s.UseAlbedoOnRM = !s.UseAlbedoOnRM;
                print("use albedo on rm style: " + s.UseAlbedoOnRM);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            MirrorManager.instance.matrixRotate = !MirrorManager.instance.matrixRotate;
        }

    }


    void ApplyVariables()
    {

        Material[] myMats = new Material[] { mainPrismMat, mirrorMat };


        analogGlitch.scanLineJitter = lineJitter;
        analogGlitch.verticalJump = verticalJump;
        analogGlitch.colorDrift = colorDrift;
        analogGlitch.horizontalShake = horiShakel;
        digitalGlitch.intensity = digitalGlitchIntensity;
        for (int i = 0; i < myMats.Length; i++)
        {
            myMats[i].SetFloat("_Shake", matVariables[i].vertexOffsetIntense);
            myMats[i].SetFloat("_ShakeFreq", matVariables[i].vertexOffsetFreq);
            myMats[i].SetFloat("_RandomMult", matVariables[i].vertexRandomMult);
            myMats[i].SetFloat("_NoisePara01", matVariables[i].noisePara01);
            myMats[i].SetFloat("_NoisePara02", matVariables[i].noiseScale);
            myMats[i].SetFloat("_NoisePara03", matVariables[i].randomNoiseScale);
            myMats[i].SetInt("_RMStyle", matVariables[i].useRMStyle ? 1 : 0);
            myMats[i].SetInt("_NormalRainbow", matVariables[i].useRainbowStyle ? 1 : 0);
            myMats[i].SetInt("_UseAlbedo", matVariables[i].UseAlbedoOnRM ? 1 : 0);
        }
    }


}
