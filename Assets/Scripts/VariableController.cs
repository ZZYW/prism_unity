using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{

    public static VariableController instance;


    public bool controlVariablesByCode = true;

    [Header(">>>>> Link Mat Here")]
    public Shader[] mirrorMatShaders;
    public Material wireMat;
    public Material mainPrismMat;
    public Material planeMat;
    public Material mirrorMat;


    [System.Serializable]
    public class MatVariables
    {
        [Range(0, 3)]
        public float vertexOffsetIntense;
        [Range(0.2f, 3)]
        public float vertexOffsetFreq = 0.2f;//min
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
    MatVariables bigPrismMatSettings = new MatVariables();
    [SerializeField]
    MatVariables mirrorMatSettings = new MatVariables();


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

    AudioController audioController;


    //stage control
    public bool broken;


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

        matVariables = new[] { bigPrismMatSettings, mirrorMatSettings };
        audioController = AudioController.instance;

        //StageController.instance.SwtichStage(4);

    }

    // Update is called once per frame
    void Update()
    {

        if (controlVariablesByCode)
        {
            lineJitter = Map(audioController.glitchDB, 0f, 0.2f, 0f, 1f);

            colorDrift = Map(audioController.introDB, 0f, 1f, 0f, 1f);

            foreach (var s in matVariables)
            {
                s.vertexOffsetFreq = Map(audioController.bellscuDB, 0, 1, 0, 3);
                s.vertexOffsetIntense = Map(audioController.ambient01DB, 0, 10, 0, 3);
            }

            if (audioController.colorAmbientDB > 0.1f)
            {
                //TODO: toggle off other tracks
                foreach (var s in matVariables)
                {
                    s.useRainbowStyle = true;
                }
            }
            else
            {
                foreach (var s in matVariables)
                {
                    s.useRainbowStyle = false;
                }
            }



        }



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
            SceneController.instance.Stage = SceneController.STAGE.SELF_ROT_MIRRORS;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneController.instance.Stage = SceneController.STAGE.MIRROR_N_WIREFRAME;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneController.instance.Stage = SceneController.STAGE.ALIGNED_MIRRORS;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneController.instance.Stage = SceneController.STAGE.BIG_PRISM;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneController.instance.Stage = SceneController.STAGE.LACC;
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
