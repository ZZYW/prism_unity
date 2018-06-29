using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{

    public int useShaderIndex;





    [Header("material and shader")]
    public Shader[] mirrorMatShaders;
    public Material wireMat;
    //public Color wireColor;
    [Range(0.1f, 1f)]
    public float wireSize;
    public Material mirrorMat;
    [Range(0, 100)]
    public float vertexOffsetIntense;
    [Range(0, 100)]
    public float vertexOffsetFreq;
    public Color mirrorColor;

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


        mirrorMat.color = mirrorColor;
        mirrorMat.SetFloat("_Shake", vertexOffsetIntense);
        mirrorMat.SetFloat("_ShakeFreq", vertexOffsetFreq);

        //if (useShaderIndex == 1)
        //{
        //    //mirrorMat.SetColor("_Color", wireColor);
        //    mirrorMat.SetFloat("_V_WIRE_Size", wireSize);
        //}


        analogGlitch.scanLineJitter = lineJitter;
        analogGlitch.verticalJump = verticalJump;
        analogGlitch.colorDrift = colorDrift;
        analogGlitch.horizontalShake = horiShakel;
        digitalGlitch.intensity = digitalGlitchIntensity;


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


        if(Input.GetKeyDown(KeyCode.C))
        {
            mirrorMat.shader=mirrorMatShaders[0];
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            mirrorMat.shader = mirrorMatShaders[1];
        }



    }




}
