using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderCamera : MonoBehaviour
{
    public float speed;
	public MirrorManager MM;

	float area;
	float r;

	float x;
	float y; 
	float z;

	float theta;
	float phi;

    Quaternion originalRot;
    GameObject target;

    // Use this for initialization
    void Start()
    {
		area = MirrorManager.instance.matrixDiameter;
		generateRadius ();
		generateAngel ();
		updatePos ();
    }

    // Update is called once per frame
    void Update()
    {
		//always look at the center cubeof cube matrix
		target = MirrorManager.instance.centerCube;
		transform.LookAt(target.transform);

		speed = Random.Range (-0.01f, 0.01f);
		move (speed);
		updatePos ();
    }
	

    public void GoForward(float value)
    {
        theta += value;
		phi += value;
    }

	public void generateAngel(){
		
		theta = Random.Range (-Mathf.PI, Mathf.PI);
		phi = Random.Range (-Mathf.PI, Mathf.PI);
	}

	public void generateRadius(){
		r = MirrorManager.instance.matrixDiameter * Random.Range(0, 2); //start some
	}

	private void move(float speed){
		theta += speed;
		phi += speed;
	}
		
	private void updatePos(){		
		x = r * Mathf.Sin (theta) * Mathf.Cos (phi);
		y = r * Mathf.Sin (theta) * Mathf.Sin (phi);
		z = r * Mathf.Cos (theta);

		Vector3 nextPos = new Vector3(x, y, z);
		transform.position = nextPos;
	}
		

}
