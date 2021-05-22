using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using TMPro;
using System.Text;

public class ReadPythonSocket : MonoBehaviour
{
    // Use this for initialization
    public TextMeshProUGUI mano;
    [HideInInspector] public bool acelerar;
    [HideInInspector] public bool frenar;
    [HideInInspector] public float giro;
    [HideInInspector] public bool objeto;
    TcpListener listener;
    string msg;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        listener = new TcpListener(IPAddress.Parse("127.0.0.1"),55001);
        listener.Start();
        print("is listening");
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
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            msg = reader.ReadToEnd();
            mano.text = msg;
            Debug.Log(msg);
            switch (msg)
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
