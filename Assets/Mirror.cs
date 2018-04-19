using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    //self rotate
    float rotateSpeed;
    Vector3 rotateAxis;
    Material material;


    public Vector3 spaceID;

    Color originalColor;

    bool inMemory;



    // Use this for initialization
    void Start()
    {
        //material/texture
        material = GetComponent<Renderer>().material;
        originalColor = material.color;

        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotateSpeed = Random.Range(1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        //if (Random.Range(0f, 100f) > 99.9f && !inMemory)
        //{
        //    GoInMemory();
        //}

        if (!inMemory)
        {
            material.mainTexture = WebStream.main.GetLatestFrame();
        }

       transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
    }

    //void GoInMemory()
    //{
    //    inMemory = true;
    //    material.color = new Color32(247, 222, 96, 125);

    //    float stay = Random.Range(20, 30);
    //    Invoke("BackToMirror", stay);
    //}

    //void BackToMirror()
    //{
    //    inMemory = false;
    //    material.color = originalColor;
    //}
}
