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
    public Vector3 SpaceID;



    float rotateSpeed;
    Vector3 rotateAxis;

    public bool wander = true;
    bool brokeOut;
    bool shaking;
    [SerializeField]
    float breakingOutProgress;
    float breakingOutSpeed;


    Quaternion localRotation;
    //Vector3 pos;

    public int id;

    // Use this for initialization
    void Start()
    {
        shaking = false;
        brokeOut = false;
        breakingOutProgress = 0;
        breakingOutSpeed = 1.5f;

        rotateAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotateSpeed = Random.Range(1f, 10f);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (SelfRotate)
        {
            transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
        }

        if (shaking && !brokeOut)
        {
            breakingOutProgress += breakingOutSpeed * Time.deltaTime;
            float randomRage = 10f * (breakingOutProgress / 100);
            transform.position += new Vector3(Random.Range(-randomRage, randomRage), Random.Range(-randomRage, randomRage), Random.Range(-randomRage, randomRage));
            if (breakingOutProgress > 100)
            {
                float force = 10000;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                TrailRenderer tr = gameObject.AddComponent<TrailRenderer>();
                tr.material = MirrorManager.instance.trailMat;
                tr.time = 10;
                gameObject.AddComponent<BoxCollider>();
                rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
                brokeOut = true;
            }

        }

    }

    internal void StartBreak()
    {
        Invoke("StartShake", Random.Range(0f, 10f));
    }

    void StartShake()
    {
        if (!brokeOut)
        {
            shaking = true;
            //pos = transform.position;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Camera.main.gameObject)
        {
            Debug.Log("main cam exit");
        }
    }







}
