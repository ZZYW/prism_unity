using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager instance;

    public Material trailMat;



    //controls
    public bool CubeSelfRotate;


    //Materials
    public Material mirrorMat;
    public Material lineMat;

    //containers
    public static GameObject wireframeCubeContainer;
    public static GameObject mirrorContainer;

    public GameObject centerCube;

    //vars
    GameObject[] mirrors;
    float rotateAngle = 1f;

    public bool matrixRotate = true;


    private void Awake()
    {

        instance = this;

        mirrors = new GameObject[(int)Mathf.Pow(DATA.DimensionData.npr, 3)];


        if (wireframeCubeContainer == null)
        {
            wireframeCubeContainer = new GameObject("Wireframe Cube Container");
            wireframeCubeContainer.transform.parent = transform;
        }


        if (mirrorContainer == null)
        {
            mirrorContainer = new GameObject("Mirror Container");
            mirrorContainer.transform.parent = transform;
        }


        print(mirrors.Length + " mirror in total.");

        int i = 0;

        float scale = DATA.DimensionData.gridSize;
        float diameter = DATA.DimensionData.matrixDiameter;
        float size = DATA.DimensionData.cubeDia;

        for (int x = 0; x < DATA.DimensionData.npr; x++)
        {
            for (int y = 0; y < DATA.DimensionData.npr; y++)
            {
                for (int z = 0; z < DATA.DimensionData.npr; z++)
                {
                    mirrors[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    mirrors[i].transform.parent = mirrorContainer.transform;
                    mirrors[i].transform.localScale = new Vector3(size, size, size);
                    mirrors[i].GetComponent<Collider>().enabled = false;
                    mirrors[i].transform.position = new Vector3(x * scale - DATA.DimensionData.matrixDiameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
                    mirrors[i].GetComponent<Renderer>().material = mirrorMat;
                    Mirror code = mirrors[i].AddComponent<Mirror>();
                    code.SelfRotate = CubeSelfRotate;
                    code.SpaceID = new Vector3(x, y, z);

                    int n = DATA.DimensionData.npr;
                    if (x == n / 2 && y == n / 2 && z == n / 2)
                    {
                        centerCube = mirrors[i];
                    }

                    i++;


                    //wireframe
                    GameObject wireframeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Destroy(wireframeCube.GetComponent<Collider>());
                    wireframeCube.name = "wireframe Cube";
                    wireframeCube.transform.parent = transform;
                    wireframeCube.transform.position = new Vector3(x * scale - DATA.DimensionData.matrixDiameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
                    wireframeCube.transform.localScale = new Vector3(scale, scale, scale);

                    Renderer r = wireframeCube.GetComponent<Renderer>();
                    r.material = lineMat;
                    r.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    r.receiveShadows = false;
                    r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    //wireframeCube.isStatic = true;
                    wireframeCube.transform.parent = wireframeCubeContainer.transform;

                }
            }

        }



    }

    // Use this for initialization
    void Start()
    {
  

        //wireframeCubeContainer.isStatic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (matrixRotate)
        {
            transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);

        }
    }

    internal void ChangeMirrorsShader(Shader newShader)
    {
        mirrorMat.shader = newShader;
    }

    internal void UseBugFixValueInShader(bool ifUse)
    {
        mirrorMat.SetInt("_UseBugFixValue", ifUse ? 1 : 0);
    }


    internal void KickOffBreaking()
    {
        foreach (var mirror in mirrors)
        {
            mirror.GetComponent<Mirror>().StartBreak();
        }
    }

    internal void SetMirrorSize(float newSize)
    {
        foreach (GameObject mirror in mirrors)
        {
            DATA.DimensionData.resetSize(newSize);
            if (mirror != null) mirror.transform.localScale = new Vector3(DATA.DimensionData.cubeDia, DATA.DimensionData.cubeDia, DATA.DimensionData.cubeDia);
        }
    }


    internal void SetSelfRotate(bool value)
    {
        try
        {
            foreach (GameObject mirror in mirrors)
            {
                Mirror code = mirror.GetComponent<Mirror>();
                code.SelfRotate = value;
                if (!value)
                {
                    mirror.transform.localRotation = Quaternion.identity;
                }
            }
        }
        catch (System.Exception e)
        {
            print(e);
        }
    }
}
