using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager main;

    public Dimensional Dimension { get; private set; }
    public struct Dimensional
    {
        public int n { get { return 10; } }
        public float diameter { get { return grid * n; } }
        public float grid { get { return 50; } }
        public float size { get { return 0.8f * grid; } }
    }

    //controls
    public bool CubeSelfRotate;


    //Materials
    public Material mirrorMat;
    public Material lineMat;

    //containers
    public static GameObject wireframeCubeContainer;
    public static GameObject mirrorContainer;

    //vars
    GameObject[] mirrors;
    float rotateAngle = 1f;


    private void Awake()
    {
        main = this;
        mirrors = new GameObject[(int)Mathf.Pow(Dimension.n, 3)];


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
    }

    // Use this for initialization
    void Start()
    {
        print(mirrors.Length + " mirror in total.");

        int i = 0;

        float scale = Dimension.grid;
        float diameter = Dimension.diameter;
        float size = Dimension.size;

        for (int x = 0; x < Dimension.n; x++)
        {
            for (int y = 0; y < Dimension.n; y++)
            {
                for (int z = 0; z < Dimension.n; z++)
                {
                    mirrors[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    mirrors[i].transform.parent = mirrorContainer.transform;
                    mirrors[i].transform.localScale = new Vector3(size, size, size);
                    mirrors[i].GetComponent<Collider>().enabled = false;
                    mirrors[i].transform.position = new Vector3(x * scale - Dimension.diameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
                    mirrors[i].GetComponent<Renderer>().material = mirrorMat;
                    Mirror code = mirrors[i].AddComponent<Mirror>();
                    code.SelfRotate = CubeSelfRotate;
                    code.SpaceID = new Vector3(x, y, z);
                    i++;


                    //wireframe
                    GameObject wireframeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Destroy(wireframeCube.GetComponent<Collider>());
                    wireframeCube.name = "wireframe Cube";
                    wireframeCube.transform.parent = transform;
                    wireframeCube.transform.position = new Vector3(x * scale - Dimension.diameter / 2 + transform.position.x,
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


        //wireframeCubeContainer.isStatic = true;

    }

    // Update is called once per frame
    void Update()
    {
        mirrorMat.mainTexture = WebStream.main.GetLatestFrame();

        transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);

    }


    internal void SetMirrorSize(float size)
    {
        foreach (GameObject mirror in mirrors)
        {
            float s = size * Dimension.grid;
            if (mirror != null) mirror.transform.localScale = new Vector3(s, s, s);
        }
    }
}
