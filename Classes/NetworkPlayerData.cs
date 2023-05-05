using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkPlayerData
{
    public string ip;

    public NetworkPlayerData() { }

    //Constructor
    public NetworkPlayerData(string newIp)
    {
        ip = newIp;
    }
}
