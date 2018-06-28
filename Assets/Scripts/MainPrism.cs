using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPrism : MonoBehaviour
{

    public static MainPrism main;
    Material mat;
    Vector3 rotateAxis;
    public float rotateSpeed = 2;


    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {
        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //mat.mainTexture = WebStream.main.GetLatestFrame();
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
    }
}
