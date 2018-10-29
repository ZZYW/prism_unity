using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderCamera : MonoBehaviour
{
    public float speed = 1f;

    float area;
    float x1, x2;
    float y1, y2;
    float z1, z2;
    Quaternion originalRot;
    GameObject target;

	//Enumerations allow you to create a collection of related constants.
    public enum MODE
    {
        BIG_PRISM, NORMAL, LOOK_AT_CENTER_CUBE
    }

    public MODE Mode = MODE.NORMAL;

    // Use this for initialization
    void Start()
    {
        area = DATA.DimensionData.matrixDiameter * 0.8f;
        x1 = Random.Range(0f, 100f);
        x2 = Random.Range(0f, 100f);
        y1 = Random.Range(0f, 100f);
        y2 = Random.Range(0f, 100f);
        z1 = Random.Range(0f, 100f);
        z2 = Random.Range(0f, 100f);

        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Mode)
        {
            case MODE.BIG_PRISM:
                transform.position = new Vector3(5.4f, 41f, -120f);
                transform.LookAt(MainPrism.main.gameObject.transform);
                break;


            case MODE.NORMAL:
                float x = Mathf.PerlinNoise(x1, x2) * area - area / 3;
                float y = Mathf.PerlinNoise(y1, y2) * area - area / 3;
                float z = Mathf.PerlinNoise(z1, z2) * area - area / 3;

                x1 += speed * Time.deltaTime;
                x2 += speed * Time.deltaTime;
                y1 += speed * Time.deltaTime;
                y2 += speed * Time.deltaTime;
                z1 += speed * Time.deltaTime;
                z2 += speed * Time.deltaTime;

                Vector3 nextPos = new Vector3(x, y, z);

                transform.position = nextPos;
                break;


            case MODE.LOOK_AT_CENTER_CUBE:
                Vector3 pos = transform.position;
                pos.z += 1f * Time.deltaTime;
                transform.position = pos;
                transform.LookAt(target.transform);
                float dis = Vector3.Distance(transform.position, target.transform.position);

                break;
        }


    }


    public void SwitchMode(MODE newMode)
    {
        Mode = newMode;
        if (Mode == MODE.NORMAL)
        {
            transform.rotation = originalRot;
        }
        if (Mode == MODE.LOOK_AT_CENTER_CUBE)
        {
            //setup LACC mode
            target = MirrorManager.instance.centerCube;
            transform.position = target.transform.position;
        }
    }

    public void GoForward(float value)
    {
        x1 += value;
        x2 += value;
        y1 += value;
        y2 += value;
        z1 += value;
        z2 += value;
    }

}
