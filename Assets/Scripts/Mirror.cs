using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    public bool SelfRotate { get; set; }
    public Vector3 SpaceID { get; set; }



    float rotateSpeed;
    Vector3 rotateAxis;

    //Vector3 oScale;
    //Vector3 minScale;

    //float scaleDelta;
    //float scaleFactor;



    // Use this for initialization
    void Start()
    {
        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotateSpeed = Random.Range(1f, 10f);
        //oScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (SelfRotate) transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);

        //scaleFactor = (Mathf.Sin(scaleDelta) + 1f) / 2f;
        //transform.localScale = scaleFactor * oScale;



        //scaleDelta += 0.1f * Time.deltaTime;


    }

    //private void OnGUI()
    //{
    //    if ((int)SpaceID.x == 0 && (int)SpaceID.y == 0 && (int)SpaceID.z == 0 )
    //    {
    //        GUI.TextField(new Rect(10, 10, 100, 20), scaleFactor.ToString());
    //    }
    //}



}
