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
                return 10;
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
                return 0.8f * grid;
            }
        }
    }
    public static MirrorManager main;
    public Material mirrorMat;

    GameObject[] mirrors;
    float rotateAngle = 1f;

    private void Awake()
    {
        main = this;
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
                    mirrors[i].isStatic = true;
                    mirrors[i].transform.position = new Vector3(x * scale - Dimension.diameter / 2, y * scale - diameter / 2, z * scale - diameter / 2);
                    mirrors[i].GetComponent<Renderer>().material = mirrorMat;
                    Mirror code = mirrors[i].AddComponent<Mirror>();
                    code.spaceID = new Vector3(x, y, z);
                    i++;
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);
    }
}
