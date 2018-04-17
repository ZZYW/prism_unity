using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMirror : MonoBehaviour
{

    public Material mirrorMat;


    int n = 700;
    float area = 500;

    float scale = 100;

    GameObject[] mirrors;


    float rotateAngle = 1f;

    // Use this for initialization
    void Start()
    {
        mirrors = new GameObject[n];

        for (int i = 0; i < mirrors.Length; i++)
        {
            mirrors[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mirrors[i].transform.parent = transform;
            mirrors[i].transform.localScale = new Vector3(scale, scale, scale);
            mirrors[i].GetComponent<Collider>().enabled = false;
            //mirrors[i].isStatic = true;
            mirrors[i].transform.position = new Vector3(Random.Range(-area, area), Random.Range(-area, area), Random.Range(-area, area));
            mirrors[i].transform.Rotate(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            mirrors[i].GetComponent<Renderer>().material = mirrorMat;
            mirrors[i].AddComponent<Mirror>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //mirrorMat.mainTexture = WebStream.main.latestFrame;


        transform.Rotate(Vector3.up + Vector3.forward / 2, rotateAngle * Time.deltaTime);

    }
}
