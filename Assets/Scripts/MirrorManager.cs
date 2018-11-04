using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
	public int npr = 10; //number of cubes per roll. total is 10x10x10=1000
	public float gridSize = 50;
	public float cubeDia = 40; //0.8 x gridSize
	public float matrixDiameter = 500; //gridSize x npr
	public float cubeR = 20; //cubeDia/2
	public GameObject centerCube; //allow camera to look at the center of 1000 cubes

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

    public GameObject kkkk;

    //vars
    GameObject[] mirrors;
    float rotateAngle = 1f;

	//enable matrix to self-rotate
    public bool matrixRotate = true;


    private void Awake()
    {

        instance = this;

		//total number of mirrors is 10^3 = 1000
        mirrors = new GameObject[(int)Mathf.Pow(npr, 3)];


        if (wireframeCubeContainer == null)
        {
			//create game object
            wireframeCubeContainer = new GameObject("Wireframe Cube Container");
			//tell its parent to be the game object where this script is attached to.
            wireframeCubeContainer.transform.parent = transform;
        }


        if (mirrorContainer == null)
        {
			//create game object
            mirrorContainer = new GameObject("Mirror Container");
			//tell its parent to be the game object where this script is attached to.
            mirrorContainer.transform.parent = transform;
        }


        print(mirrors.Length + " mirror in total.");



        int i = 0;
		float size = cubeDia;
        float scale = size;
        float diameter = matrixDiameter;
        

        for (int x = 0; x < npr; x++)
        {
            for (int y = 0; y < npr; y++)
            {
                for (int z = 0; z < npr; z++)
                {
					//create some primitive shapes, such as a cube
                    mirrors[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					//put these new shapes into their parent - the mirrorContainer
                    mirrors[i].transform.parent = mirrorContainer.transform;
					//set each cube size
                    mirrors[i].transform.localScale = new Vector3(size, size, size);
					//disable collider. we just want the look
                    mirrors[i].GetComponent<Collider>().enabled = false;
                    mirrors[i].transform.position = new Vector3(x * scale - diameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
					
                    mirrors[i].GetComponent<Renderer>().material = mirrorMat;
                    Mirror code = mirrors[i].AddComponent<Mirror>();
                    code.SelfRotate = CubeSelfRotate;
                    code.SpaceID = new Vector3(x, y, z);

					//define center cube
                    int n = npr;
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
                    wireframeCube.transform.position = new Vector3(x * scale - matrixDiameter / 2 + transform.position.x,
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

        wireframeCubeContainer.isStatic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (matrixRotate)
        {
            transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);

        }
    }

//    internal void ChangeMirrorsShader(Shader newShader)
//    {
//        mirrorMat.shader = newShader;
//    }

//    internal void UseBugFixValueInShader(bool ifUse)
//    {
//        mirrorMat.SetInt("_UseBugFixValue", ifUse ? 1 : 0);
//    }



//    internal void SetMirrorSize(float newSize)
//    {
//        foreach (GameObject mirror in mirrors)
//        {
//            DATA.DimensionData.resetSize(newSize);
//            if (mirror != null) mirror.transform.localScale = new Vector3(cubeDia, cubeDia);
//        }
//    }


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
					//local rotation = no rotation
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
