using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	//create a instance of SceneController so we can call this instance from other places directly
	public static SceneController instance;
	public AudioController AC;

	private Kino.DigitalGlitch digiGlitch;
	private Kino.AnalogGlitch anaGlitch;

	WanderCamera camControl;
	//custom camera controller script we wrote ourselves


	public int stage;


	private void Awake ()
	{
		instance = this;

	}

	void Start ()
	{
		camControl = Camera.main.GetComponent<WanderCamera> ();
		digiGlitch = Camera.main.GetComponent<Kino.DigitalGlitch> ();
		anaGlitch = Camera.main.GetComponent<Kino.AnalogGlitch> ();
	}


	private void Update ()
	{
		//test
		/*
		Material mymat;
		mymat.SetColor ("_Color", Color.red);
		mymat.SetFloat ("_Multiplier_displacement", 0.5f);
		*/

		//set stage
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SwitchStage (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SwitchStage (1);
		}

		//once stage is switched to the matching stage, the following functions will always get called 40 times per second
		switch (stage) {
		case 0:
			anaGlitch.verticalJump = Map (AC.introDB, 0.002f, 0.08f, 0, 1);
			digiGlitch.intensity = Map (AC.introDB, 0.002f, 0.08f, 0, 1);
			//Debug.Log ("introDB " + AC.introDB);
			break;
		case 1:
			anaGlitch.verticalJump = Map (AC.colorAmbientDB, 0.01f, 0.2f, 0, 1);
			digiGlitch.intensity = Map (AC.colorAmbientDB, 0.01f, 0.2f, 0, 1);
			Debug.Log ("colorAmbientDB " + AC.colorAmbientDB);
			break;
		}

	}

	//switchStage will only get called once whenever keys mapped to this stage is pressed
	private void SwitchStage (int targetStage)
	{
		switch (targetStage) {
		case 0:
			stage = 0;
			MirrorManager.mirrorContainer.SetActive (true);
			MainPrism.main.gameObject.SetActive (false);
			camControl.generateAngel ();
			camControl.generateRadius ();

			for (int i = 0; i < AC.allSources.Length; i++) {
				AC.allSources [i].Stop ();
			}
			AC.intro.Play ();
			
			break;
		case 1:
			stage = 1;
			MirrorManager.mirrorContainer.SetActive (false);
			MainPrism.main.gameObject.SetActive (true);
			camControl.generateAngel ();
			camControl.generateRadius ();
			for (int i = 0; i < AC.allSources.Length; i++) {
				AC.allSources [i].Stop ();
			}
			AC.colorAmbient.Play ();
				//Debug.Log (AC.allSources[5].isPlaying);


			break;
		}

	}


	public float Map (float x, float in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}

}
