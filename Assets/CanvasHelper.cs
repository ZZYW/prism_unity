using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHelper : MonoBehaviour
{

    [SerializeField]
    Slider dragbleProgressBar;
    [SerializeField]
    Slider nondragbleProgressBar;


    // Use this for initialization
    void Start()
    {
        dragbleProgressBar.onValueChanged.AddListener((arg0) => SetProgress(arg0));
    }

    // Update is called once per frame
    void Update()
    {
        nondragbleProgressBar.value = AudioController.instance.GetProgress();
    }

    void SetProgress(float p)
    {
        AudioController.instance.SetProgress(p);
    }
}
