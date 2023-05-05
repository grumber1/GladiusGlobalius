using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

public static class MultiplayerManagerServer
{
    public static bool startGameButton = false;
    public static List<Player> connectedPlayers = new List<Player>();
    public static List<IPAddress> connectedIpAddresses = new List<IPAddress>();
    public static List<Thread> clientHandlingThreads = new List<Thread>();
    public static List<TcpClient> serverToClientClients = new List<TcpClient>();
    public static List<Stream> serverToClientStreams = new List<Stream>();
}
