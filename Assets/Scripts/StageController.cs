using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{

    [SerializeField]
    float swtichFreq = 10f;

    public AudioSource StagSwtichingSound;



    int activeStage;
    WanderCamera camControl;

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("SwitchStage", 30f, swtichFreq);
        camControl = Camera.main.GetComponent<WanderCamera>();
        SwtichStage(activeStage);
        InvokeRepeating("ProceedStage", swtichFreq, swtichFreq);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            ProceedStage();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            print("Reloading");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }


    void ProceedStage()
    {
        print("Proceeding Stage");

        activeStage++;
        if (activeStage > 2) activeStage = 0;
        SwtichStage(activeStage);

        StagSwtichingSound.Play();

    }

    void SwtichStage(int targetStage)
    {
        switch (targetStage)
        {
            //just mirrors
            case 0:
                //mirrors
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                //wireframe cubes
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                //big prism
                MainPrism.main.gameObject.SetActive(false);

                MirrorManager.main.SetMirrorSize(0.8f);
                camControl.SwitchMode(false);
                break;
            //smaller mirror and wireframe
            case 1:
                //mirror
                MirrorManager.mirrorContainer.gameObject.SetActive(true);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(true);
                //big prism
                MainPrism.main.gameObject.SetActive(false);

                MirrorManager.main.SetMirrorSize(0.2f);
                camControl.SwitchMode(false);
                break;

            //just big prism
            case 2:
                //mirror
                MirrorManager.mirrorContainer.gameObject.SetActive(false);
                //wireframe cube
                MirrorManager.wireframeCubeContainer.gameObject.SetActive(false);
                //big prism
                MainPrism.main.gameObject.SetActive(true);

                camControl.SwitchMode(true);
                break;
        }

    }
}
