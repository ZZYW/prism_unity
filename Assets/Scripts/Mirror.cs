using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{


    bool _selfRotate;
    public bool SelfRotate
    {
        get
        {
            return _selfRotate;
        }
        set
        {
            _selfRotate = value;
            if (!value)
            {
                localRotation = transform.localRotation;
            }
            else
            {
                transform.localRotation = localRotation;
            }

        }
    }
    public Vector3 SpaceID { get; set; }



    float rotateSpeed;
    Vector3 rotateAxis;


    Quaternion localRotation;

    // Use this for initialization
    void Start()
    {
        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotateSpeed = Random.Range(1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        if (SelfRotate)
        {
            transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
        }


    }





}
