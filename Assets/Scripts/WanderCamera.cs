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

    public bool BigPrismMode { get; set; }


    // Use this for initialization
    void Start()
    {
        area = MirrorManager.main.Dimension.diameter * 1f;
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
        if (BigPrismMode)
        {
            transform.position = new Vector3(5.4f, 41f, -120f);
            transform.LookAt(MainPrism.main.gameObject.transform);
        }
        else
        {
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
        }

    }

    public void SwitchMode(bool lookAtBigPrism)
    {
        BigPrismMode = lookAtBigPrism;
        if (!BigPrismMode) transform.rotation = originalRot;
    }

}
