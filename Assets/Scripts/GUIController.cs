using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public GameObject panel;
    public Funnel.Funnel syphon;

    public bool defaultSyphonState;
    bool vis;

    private void Start()
    {
        UpdateSyphonState(defaultSyphonState);
        panel.SetActive(vis);
    }


    public void UpdateIP(string input)
    {
        // VideoSource.main.ResetIP(input);
    }

    public void UpdateSyphonState(bool state)
    {
        print(state);
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
