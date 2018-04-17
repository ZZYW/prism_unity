using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    //self rotate
    float rotateSpeed;
    Vector3 rotateAxis;

    //texture scaling
    Material material;
    Vector2 textureScale;
    float ix, iy;
    float ixPlus;
    bool changeTextureScaling;

    public Vector3 spaceID;

    // Use this for initialization
    void Start()
    {

        //material/texture
        material = GetComponent<Renderer>().material;
        textureScale = new Vector2(1, 1);
        ixPlus = Random.Range(0.01f, 0.03f);
        if (Random.Range(0f, 1f) > 0.7f)
        {
            changeTextureScaling = true;
        }


        changeTextureScaling = false;


        //self rotate
        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotateSpeed = Random.Range(1f, 10f);

    }

    // Update is called once per frame
    void Update()
    {
        if (changeTextureScaling)
        {
            ix += ixPlus * Time.deltaTime;

            textureScale = new Vector2((Mathf.Sin(ix) + 1) * 50, 1);

            material.mainTextureScale = textureScale;
        }

        material.mainTexture = WebStream.main.latestFrame;
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);

    }
}
