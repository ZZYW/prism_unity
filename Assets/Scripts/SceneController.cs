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

		switch (stage) {
		case 0:
			anaGlitch.verticalJump = Map (AC.introDB, 0.002f, 0.08f, 0, 1);
			digiGlitch.intensity = Map (AC.introDB, 0.002f, 0.08f, 0, 1);
			Debug.Log ("introDB " + AC.introDB);
			break;
		case 1:
			break;
		}

	}

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
			AC.intro.Play ();
				//Debug.Log (AC.allSources[5].isPlaying);


			break;
		

		/*
            //just mirrors
            case 0:
                //mirrors
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                MirrorManager.instance.SetSelfRotate(true);
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                MainPrism.main.gameObject.SetActive(false);
                //MirrorManager.instance.SetMirrorSize(DATA.LARGE_MIRROR_SIZE_PERCENTAGE);
//                EnvController.instance.SetWorldColor(normalWorldColor);
                camControl.SwitchMode(WanderCamera.MODE.NORMAL);
                //MirrorManager.instance.UseBugFixValueInShader(true);
                break;

            //smaller mirror and wireframe
            case 1:
                //mirror
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                MirrorManager.instance.SetSelfRotate(true);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(true);
                //big prism
                MainPrism.main.gameObject.SetActive(false);

                //MirrorManager.instance.SetMirrorSize(DATA.SMALL_MIRROR_SIZE_PERCENTAGE);
//                EnvController.instance.SetWorldColor(normalWorldColor);
                camControl.SwitchMode(WanderCamera.MODE.NORMAL);

                break;

            // mirror only but no self rotating
            case 2:
                //mirror
                //MirrorManager.instance.UseBugFixValueInShader(true);
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                MirrorManager.instance.SetSelfRotate(false);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                //big prism
                MainPrism.main.gameObject.SetActive(false);

                //MirrorManager.instance.SetMirrorSize(DATA.MEDIUM_MIRROR_SIZE_PERCENTAGE);
                camControl.SwitchMode(WanderCamera.MODE.NORMAL);
//                EnvController.instance.SetWorldColor(normalWorldColor);
                camControl.GoForward(10);
                break;

            //just big prism
            case 3:
                //mirror
                MirrorManager.mirrorContainer.gameObject.SetActive(false);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                //big prism
                MainPrism.main.gameObject.SetActive(true);
//                EnvController.instance.SetWorldColor(bigPrismWorldColor);
                camControl.SwitchMode(WanderCamera.MODE.BIG_PRISM);
                break;

            case 4:
                //mirror
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                MirrorManager.instance.SetSelfRotate(false);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                //big prism
                MainPrism.main.gameObject.SetActive(false);
                camControl.SwitchMode(WanderCamera.MODE.LOOK_AT_CENTER_CUBE);
                break;
			*/
		}

	}


	public float Map (float x, float in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}

}
