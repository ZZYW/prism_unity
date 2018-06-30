using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Receive and send OSC data to the iPad control panel
/// </summary>
[RequireComponent(typeof(OSC))]
public class OSCController : MonoBehaviour
{
    private static bool created = false;

    //event types
    public delegate void FloatMsg(float m);
    public delegate void IntMsg(int m);
    public delegate void IntPlusValueMsg(int i, float v);
    public delegate void BoolMsg();

    public static OSCController instance = null;

    private OSC oscObject;
    private bool showIP = true;
    private string ip = "";

    struct Paths
    {
        public const string stage = "/stage";
        public const string shakefreq = "/glitch/freq/";
        public const string shakeintense = "/glitch/intense";
    }


    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of OSCController is detected, deleting...");
            Destroy(gameObject);
        }

        ip = LocalIPAddress();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            showIP = !showIP;
        }
    }

    private string LocalIPAddress()
    {
        System.Net.IPHostEntry host;
        string localIP = "";
        host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (System.Net.IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private void OnGUI()
    {
        if (showIP)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(10, 10, 300, 50), "ip: " + ip + "     port:   " + oscObject.inPort);
        }
    }

    void Start()
    {
        oscObject = GetComponent<OSC>();
        oscObject.SetAllMessageHandler(ProcessMessage);
    }

    void ProcessMessage(OscMessage msg)
    {

        Debug.Log(msg);

        switch (msg.address)
        {
            case Paths.shakefreq:
                VariableController.instance.vertexOffsetFreq = msg.GetFloat(0);
                break;
            case Paths.shakeintense:
                VariableController.instance.vertexOffsetIntense = msg.GetFloat(0);
                break;
            case Paths.stage:
                StageController.instance.SwtichStage((int)msg.GetFloat(0));
                break;
            default:
                Debug.LogWarning("Incoming message has no matched handler");
                break;
        }
    }
}
