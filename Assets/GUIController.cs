using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{



    public GameObject panel;
    public Funnel.Funnel syphon;

    public int syphonOutputW = 1920;
    public int syphonOutputH = 1080;
    public bool defaultSyphonState;

    Vector2 syphonRes;

    bool vis;

    private void Start()
    {
        syphonRes = new Vector2(syphonOutputW, syphonOutputH);
        UpdateSyphonState(defaultSyphonState);
    }


    public void UpdateIP(string input)
    {
        print(input);
        WebStream.main.ResetIP(input);
    }

    public void UpdateSyphonState(bool state)
    {
        syphon.enabled = state;
    }

    public void UpdateSyphonOutputWidth(string sw)
    {

        syphon.screenWidth = int.Parse(sw);
        //UpdateSyphonOutputRes();
    }

    public void UpdateSyphonOutputHeight(string sh)
    {
        syphon.screenHeight = int.Parse(sh);
    }


    void UpdateSyphonOutputRes()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            vis = !vis;
            panel.SetActive(vis);
        }
    }
}
