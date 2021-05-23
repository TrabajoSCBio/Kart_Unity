using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using TMPro;
using System.Diagnostics;
using System.Text;

public class ReadPythonSocket : MonoBehaviour
{
    // Use this for initialization
    public TextMeshProUGUI mano;
    public TextMeshProUGUI mano2;
    [HideInInspector] public bool acelerar;
    [HideInInspector] public bool frenar;
    [HideInInspector] public float giro;
    [HideInInspector] public bool objeto;
    TcpListener listener;
    string[] msg;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        listener = new TcpListener(IPAddress.Parse("127.0.0.1"),55001);
        listener.Start();
        print("is listening");
        Process bat = new Process();
        bat.StartInfo.FileName = "script.bat";
        bat.StartInfo.RedirectStandardInput = true;
        bat.StartInfo.RedirectStandardOutput = true;
        bat.StartInfo.CreateNoWindow = true;
        bat.StartInfo.UseShellExecute = false;
        bat.Start();
    }
    // Update is called once per frame
    void Update()
    {
        if (!listener.Pending())
        {

        }
        else
        {
            print("socket comes");
            string msgAll = "";
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            msgAll = reader.ReadToEnd();
            msg = msgAll.Split(' ');
            //Debug.Log(msg);
            switch (msg[0])
            {
                case "acelerar":
                acelerar = true;
                frenar = false;
                break;
                case "frenar":
                frenar = true;
                acelerar = false;
                break;
                case "objeto":
                objeto = true;
                break;
                default:
                break;
            }
            switch (msg[1])
            {
                case "izquierda":
                giro = -1f;
                break;
                case "derecha":
                giro = 1f;
                break;
                case "centro":
                giro = 0f;
                break;
                default:
                break;
            }
        }
    }
}
