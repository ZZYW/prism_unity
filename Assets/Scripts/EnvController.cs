using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : MonoBehaviour {


    public GameObject plane;
    public static EnvController instance;


    Material planeMat;

	// Use this for initialization
	void Awake () {
        instance = this;
        planeMat = plane.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWorldColor(Color c){
        Camera.main.backgroundColor = c;
        planeMat.color = c;
    }

   
}
