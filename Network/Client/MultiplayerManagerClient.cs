using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using TMPro;

public static class MultiplayerManagerClient
{
    public static int id; //comes from server
    public static string name;
    public static string localIp; //comes from server
    public static TcpClient clientToServerClient;
    public static Stream clientToServerStream;
    public static List<Player> connectedPlayers = new List<Player>();
    public static Player connectedPlayer;
    public static bool isServer = false;
    public static bool startGameButton = false;
    public static string remoteIp;
}
