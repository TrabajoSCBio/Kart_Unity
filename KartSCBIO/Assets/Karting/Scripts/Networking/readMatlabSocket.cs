using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;
public class readMatlabSocket : MonoBehaviour
{
    // Use this for initialization
    TcpListener listener;
    String msg;
    public bool acelerar = false;
    public bool frenar = false;
    public bool objeto = false;
    public float girar;
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
            switch (msg)
            {
                case "acelerar":
                frenar = false;
                acelerar = true;
                break;
                case "frenar":
                acelerar = false;
                frenar = true;
                break;
                case "objeto":
                objeto = true;
                break;
                default:
                break;
            }
            print(msg);
        }
    }
}