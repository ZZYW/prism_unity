using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{

    public static Dimensional Dimension;
    public struct Dimensional
    {
        public int n
        {
            get
            {
                return 12;
            }
        }
        public float diameter
        {
            get
            {
                return grid * n;
            }
        }
        public float grid
        {
            get
            {
                return 50;
            }
        }
        public float size
        {
            get
            {
                return 0.3f * grid;
            }
        }
    }

    public Material mirrorMat;
    public Material lineMat;

    GameObject[] mirrors;
    float rotateAngle = 1f;

    public bool cubeSelfRotate;

    private void Awake()
    {
        mirrors = new GameObject[(int)Mathf.Pow(Dimension.n, 3)];
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
                    mirrors[i].transform.parent = transform;
                    mirrors[i].transform.localScale = new Vector3(size, size, size);
                    mirrors[i].GetComponent<Collider>().enabled = false;
                    //mirrors[i].isStatic = true;
                    mirrors[i].transform.position = new Vector3(x * scale - Dimension.diameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
                    mirrors[i].GetComponent<Renderer>().material = mirrorMat;
                    //mirrors[i].
                    Mirror code = mirrors[i].AddComponent<Mirror>();
                    code.selfRotate = cubeSelfRotate;
                    code.spaceID = new Vector3(x, y, z);
                    i++;


                    GameObject wireframeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wireframeCube.name = "wireframe Cube";
                    wireframeCube.transform.parent = transform;
                    wireframeCube.transform.position = new Vector3(x * scale - Dimension.diameter / 2 + transform.position.x,
                                                                y * scale - diameter / 2 + transform.position.y,
                                                                z * scale - diameter / 2 + transform.position.z);
                    wireframeCube.transform.localScale = new Vector3(scale, scale, scale);
                    wireframeCube.GetComponent<Renderer>().material = lineMat;
                    Destroy(wireframeCube.GetComponent<Collider>());
                    
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        mirrorMat.mainTexture = WebStream.main.GetLatestFrame();

        transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);

        //for (int i = 0;i<)
    }
}
